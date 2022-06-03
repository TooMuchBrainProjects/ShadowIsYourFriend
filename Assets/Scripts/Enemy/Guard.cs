using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Guard : EnemyBehaviour
{
    [Header("Guard Settings")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float maxMoveSpeed;
    [SerializeField] public Transform[] waypoints;
    private IEnumerator waypointsIter;

    private new Rigidbody2D rigidbody;
    private Animator animator;

    protected override void Start()
    {
        base.Start();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();


        if (waypoints.Length < 0)
            throw new UnityException("No Waypoints defined.");

        waypointsIter = waypoints.GetEnumerator();
        waypointsIter.MoveNext();
        RotateTowardsWaypoint();
    }

    private void RotateTowardsWaypoint()
    {
        if ((waypointsIter.Current as Transform).position.x - transform.position.x >= 0)
            this.transform.localRotation = Quaternion.Euler(0,transform.rotation.y,0);
        else
            this.transform.localRotation = Quaternion.Euler(180f, transform.rotation.y, 180f);
    }
    
    public override void SeekMovement()
    {
        if( Mathf.Abs((((Transform)waypointsIter.Current).position - transform.position).x) < 0.2f)
        {
            if (!waypointsIter.MoveNext())
            {
                waypointsIter = waypoints.GetEnumerator();
                waypointsIter.MoveNext();
            }
            RotateTowardsWaypoint();
            //rigidbody.AddForce(Mathf.Sign(rigidbody.velocity.x/2) * -1f * Mathf.Clamp(Mathf.Abs(rigidbody.velocity.x), 0,maxMoveSpeed) * Vector2.right , ForceMode2D.Impulse);
        }

        Debug.DrawRay(transform.position, (waypointsIter.Current as Transform).position-transform.position);


        float force = maxMoveSpeed * Time.deltaTime;

        if (Mathf.Abs(force + rigidbody.velocity.x) > maxMoveSpeed)
            force = Mathf.Clamp(maxMoveSpeed - Mathf.Abs(rigidbody.velocity.x), 0, maxMoveSpeed) * Time.deltaTime;

        rigidbody.velocity += (Vector2)(Mathf.Sign((waypointsIter.Current as Transform).position.x - transform.position.x) * Vector2.right * force);

    }

    public override void OnInSight()
    {
        base.OnInSight();
        animator.SetTrigger("idle");
    }

    public override void OnOutSight()
    {
        base.OnOutSight();
        animator.SetTrigger("walking");
    }
}