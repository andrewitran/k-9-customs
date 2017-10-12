using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainEnergyDestroy : MonoBehaviour 
{
    public float Cost { private get; set; } 
    public float Rate { private get; set; } 

    private void OnDisable() 
    {
        CancelInvoke("DrainProcess");
    }

    public void StartDrain() 
    {
        InvokeRepeating("DrainProcess", 0, Rate);
    }

    private void DrainProcess() 
    {
        StatsTracker.Instance.Energy = Mathf.Clamp(StatsTracker.Instance.Energy -= Cost, 0, StatsTracker.MaxEnergy);

        if (StatsTracker.Instance.Energy == 0) 
        {
            gameObject.SetActive(false);
        }
    }
}
