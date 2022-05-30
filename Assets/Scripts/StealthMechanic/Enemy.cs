using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int attentionRaiseValue;

    [SerializeField] StealthMaster target;
    [SerializeField] float viewAngle;
    [SerializeField] float viewRange;
    [SerializeField] Vector2 viewOffset;

    [SerializeField] LayerMask targetMask;
    
    void Update()
    {

        if (CanSeeTarget())
        {
            Debug.Log("Can See!");
            target.AttentionAttracted(this);
        }
        else
            target.AttentionLost(this);
    }

    bool CanSeeTarget()
    {
        /*
        float distance = (target.position - transform.position).magnitude;
        if (distance <= viewRange)
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position + (Vector3)viewOffset, transform.position );
            if (ray.transform == target)
            {
                Debug.DrawRay(transform.position + (Vector3)viewOffset, transform.position - target.position, Color.green);
            }
        }*/
                   Debug.DrawRay(transform.position + (Vector3)viewOffset, Vector2.left, Color.magenta);
        return Physics2D.Raycast(transform.position + (Vector3)viewOffset, Vector2.left, 1f, targetMask);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawRay(transform.position + (Vector3)viewOffset,new Vector3(1,Mathf.Tan(Mathf.Deg2Rad * viewAngle)));
        //Gizmos.DrawRay(transform.position + (Vector3)viewOffset,new Vector3(1,Mathf.Tan(Mathf.Deg2Rad * viewAngle) * -1));
    }
}
