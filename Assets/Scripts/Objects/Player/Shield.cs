using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour 
{
    private const float energyCost = 0.05f;
    private const float energyRate = 0.01f; 

    private DrainEnergyDestroy energy;

    private void Awake() 
    {
        EventManager.Instance.BulletEnd += OnBulletEnd;
    }

    private void OnEnable() 
    {
        GameObject cursor = GameObject.Find("Cursor");

        transform.position = cursor.transform.position;
        transform.rotation = cursor.transform.rotation;

        if (energy) 
        {
            energy.StartDrain();
            EventManager.Instance.OnShieldRaised();
        }
    }

    private void Start() 
    {
        energy = GetComponent<DrainEnergyDestroy>();
        energy.Cost = energyCost;
        energy.Rate = energyRate;

        energy.StartDrain();
        EventManager.Instance.OnShieldRaised();
    }

    private void OnDisable() 
    {
        EventManager.Instance.OnShieldLowered();
    }

    private void OnDestroy() 
    {
        EventManager.Instance.BulletEnd -= OnBulletEnd;
    }

    private void OnBulletEnd() 
    {
        gameObject.SetActive(false);
    }
}
