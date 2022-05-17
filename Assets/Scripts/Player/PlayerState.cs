using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : State
{
    protected Player player { get; set; }

    public PlayerState(Player player, StateMachine stateMachine) : base(stateMachine)
    {
        this.player = player;
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {}
}

public abstract class Grounded : PlayerState
{

    Collider2D playerCollider;

    protected bool isGrounded;
    protected Vector2 movementInput;

    public Grounded(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
        this.playerCollider = player.GetComponent<Collider2D>();
        
        this.movementInput = Vector2.zero;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (movementInput.sqrMagnitude != 0)
        {
            if (movementInput.y > 0)
            {

            }
            else if (movementInput.y < 0)
            {

            }
            else
            {
                stateMachine.ChangeState(player.running);
            }

        }
        else
            stateMachine.ChangeState(player.idle);
    }

    public override void HandleInput()
    {
        base.HandleInput();
        this.movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        isGrounded = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, .1f, this.player.jumpableGrounds);
    }
}

public class IdleState : Grounded
{
    public IdleState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {}
}

public class RunningState : Grounded
{
    public RunningState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    { }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        base.player.rb.velocity += new Vector2(movementInput.x * base.player.runningSpeed * Time.fixedDeltaTime, 0);
    }
}