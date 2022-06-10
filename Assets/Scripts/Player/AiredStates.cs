using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Aired : PlayerState
{
    protected bool isGrounded;
    protected Vector2 movementInput;

    public Aired(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {    }

    public override void Enter()
    {
        base.Enter();
        this.isGrounded = false;
        this.movementInput = Vector2.zero;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (movementInput.x > 0.1f)
            player.spriteRenderer.flipX = false;
        else if (movementInput.x < -0.1f)
            player.spriteRenderer.flipX = true;

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

        /*
        if(isGrounded)
            this.player.spriteRenderer.color = Color.green;
        else
            this.player.spriteRenderer.color = Color.red;
        */
    }

    public override void HandleInput()
    {
        base.HandleInput();
        this.movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        this.movementInput.y = Mathf.Clamp(movementInput.y + Input.GetAxisRaw("Jump"), -1, 1);

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        base.player.Move(movementInput.x);
    }

    public override void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);
        //isGrounded = player.IsGroundCollision(collision);
        isGrounded = player.IsGrounded();
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
        this.player.animator.SetTrigger("jump");
        AudioManager.Instance.Play("jump");

        this.player.rb.gravityScale = 2;
    }

    public override void Exit()
    {
        base.Enter();
        this.player.animator.ResetTrigger("jump");

        this.player.rb.gravityScale = 1;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(this.player.rb.velocity.y < -0.1f)
        {
            stateMachine.ChangeState(player.fall);
        }
    }
}

public class FallState : Aired
{
    public FallState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    { }

    public override void Enter()
    {
        base.Enter();
        this.player.animator.SetTrigger("fall");

        this.player.rb.gravityScale = 3;
    }

    public override void Exit()
    {
        base.Enter();
        this.player.animator.ResetTrigger("fall");

        this.player.rb.gravityScale = 1;
    }
}