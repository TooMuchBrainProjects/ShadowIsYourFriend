using UnityEngine;

public static class Seeker 
{
    public static bool CanSeeTarget(Transform transform, Transform target, float viewAngle, float viewDistance)
    {
        Vector2 targetPos = target.position;
        Vector2 viewerPos = (Vector2)transform.position;

        if ((targetPos - viewerPos).sqrMagnitude < viewDistance * viewDistance)
        {
            Vector2 dirToPlayer = (targetPos - viewerPos).normalized;
            float angleBetweenGuardAndPlayer = Vector2.Angle(transform.up, dirToPlayer);
            
            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                RaycastHit2D raycasthit =  Physics2D.Raycast(viewerPos, targetPos-viewerPos);
                return raycasthit.collider.gameObject == target.gameObject;
            }
        }
        return false;
    }
    public static Vector2 RotateVector(Vector2 vec, float angle) => new Vector2(Mathf.Cos(angle) * vec.x - Mathf.Sin(angle) * vec.y, Mathf.Sin(angle) * vec.x + Mathf.Cos(angle) * vec.y);

    /* no use case
    public static void DrawLightCone(Transform transform, Transform target, float viewAngle, float viewDistance, float viewOffsetX, bool canSeeTarget)
    {
        Gizmos.color = Color.red;

        Vector2 startPoint = transform.position + (Vector3)(new Vector2(viewOffsetX, 0.5f) * transform.right);
        Vector2 upperDir = RotateVector(transform.right, (viewAngle / 2f) * Mathf.Deg2Rad).normalized * viewDistance;
        Vector2 lowerDir = RotateVector(transform.right, (viewAngle / 2f) * Mathf.Deg2Rad * -1).normalized * viewDistance;
        Vector2 normalEndPoint = startPoint + (Vector2)transform.right * viewDistance;

        Gizmos.DrawRay(startPoint, upperDir);
        Gizmos.DrawRay(startPoint, lowerDir);
        Gizmos.DrawRay(startPoint + upperDir, normalEndPoint - (startPoint + upperDir));
        Gizmos.DrawRay(startPoint + lowerDir, normalEndPoint - (startPoint + lowerDir));

        Gizmos.color = Color.green;
        if (canSeeTarget)
            Gizmos.DrawRay(startPoint, target.transform.position - (Vector3)startPoint);
        
    }*/


    /* Curve Drawing
    void DrawQuadraticCurve(Vector2 startPoint, Vector2 middlePoint, Vector2 endPoint)
    {
        Gizmos.DrawRay(middlePoint, Vector2.up);    
        float range = Vector2.Distance(startPoint, endPoint);
        float step = (Mathf.PI*2) / Vector2.Angle(startPoint,endPoint);

        Debug.Log(step + " ; "  + range);

        for (float i = 0; i < 1f; i += step)
        {
            Vector2 point1 = Vector2.Lerp(startPoint, middlePoint, i);
            Vector2 point2 = Vector2.Lerp(middlePoint, endPoint, i);


            Vector2 point = Mathf.Pow((1f - i), 2f) * startPoint + 2f * (1f - i) * i * middlePoint + Mathf.Pow(i, 2f) * endPoint;

            if(i + step >= 1)
                Gizmos.DrawRay(point, endPoint-point);
            else
                Gizmos.DrawRay(point, (point2-point1).normalized * (range*step));
        }
    }*/
}