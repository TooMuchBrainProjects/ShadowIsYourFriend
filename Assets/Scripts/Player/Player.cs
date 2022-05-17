using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [SerializeField] public LayerMask jumpableGrounds;
    
    [SerializeField] public float runningSpeed;

    public IdleState idle;
    public RunningState running;
    StateMachine movementSM;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        movementSM = new StateMachine();
        idle = new IdleState(this, movementSM);
        running = new RunningState(this, movementSM);

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
