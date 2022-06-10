using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator animator;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public new BoxCollider2D collider;
    [SerializeField] public LayerMask jumpableGrounds;

    [SerializeField] public float runSpeed;
    [SerializeField] public float runMaxSpeed;
    [SerializeField] public float crouchHeightFactor;
    [SerializeField] public float crouchAngularDragFactor;
    [SerializeField] public float jumpForce;  

    public IdleState idle;
    public RunState run;
    public CrouchState crouch;
    public JumpState jump;
    public FallState fall;
    public Death death;
    StateMachine movementSM;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();

        movementSM = new StateMachine();
        idle = new IdleState(this, movementSM);
        run = new RunState(this, movementSM);
        crouch = new CrouchState(this, movementSM);
        jump = new JumpState(this, movementSM);
        fall = new FallState(this, movementSM);
        death = new Death(this, movementSM);

        movementSM.Initialize(idle);
    }

    public void Die()
    {
        movementSM.ChangeState(death);
    }

    private void Update()
    {
        movementSM.CurrentState.HandleInput();

        movementSM.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        movementSM.CurrentState.PhysicsUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        (movementSM.CurrentState as PlayerState).OnCollisionEnter2D(collision);

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        (movementSM.CurrentState as PlayerState).OnCollisionStay2D(collision);

    }

    public void Move(float directionX)
    {
        float force = runSpeed * Time.fixedDeltaTime * directionX;

        if (Mathf.Abs(force + rb.velocity.x) > runMaxSpeed)
            force = Mathf.Clamp(runMaxSpeed - Mathf.Abs(rb.velocity.x),0,runMaxSpeed) * directionX * Time.fixedDeltaTime;

        rb.velocity += new Vector2(force, 0f);
    }

    /* IsGround Detection with Collision
    public bool IsGroundCollision(Collision2D collision)
    {
        if (collision.gameObject.tag != "Ground")
            return false;

        int groundContractPoints = 0;
        foreach (ContactPoint2D contract in collision.contacts)
        {
            if (contract.normal == Vector2.up)
                groundContractPoints++;
        }

        return groundContractPoints >= 2;
    }*/

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(this.collider.bounds.center, this.collider.bounds.size, 0f, Vector2.down, .1f, this.jumpableGrounds);
    }
}
 