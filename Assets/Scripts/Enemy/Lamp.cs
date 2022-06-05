using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : EnemyBehaviour
{
    [Header("Lamp Settings")]
    [SerializeField] public float maxAttetionRaise;

    public override float AttentionRaise(float attention)
    {
        if (attentionRaiseValue + attention > maxAttetionRaise)
            return 0;
        else
            return attentionRaiseValue;
    }
}
