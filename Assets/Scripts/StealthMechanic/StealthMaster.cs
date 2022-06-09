using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StealthMaster : MonoBehaviour
{
    [SerializeField] public AudioManager audioManager;

    [HideInInspector] public float attention;
    [SerializeField] public float maxAttention;
    [SerializeField] public float attentionUpdateDelay;
    [SerializeField] public float attentionDropBase;

    [SerializeField] public UnityEvent OnRecognised;

    [HideInInspector] public List<EnemyBehaviour> watchers;
    public Invisible invisible;
    public Visible visible;
    public DroppingVisible droppingVisible;
    public Recognised recognised;
    StateMachine stealthSM;

    private void Awake()
    {
        StealthMaster.instance = this;
    }

    void Start()
    {
        stealthSM = new StateMachine();
        invisible = new Invisible(this, stealthSM);
        visible = new Visible(this, stealthSM);
        droppingVisible = new DroppingVisible(this, stealthSM);
        recognised = new Recognised(this, stealthSM);

        stealthSM.Initialize(invisible);
    }

    public void Update()
    {
        Debug.Log($"Attention Level: {attention}, Watchers: {watchers.Count}");
        this.stealthSM.CurrentState.LogicUpdate();
    }

    public void AttentionAttracted(EnemyBehaviour enemy)
    {
        foreach (var watcher in watchers)
        {
            if (Object.ReferenceEquals(watcher,enemy))
                return;
        }
        watchers.Add(enemy);
    }

    public void AttentionLost(EnemyBehaviour enemy)
    {
        watchers.Remove(enemy);
    }

    public IEnumerator AttentionRaiseUpdate()
    {
        while (true)
        {
            //Debug.Log("AttentionRaiseUpdate");
            EnemyBehaviour[] watchersArr = watchers.ToArray();
            foreach (var watcher in watchersArr)
            {
                attention += watcher.attentionRaise(attention);
            }

            yield return new WaitForSeconds(this.attentionUpdateDelay);
        }
    }

    public IEnumerator AttentionDropUpdate()
    {
        int i = 0;
        while (true)
        {
            //Debug.Log("AttentionDropUpdate");
            int attentionDropValue = (int)Mathf.Ceil(Mathf.Pow(attentionDropBase, i));

            if (attention - attentionDropValue < 0)
                attention = 0;
            else
                attention -= attentionDropValue;

            yield return new WaitForSeconds(this.attentionUpdateDelay);
            i++;
        }
    }

    private static StealthMaster instance;
    public static StealthMaster Instance
    {
        get => instance;
        set
        {
            if (instance != null)
                throw new System.Exception("There is another StealthMaster Instance!");
            else
                instance = value;
        }
    }
}
