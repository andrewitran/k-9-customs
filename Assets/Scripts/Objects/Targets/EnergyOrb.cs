using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyOrb : Target 
{
    private bool isQuitting;

    public EnergyOrb() 
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
            StatsTracker.Instance.Energy = StatsTracker.MaxEnergy;
            EventManager.Instance.OnEnergyOrbDestroyed();
        }
    }
}
