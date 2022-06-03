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
    public virtual void OnCollisionStay2D(Collision2D collision)
    {}
}