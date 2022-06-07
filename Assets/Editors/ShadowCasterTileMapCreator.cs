using System.Reflection;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Rendering.Universal;
using System;
public class ShadowCasterCompositeCreator : EditorWindow
{
    CompositeCollider2D compositeCollider2D;
    bool castsShadows = true; 
    bool selfShadows = false;

    [MenuItem("Window/Shadow Caster Composite Creator")]
    static public void ShowWindow()
    {
        GetWindow<ShadowCasterCompositeCreator>("Shadow Caster Composite Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Shadow Caster Composite Creator");
        compositeCollider2D = (CompositeCollider2D)EditorGUILayout.ObjectField("Composite Collider", compositeCollider2D, typeof(CompositeCollider2D), true);
        castsShadows = EditorGUILayout.Toggle("Cast Shadows", castsShadows);
        selfShadows = EditorGUILayout.Toggle("Self Shadows", selfShadows);


        if (GUILayout.Button("Create Shadow Casters"))
        {
            CreateShadowCasters();
            EditorUtility.SetDirty(compositeCollider2D);
            compositeCollider2D = null;
        }
    }

    private void CreateShadowCasters()
    {
        ShadowCaster2DFromComposite.Build(compositeCollider2D, castsShadows, selfShadows);
    }
}

static class ShadowCaster2DFromComposite
{
    static readonly FieldInfo _meshField;
    static readonly FieldInfo _shapePathField;
    static readonly MethodInfo _generateShadowMeshMethod;

    /// <summary>
    /// Using reflection to expose required properties in ShadowCaster2D
    /// </summary>
    static ShadowCaster2DFromComposite()
    {
        _meshField = typeof(ShadowCaster2D).GetField("m_Mesh", BindingFlags.NonPublic | BindingFlags.Instance);
        _shapePathField = typeof(ShadowCaster2D).GetField("m_ShapePath", BindingFlags.NonPublic | BindingFlags.Instance);

        _generateShadowMeshMethod = typeof(ShadowCaster2D)
                                    .Assembly
                                    .GetType("UnityEngine.Rendering.Universal.ShadowUtility")
                                    .GetMethod("GenerateShadowMesh", BindingFlags.Public | BindingFlags.Static);
    }

    /// <summary>
    /// Rebuilds this specific ShadowCaster2DFromComposite
    /// </summary>
    public static void Build(CompositeCollider2D compositeCollider2D, bool castsShadows, bool selfShadows)
    {
        ShadowCaster2D[] shadowCasters = CreateShadowGameObjects(compositeCollider2D);
        for (int i = 0; i < compositeCollider2D.pathCount; i++)
        {
            List<Vector2> compositeVerts = GetCompositeVerts(compositeCollider2D,i);
            UpdateCompositeShadow(shadowCasters[i], compositeVerts, castsShadows, selfShadows);
        }
    }

    /// <summary>
    /// Automatically creates holder gameobjects for each needed ShadowCaster2D, depending on complexity of tilemap
    /// </summary>
    static ShadowCaster2D[] CreateShadowGameObjects(CompositeCollider2D compositeCollider2D)
    {

        ShadowCaster2D[] shadowCasters = compositeCollider2D.GetComponentsInChildren<ShadowCaster2D>();
        foreach(var shadowCaster in shadowCasters)
        {
            Transform.DestroyImmediate(shadowCaster.transform.gameObject);
        }

        shadowCasters = new ShadowCaster2D[compositeCollider2D.pathCount];
        //Generate new ones
        for (int i = 0; i < compositeCollider2D.pathCount; i++)
        {
            GameObject newShadowCaster = new GameObject("ShadowCaster");
            newShadowCaster.transform.parent = compositeCollider2D.transform;
            shadowCasters[i] = newShadowCaster.AddComponent<ShadowCaster2D>();
        }
        return shadowCasters;
    }

    /// <summary>
    /// Gathers all the verts of a given path shape in a CompositeCollider2D
    /// </summary>
    static List<Vector2> GetCompositeVerts(CompositeCollider2D compositeCollider2D,int path)
    {
        List<Vector2> compositeVerts = new List<Vector2>();

        Vector2[] pathVerts = new Vector2[compositeCollider2D.GetPathPointCount(path)];
        compositeCollider2D.GetPath(path, pathVerts);
        compositeVerts.AddRange(pathVerts);
        return compositeVerts;
    }

    /// <summary>
    /// Sets the verts of each ShadowCaster2D to match their corresponding
    /// verts in CompositeCollider2D and then generates the mesh
    /// </summary>
    static void UpdateCompositeShadow(ShadowCaster2D caster,List<Vector2> compositeVerts, bool castsShadows,bool selfShadows)
    {
        caster.castsShadows = castsShadows;
        caster.selfShadows = selfShadows;

        Vector2[] points = compositeVerts.ToArray();
        Vector3[] threes = ConvertArray(points);

        _shapePathField.SetValue(caster, threes);
        _meshField.SetValue(caster, new Mesh());
        _generateShadowMeshMethod.Invoke(caster,
            new object[] { _meshField.GetValue(caster),
                _shapePathField.GetValue(caster) });
    }

    //Quick method for converting a Vector2 array into a Vector3 array
    static Vector3[] ConvertArray(Vector2[] v2)
    {
        Vector3[] v3 = new Vector3[v2.Length];
        for (int i = 0; i < v3.Length; i++)
        {
            Vector2 tempV2 = v2[i];
            v3[i] = new Vector3(tempV2.x, tempV2.y);
        }
        return v3;
    }
}