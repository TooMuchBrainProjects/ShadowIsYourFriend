using UnityEditor;
using UnityEngine;
using System;
[CustomEditor(typeof(RoomManager))]
[CanEditMultipleObjects]
public class RoomManagerInspectorExtension : Editor
{
    SerializedProperty rooms;

    void OnEnable()
    {
        // Setup the SerializedProperties.
        rooms = serializedObject.FindProperty("rooms");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
        serializedObject.Update();

        if (GUILayout.Button("Add Selected Rooms")) { AddSelectedRooms(); }

        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
        serializedObject.ApplyModifiedProperties();
    }

    void AddSelectedRooms()
    {
        GameObject[] selected = Selection.gameObjects;
        
        foreach(GameObject g in selected)
        {
            if (g.gameObject.GetComponent<Room>() == null)
            {
                Debug.LogError("There are GameObjects which aren't rooms in the selection!");
                return;
            }
        }

        foreach(GameObject g in selected)
        {
            rooms.InsertArrayElementAtIndex(rooms.arraySize);
            rooms.GetArrayElementAtIndex(rooms.arraySize-1).objectReferenceValue = g;
        }
    }
}