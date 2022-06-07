using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : EnemyBehaviour
{
    [Header("Lamp Settings")]
    [SerializeField] public float maxAttetionRaise;

    protected override void Start()
    {
        base.Start();

        base.attentionRaise = delegate (float attention)
        {
            if (attentionRaiseValue + attention > maxAttetionRaise)
                return Mathf.Max(0, maxAttetionRaise - attention);
            else
                return attentionRaiseValue;
        };
    }
}
