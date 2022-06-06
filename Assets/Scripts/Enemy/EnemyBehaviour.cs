using UnityEngine;
using UnityEngine.Rendering.Universal;
using System;

public abstract class EnemyBehaviour : MonoBehaviour
{
    [HideInInspector] public StateMachine stateMachine;
    [HideInInspector] public SeekState seekState;
    [HideInInspector] public InSightState inSightState;
    [HideInInspector] public EnemyRecognisedState recognisedState;

    [Header("EnemyBehaviour Settings")]
    [SerializeField] public StealthMaster target;
    [SerializeField] public float attentionRaiseValue;
    [HideInInspector] public Func<float,float> attentionRaise;

    [SerializeField] public new Light2D light;
    [SerializeField] public Color warnLightColor;
    [HideInInspector] private Color normalLightColor;

    protected virtual void Start()
    {
        stateMachine = new StateMachine();
        seekState = new SeekState(this,stateMachine);
        inSightState = new InSightState(this,stateMachine);
        recognisedState = new EnemyRecognisedState(this, stateMachine);
        stateMachine.Initialize(seekState);
        normalLightColor = light.color;

        attentionRaise = (attention) => attentionRaiseValue;
    }

    private void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
    }
    public virtual void OnInSight()
    {
        target.AttentionAttracted(this);
        light.color = warnLightColor;

    }

    public virtual void OnOutSight()
    {
        target.AttentionLost(this);
        light.color = normalLightColor;
    }

    public virtual void SeekMovement() { }

    public virtual void InSightMovement() { }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(stateMachine != null && stateMachine.CurrentState.GetType() == typeof(InSightState))
            Gizmos.DrawRay(light.transform.position, target.transform.position - light.transform.position);
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        (stateMachine.CurrentState as EnemyState).OnCollisionEnter2D(collision);
    }
}


public class EnemyState : State
{
    protected EnemyBehaviour behaviour { get; set; }

    protected Func<bool> canSee;

    public EnemyState(EnemyBehaviour behaviour, StateMachine stateMachine) : base(stateMachine)
    {
        this.behaviour = behaviour;
        this.canSee = () => { return Seeker.CanSeeTarget(behaviour.light.transform, behaviour.target.transform, behaviour.light.pointLightInnerAngle, behaviour.light.pointLightInnerRadius); };
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.transform == behaviour.target.transform)
        {
            stateMachine.ChangeState(behaviour.recognisedState);
        }
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
        if (base.canSee())
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
        base.LogicUpdate();
        behaviour.InSightMovement();
        if (!base.canSee())
            base.stateMachine.ChangeState(behaviour.seekState);
    }

    public override void Exit()
    {
        base.Exit();
        base.behaviour.OnOutSight();
    }
}

public class EnemyRecognisedState : EnemyState
{
    public EnemyRecognisedState(EnemyBehaviour behaviour, StateMachine stateMachine) : base(behaviour, stateMachine)
    { }

    public override void Enter()
    {
        base.Enter();
        behaviour.OnInSight();
        behaviour.attentionRaise = (attention) => Mathf.Infinity;
    }
}