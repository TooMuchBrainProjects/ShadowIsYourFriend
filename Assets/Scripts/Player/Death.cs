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
        this.player.animator.Play("player_death");
        this.player.audioManager.Play("death"); 
    }

    public override void Exit()
    {
        base.Enter();
    }
}