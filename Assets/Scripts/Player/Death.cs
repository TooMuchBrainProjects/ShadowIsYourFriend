using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : PlayerState
{
    public Death(Player player, StateMachine stateMachine) : base(player, stateMachine)
    { }

    public override void Enter()
    {
        base.Enter();
        this.player.animator.SetTrigger("death");
    }

    public override void Exit()
    {
        base.Enter();
        this.player.animator.ResetTrigger("death");
    }
}