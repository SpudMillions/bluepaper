﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    [SerializeField] CanvasGroup healthGroup;

    public override Transform Select()
    {
        healthGroup.alpha = 1;
        return base.Select();
    }

    public override void DeSelect()
    {
        healthGroup.alpha = 0;
        base.DeSelect();
    }

    public override void TakeDamage(float damage)
    {
        
        base.TakeDamage(damage);
        OnHealthChanged(Health.CurrentValue);
    }
}
