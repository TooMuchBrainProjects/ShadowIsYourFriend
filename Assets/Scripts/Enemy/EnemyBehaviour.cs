using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public abstract class EnemyBehaviour : MonoBehaviour
{
    [HideInInspector] public StateMachine stateMachine;
    [HideInInspector] public SeekState seekState;
    [HideInInspector] public InSightState inSightState;
    
    [Header("EnemyBehaviour Settings")]
    [SerializeField] public StealthMaster target;
    [SerializeField] public float viewAngle;
    [SerializeField] public float viewDistance;
    [SerializeField] public float viewOffsetX;
    [SerializeField] public float attentionRaiseValue;

    protected virtual void Start()
    {
        stateMachine = new StateMachine();
        seekState = new SeekState(this,stateMachine);
        inSightState = new InSightState(this,stateMachine);
        stateMachine.Initialize(seekState);
    }

    private void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
    }
    public virtual void OnInSight()
    {
        target.AttentionAttracted(this);
    }

    public virtual void OnOutSight()
    {
        target.AttentionLost(this);
    }

    public virtual float AttentionRaise(float attention) { return attentionRaiseValue; }

    public virtual void SeekMovement() { }

    public virtual void InSightMovement() { }

    private void OnDrawGizmos()
    {
        if (target == null)
            return;

        bool canSee = false;
        if(stateMachine != null)
            canSee = stateMachine.CurrentState.GetType() == typeof(InSightState);
        
        Seeker.DrawGizmos(transform, target.transform, viewAngle, viewDistance, viewOffsetX, canSee);
    }
}


public class EnemyState : State
{
    protected EnemyBehaviour behaviour { get; set; }

    protected bool canSee;

    public EnemyState(EnemyBehaviour behaviour, StateMachine stateMachine) : base(stateMachine)
    {
        this.behaviour = behaviour;
        this.canSee = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        canSee = Seeker.CanSeeTarget(behaviour.transform, behaviour.target.transform, behaviour.viewAngle, behaviour.viewDistance, behaviour.viewOffsetX);
    }

}

public class SeekState : EnemyState
{
    public SeekState(EnemyBehaviour behaviour, StateMachine stateMachine) : base(behaviour, stateMachine)
    { }

    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        behaviour.SeekMovement();
        if (base.canSee)
            base.stateMachine.ChangeState(behaviour.inSightState);
    }
    public override void Exit()
    {
        base.Exit();
    }
}

public class InSightState : EnemyState
{
    public InSightState(EnemyBehaviour behaviour, StateMachine stateMachine) : base(behaviour, stateMachine)
    { }
    public override void Enter()
    {
        base.Enter();
        base.behaviour.OnInSight();
    }
    public override void LogicUpdate()
    {
        behaviour.InSightMovement();
        base.LogicUpdate();
        if (!base.canSee)
            base.stateMachine.ChangeState(behaviour.seekState);
    }

    public override void Exit()
    {
        base.Exit();
        base.behaviour.OnOutSight();
    }
}