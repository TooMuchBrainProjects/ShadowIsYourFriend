using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthState : State
{
    protected StealthMaster stealthmaster;
    public StealthState(StealthMaster stealthmaster, StateMachine stateMachine) : base(stateMachine)
    {
        this.stealthmaster = stealthmaster;
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.LogWarning($"State Changed to {this.ToString()}");
    }
}


public class Invisible : StealthState
{
    public Invisible(StealthMaster stealthmaster, StateMachine stateMachine) : base(stealthmaster, stateMachine)
    {}

    public override void Enter()
    {
        base.Enter();
        AudioManager.Instance.StopWithFade("visible", 0.25f);
        AudioManager.Instance.Play("invisible");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(stealthmaster.watchers.Count > 0)
        {
            stateMachine.ChangeState(stealthmaster.visible);
        }
    }
    public override void Exit()
    {
        base.Exit();
        AudioManager.Instance.PauseWithFade("invisible",0.25f);
    }
}

public class Visible : StealthState
{

    private Coroutine attentionRaiseUpdateCoroutine;
    public Visible(StealthMaster stealthmaster, StateMachine stateMachine) : base(stealthmaster, stateMachine)
    { }

    public override void Enter()
    {
        base.Enter();
        attentionRaiseUpdateCoroutine = stealthmaster.StartCoroutine(stealthmaster.AttentionRaiseUpdate());
        AudioManager.Instance.Play("visible");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (stealthmaster.watchers.Count < 1)
        {
            stateMachine.ChangeState(stealthmaster.droppingVisible);
        }

        if (stealthmaster.attention > stealthmaster.maxAttention)
        {
            stateMachine.ChangeState(stealthmaster.recognised);
        }
    }

    public override void Exit()
    {
        base.Exit();
        stealthmaster.StopCoroutine(attentionRaiseUpdateCoroutine);
    }
}

public class DroppingVisible : StealthState
{
    private Coroutine attentionDropUpdateCoroutine;

    public DroppingVisible(StealthMaster stealthmaster, StateMachine stateMachine) : base(stealthmaster, stateMachine)
    { }

    public override void Enter()
    {
        base.Enter();
        attentionDropUpdateCoroutine = stealthmaster.StartCoroutine(stealthmaster.AttentionDropUpdate());
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (stealthmaster.attention < 1)
        {
            stateMachine.ChangeState(stealthmaster.invisible);
        }
        else if (stealthmaster.watchers.Count > 0)
        {
            stateMachine.ChangeState(stealthmaster.visible);
        }
    }

    public override void Exit()
    {
        base.Exit();
        stealthmaster.StopCoroutine(attentionDropUpdateCoroutine);
    }
}

public class Recognised : StealthState
{
    public Recognised(StealthMaster stealthmaster, StateMachine stateMachine) : base(stealthmaster, stateMachine)
    { }

    public override void Enter()
    {
        base.Enter();
        AudioManager.Instance.StopWithFade("visible", 0.25f);

        EnemyBehaviour[] enemyBehaviours = stealthmaster.watchers.ToArray();
        foreach (EnemyBehaviour enemyBehaviour in enemyBehaviours)
        {
            enemyBehaviour.stateMachine.ChangeState(enemyBehaviour.recognisedState);
        }

        stealthmaster.OnRecognised.Invoke();
    }
}
