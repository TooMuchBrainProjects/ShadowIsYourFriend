using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCamera : EnemyBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float followMoveSpeedModifier;
    [SerializeField] public float maxMoveAngle;

    [HideInInspector] private Vector3 endTurnDir;
    [HideInInspector] private Vector3 startTurnDir;
    [HideInInspector] private Vector3 currentTurnDir;

    protected override void Start()
    {
        base.Start();
        startTurnDir = transform.right;
        endTurnDir = Seeker.RotateVector(transform.right, maxMoveAngle * Mathf.Deg2Rad).normalized;
        currentTurnDir = endTurnDir;
    }

    public override void SeekMovement()
    {
        if (RotateTowards(currentTurnDir))
            currentTurnDir = currentTurnDir == endTurnDir ? startTurnDir : endTurnDir;
    }

    public override void InSightMovement() 
    {
        RotateTowards((target.transform.position - transform.position).normalized, followMoveSpeedModifier);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Debug.DrawRay(transform.position, transform.right);
        Debug.DrawRay(transform.position, startTurnDir);
        Debug.DrawRay(transform.position, endTurnDir);
    }

    private bool RotateTowards(Vector3 turnDir, float moveSpeedmodifier = 1f)
    {
        float look = transform.eulerAngles.y == 0f ? 1f : -1f;
        transform.Rotate(new Vector3(0,0,Time.deltaTime * moveSpeed  * moveSpeedmodifier * Mathf.Sign(Vector2.SignedAngle(transform.right,turnDir) * look)));
        return (transform.right - turnDir).sqrMagnitude < 0.001f;
    }
}
