﻿using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ShieldControl : MonoBehaviour
{
    [SerializeField, Range(0.0f, 10.0f)] private float shieldIntegrity;
    [SerializeField] private float shieldStrength;
    [SerializeField] private Animator shieldDepletedMessage;

    private Material _shieldMaterial;
    
    private readonly string shieldLifeMaterialUniform = "ShieldPower";

    public float ShieldIntegrity => shieldIntegrity;

    public float ShieldStrength => shieldStrength;


    private void Awake()
    {
        var myRenderer = GetComponent<Renderer>();
        _shieldMaterial = myRenderer.material;
    }

    public void IncrementShieldStrength(float incrementBy)
    {
        shieldStrength += incrementBy;
    }

    public void TakeDamage(float rawDamage)
    {
        var afterDamage = ShieldIntegrity - Math.Max(rawDamage - ShieldStrength, 0.0f);
        shieldIntegrity = Mathf.Clamp(afterDamage, 0.0f, 10.0f);
        _shieldMaterial.SetFloat(shieldLifeMaterialUniform, NormalizedIntegrity());

        if (Math.Abs(ShieldIntegrity) < 0.0001f)
        {
            ShieldIsOff();
        }
    }

    public void RestoreShield()
    {
        DOTween.To(() => _shieldMaterial.GetFloat(shieldLifeMaterialUniform),
            value => _shieldMaterial.SetFloat(shieldLifeMaterialUniform, value),
            1.0f, 2.0f).OnComplete(()=> {
            
                shieldIntegrity = 10.0f;
                ShieldIsOn();
                }
            );
    }

    private void ShieldIsOn()
    {
        GameLogic.Instance.MineControl.RestoreHarvesting();
    }
    
    private void ShieldIsOff()
    {
        GameLogic.Instance.MineControl.StopHarvesting();
        shieldDepletedMessage.SetTrigger("ShieldDepleted");
    }

    public float NormalizedIntegrity()
    {
        return ShieldIntegrity / 10.0f;
    }
}
