using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float viewAngle;
    [SerializeField] float viewRange;
    [SerializeField] float heightofEyes;
    void Start()
    {
        
    }
    
    void Update()
    {

    }

    bool CanSeeTarget()
    {
        float distance = (target.position - transform.position).magnitude;
        if (distance <= viewRange)
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, target.position);
            if (ray.transform == target)
            {
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(new Ray(transform.position + new Vector3(0,heightofEyes),new Vector3(1,0)));
    }
}
