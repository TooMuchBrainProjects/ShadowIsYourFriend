using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Grounded : PlayerState
{
    protected bool isGrounded;
    protected Vector2 movementInput;

    public Grounded(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {}

    public override void Enter()
    {
        base.Enter();
        this.isGrounded = false;
        this.movementInput = Vector2.zero;

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isGrounded && player.rb.velocity.y < -0.1f)
            stateMachine.ChangeState(player.fall);
        else if (movementInput.sqrMagnitude != 0)
        {
            if (movementInput.y > 0)
            {
                stateMachine.ChangeState(player.jump);
            }
            else if (movementInput.y < 0)
            {
                stateMachine.ChangeState(player.crouch);

            }
            else
            {
                stateMachine.ChangeState(player.run);
            }

        }
        else
            stateMachine.ChangeState(player.idle);

        /*
        if (isGrounded)
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

    public override void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);
        //isGrounded = player.IsGroundCollision(collision);
        isGrounded = player.IsGrounded();
    }
}

public class IdleState : Grounded
{
    public IdleState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    { }

    public override void Enter()
    {
        base.Enter();
        this.player.animator.SetTrigger("idle");
    }

    public override void Exit()
    {
        base.Enter();
        this.player.animator.ResetTrigger("idle");
    }
}

public class RunState : Grounded
{
    public RunState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    { }


    public override void Enter()
    {
        base.Enter();
        this.player.animator.SetTrigger("run");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(movementInput.x > 0.1f)
            player.spriteRenderer.flipX = false;
        else if(movementInput.x < -0.1f)
            player.spriteRenderer.flipX = true;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.Move(movementInput.x);
    }
    public override void Exit()
    {
        base.Enter();
        this.player.animator.ResetTrigger("run");
    }
}

public class CrouchState : Grounded
{
    public CrouchState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {}

    public override void Enter()
    {
        base.Enter();

        this.player.rb.angularDrag *= this.player.crouchAngularDragFactor;

        float colliderYSize = this.player.collider.size.y;
        this.player.collider.offset -= new Vector2(0, (colliderYSize - (this.player.crouchHeightFactor * colliderYSize)) / 2);
        this.player.collider.size *= new Vector2(1, this.player.crouchHeightFactor);

        this.player.animator.SetTrigger("crouch");
    }
    public override void Exit()
    {
        base.Exit();

        this.player.rb.angularDrag /= this.player.crouchAngularDragFactor;

        float colliderYSize = this.player.collider.size.y;
        this.player.collider.offset -= new Vector2(0, (colliderYSize - (colliderYSize / this.player.crouchHeightFactor)) / 2);
        this.player.collider.size /= new Vector2(1, this.player.crouchHeightFactor);

        this.player.animator.ResetTrigger("crouch");
        this.player.animator.Play("player_crouch_rev");
    }
}
