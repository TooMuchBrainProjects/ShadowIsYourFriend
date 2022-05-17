using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        else if(!isGrounded)
            stateMachine.ChangeState(player.fall);
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
    { }
}

public class RunState : Grounded
{
    public RunState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    { }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        base.player.rb.velocity += new Vector2(movementInput.x * base.player.runSpeed * Time.fixedDeltaTime, 0);
    }
}

public class CrouchState : Grounded
{
    Transform playerTransform;
    Transform playerDefaultTransform;
    public CrouchState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
        playerTransform = player.transform;
        playerDefaultTransform = player.transform;
    }

    public override void Enter()
    {
        base.Enter();
        this.player.rb.angularDrag *= this.player.crouchAngularDragFactor;
        playerTransform.localScale = new Vector3(playerTransform.localScale.x, playerTransform.localScale.y * this.player.crouchHeightFactor, playerTransform.localScale.z);
        //playerTransform.position = new Vector3(playerTransform.position.x, (playerDefaultTransform.localScale.y - playerTransform.localScale.y)/2 , playerTransform.position.z);

    }
    public override void Exit()
    {
        base.Exit();
        this.player.rb.angularDrag /= this.player.crouchAngularDragFactor;
        playerTransform.localScale = new Vector3(playerTransform.localScale.x, playerTransform.localScale.y / this.player.crouchHeightFactor, playerTransform.localScale.z);
    }
}
