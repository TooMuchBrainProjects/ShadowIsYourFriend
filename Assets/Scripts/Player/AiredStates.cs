using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Aired : PlayerState
{

    Collider2D playerCollider;
    protected bool isGrounded;
    protected Vector2 movementInput;

    public Aired(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
        this.playerCollider = player.GetComponent<Collider2D>();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isGrounded)
        {
            if (movementInput.y > 0)
            {
                stateMachine.ChangeState(player.jump);
            }
            else if (movementInput.y < 0)
            {
                stateMachine.ChangeState(player.crouch);

            }
            else if (Mathf.Abs(movementInput.x) > 0)
            {
                stateMachine.ChangeState(player.run);

            }
            else
            {
                stateMachine.ChangeState(player.idle);
            }

        }
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
        base.player.rb.velocity += new Vector2(movementInput.x * base.player.runSpeed * Time.fixedDeltaTime, 0);

    }
}

public class JumpState : Aired
{
    public JumpState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    { }

    public override void Enter()
    {
        base.Enter();
        this.player.rb.AddForce(Vector2.up * this.player.jumpForce, ForceMode2D.Impulse);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(this.player.rb.velocity.y < 0f)
        {
            stateMachine.ChangeState(player.fall);
        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

public class FallState : Aired
{
    public FallState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}