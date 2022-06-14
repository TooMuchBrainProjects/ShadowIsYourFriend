using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttentionLevel : MonoBehaviour
{
    [Header("UI Settings")]
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    [Header("Attention Settings")]
    [HideInInspector] public StealthMaster stealthMaster;

    public void Start()
    {
        stealthMaster = StealthMaster.Instance;

        // anfangs 100 Leben und grün
        slider.value = 100;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetAttention()
    {
        float currentAttentionPercent = stealthMaster.attention / stealthMaster.maxAttention * 100;

        // invert
        currentAttentionPercent = 100 - currentAttentionPercent;

        // set slider value
        slider.value = currentAttentionPercent;

        // set slider color
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
