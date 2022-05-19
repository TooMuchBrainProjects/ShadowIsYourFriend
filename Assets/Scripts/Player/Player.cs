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

    [SerializeField] public LayerMask jumpableGrounds;

    [SerializeField] public float runSpeed;
    [SerializeField] public float crouchHeightFactor;
    [SerializeField] public float crouchAngularDragFactor;
    [SerializeField] public float jumpForce;  

    public IdleState idle;
    public RunState run;
    public CrouchState crouch;
    public JumpState jump;
    public FallState fall;
    StateMachine movementSM;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        movementSM = new StateMachine();
        idle = new IdleState(this, movementSM);
        run = new RunState(this, movementSM);
        crouch = new CrouchState(this, movementSM);
        jump = new JumpState(this, movementSM);
        fall = new FallState(this, movementSM);

        movementSM.Initialize(idle);
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
}
