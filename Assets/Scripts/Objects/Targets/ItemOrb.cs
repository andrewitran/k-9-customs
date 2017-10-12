using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOrb : Target 
{
    private bool isQuitting;

    public ItemOrb() 
    {
        health = 6;
    }

    private void OnApplicationQuit() 
    {
        isQuitting = true;
    }

    protected override void OnDisable() 
    {
        base.OnDisable();

        if (StatsTracker.Instance.BulletInProgress && !isQuitting) 
        {
            --StatsTracker.Instance.Items;
            EventManager.Instance.OnItemOrbDestroyed();
        }
    }
}
