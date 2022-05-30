using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StateMachine
{
    public State CurrentState { get; set; }

    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(State newState)
    {
        if(newState == CurrentState)
            return;

        CurrentState.Exit();

        CurrentState = newState;
        newState.Enter();
    }

}


public class State
{
    protected StateMachine stateMachine;

    public State(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
    //    Debug.Log($"State Changed to {this.ToString()}");
    }

    public virtual void HandleInput()
    { }

    public virtual void LogicUpdate()
    { }

    public virtual void PhysicsUpdate()
    { }

    public virtual void Exit()
    { }
}