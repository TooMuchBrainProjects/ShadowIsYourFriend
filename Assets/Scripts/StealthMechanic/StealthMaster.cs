using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StealthMaster : MonoBehaviour
{
    [HideInInspector] public int attention;
    [SerializeField] public int maxAttention;
    [SerializeField] public int attentionUpdateDelay;
    [SerializeField] public float attentionDropBase;

    [SerializeField] public UnityEvent OnRecognised;

    [HideInInspector] public List<Enemy> watchers;
    public Invisible invisible;
    public Visible visible;
    public DroppingVisible droppingVisible;
    StateMachine stealthSM;

    void Start()
    {
        stealthSM = new StateMachine();
        invisible = new Invisible(this, stealthSM);
        visible = new Visible(this, stealthSM);
        droppingVisible = new DroppingVisible(this, stealthSM);

        stealthSM.Initialize(invisible);
    }

    public void Update()
    {
        Debug.Log($"Attention Level: {attention}, Watchers: {watchers.Count}");
        this.stealthSM.CurrentState.LogicUpdate();
    }

    public void AttentionAttracted(Enemy enemy)
    {
        foreach (var watcher in watchers)
        {
            if (Object.ReferenceEquals(watcher,enemy))
                return;
        }
        watchers.Add(enemy);
    }

    public void AttentionLost(Enemy enemy)
    {
        watchers.Remove(enemy);
    }

    public IEnumerator AttentionRaiseUpdate()
    {
        while (true)
        {
            Debug.Log("AttentionRaiseUpdate");

            foreach (var watcher in watchers)
            {
                attention += watcher.attentionRaiseValue;
            }

            yield return new WaitForSeconds(this.attentionUpdateDelay);
        }
    }

    public IEnumerator AttentionDropUpdate()
    {
        int i = 0;
        while (true)
        {
            Debug.Log("AttentionDropUpdate");
            int attentionDropValue = (int)Mathf.Ceil(Mathf.Pow(attentionDropBase, i));

            if (attention - attentionDropValue < 0)
                attention = 0;
            else
                attention -= attentionDropValue;

            yield return new WaitForSeconds(this.attentionUpdateDelay);
            i++;
        }
    }
}
