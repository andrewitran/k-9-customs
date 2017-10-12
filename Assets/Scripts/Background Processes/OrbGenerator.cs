using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbGenerator : MonoBehaviour 
{
    public static OrbGenerator Instance { get; private set; }

    private const int amountOrbsX = 5;
    private const int amountOrbsY = 4;
    private const int maxEnergyOrbs = 3;
    private const int maxItemOrbs = 3;
    private const int maxTrapOrbs = 12;
    private const float offsetTolerance = 0.05f; 
    
    [SerializeField]
    private GameObject energyOrbPrefab;
    [SerializeField]
    private GameObject itemOrbPrefab;
    [SerializeField]
    private GameObject trapOrbPrefab;
    [SerializeField]
    private ObjectPool energyOrbPool;
    [SerializeField]
    private ObjectPool itemOrbPool;
    [SerializeField]
    private ObjectPool trapOrbPool;

    private Vector3 orbRoot = new Vector3(-1.41f, 2.713f, 0);
    private Vector3 orbOffsetX = new Vector3(0.958f, 0, 0);
    private Vector3 orbOffsetY = new Vector3(0, -0.586f, 0);
    private float maxOrbOffset;
    private int energyOrbsThisRound = 1; 
    private int itemOrbsThisRound = 1;
    private int trapOrbsThisRound = 5;
    private List<Vector3> energyOrbPositions = new List<Vector3>();
    private List<Vector3> itemOrbPositions = new List<Vector3>();
    private List<Vector3> orbPositions = new List<Vector3>();

    private OrbGenerator() 
    {
        maxOrbOffset = Mathf.Max(Mathf.Abs(orbOffsetX.x), Mathf.Abs(orbOffsetY.y)) + offsetTolerance;
        itemOrbsThisRound = 1;
    }

    private void Awake() 
    {
        Instance = this;
    }
        
    private void Start() 
    {
        CreateOrbs();

        EventManager.Instance.BulletStart += OnBulletStart;
    }

    private void OnDisable() 
    {
        EventManager.Instance.BulletStart -= OnBulletStart;
    }

    private void OnBulletStart() 
    {
        InitializeSettings();
        GetOrbs();
    }

    private void CreateOrbs() 
    {
        energyOrbPool.AddToPool(energyOrbPrefab, maxEnergyOrbs);
        itemOrbPool.AddToPool(itemOrbPrefab, maxItemOrbs);
        trapOrbPool.AddToPool(trapOrbPrefab, maxTrapOrbs);
    }

    private void InitializeSettings() 
    {
        energyOrbPositions.Clear();
        itemOrbPositions.Clear();
        orbPositions.Clear();

        for (int i = 0; i < amountOrbsX; ++i) 
        {
            for (int j = 0; j < amountOrbsY; ++j) 
            {
                energyOrbPositions.Add(orbRoot + i * orbOffsetX + j * orbOffsetY);
                itemOrbPositions.Add(orbRoot + i * orbOffsetX + j * orbOffsetY);
                orbPositions.Add(orbRoot + i * orbOffsetX + j * orbOffsetY);
            }
        }

        int previousItemOrbsThisRound = itemOrbsThisRound;

        switch (StatsTracker.Instance.Level) 
        {
            case 3:
                energyOrbsThisRound = 2;

                itemOrbsThisRound = 2;

                trapOrbsThisRound = 8;

                break;
            case 5:
                trapOrbsThisRound = 10;

                break;
            case 7:
                itemOrbsThisRound = 3;

                break;
            case 9:
                energyOrbsThisRound = 3;
                trapOrbsThisRound = 12;

                break;
        }

        StatsTracker.Instance.Items = itemOrbsThisRound;

        if (itemOrbsThisRound != previousItemOrbsThisRound) 
        {
            EventManager.Instance.OnMaxItemsIncrease();
        }
    }

    private void GetOrbs() 
    {
        int energyOrbCount = 0;
        int itemOrbCount = 0;
        int trapOrbCount = 0;

        while (energyOrbCount != energyOrbsThisRound) 
        {
            GetEnergyOrb(); 
            ++energyOrbCount;
        }

        while (itemOrbCount != itemOrbsThisRound) 
        {
            GetItemOrb();
            ++itemOrbCount;
        }

        while (trapOrbCount != trapOrbsThisRound) 
        {
            GetTrapOrb();
            ++trapOrbCount;
        }
    }

    private void GetEnergyOrb() 
    {
        int randomPosition = Random.Range(0, energyOrbPositions.Count);

        GameObject obj = energyOrbPool.GetObject(energyOrbPositions[randomPosition], Quaternion.identity);

        for (int i = 0; i < orbPositions.Count; ++i) 
        {
            if (orbPositions[i] == obj.transform.position) 
            {
                orbPositions.RemoveAt(i);

                break;
            }
        }

        for (int i = 0; i < itemOrbPositions.Count; ++i) 
        {
            if (itemOrbPositions[i] == obj.transform.position) 
            {
                itemOrbPositions.RemoveAt(i);
                
                break;
            }
        }

        for (int i = 0; i < energyOrbPositions.Count; ++i) 
        {
            // Prevents too many EnergyOrbs from being spawned right next to each other.
            if ((energyOrbPositions[i] - obj.transform.position).magnitude <= maxOrbOffset) 
            {
                energyOrbPositions.RemoveAt(i); 

                i = 0;
            }
        }
    }

    private void GetItemOrb() 
    {
        int randomPosition = Random.Range(0, itemOrbPositions.Count);

        GameObject obj = itemOrbPool.GetObject(itemOrbPositions[randomPosition], Quaternion.identity);

        for (int i = 0; i < orbPositions.Count; ++i) 
        {
            if (orbPositions[i] == obj.transform.position) 
            {
                orbPositions.RemoveAt(i);

                break;
            }
        }

        for (int i = 0; i < energyOrbPositions.Count; ++i) 
        {
            if (energyOrbPositions[i] == obj.transform.position) 
            {
                energyOrbPositions.RemoveAt(i);

                break;
            }
        }

        for (int i = 0; i < itemOrbPositions.Count; ++i) 
        {
            // Prevents too many ItemOrbs from being spawned right next to each other.
            if ((itemOrbPositions[i] - obj.transform.position).magnitude <= maxOrbOffset) 
            {
                itemOrbPositions.RemoveAt(i); 

                i = 0;
            }
        }
    }

    private void GetTrapOrb() 
    {
        int randomPosition = Random.Range(0, orbPositions.Count);

        GameObject obj = trapOrbPool.GetObject(orbPositions[randomPosition], Quaternion.identity);

        for (int i = 0; i < energyOrbPositions.Count; ++i) 
        {
            if (energyOrbPositions[i] == obj.transform.position) 
            {
                energyOrbPositions.RemoveAt(i);

                break;
            }
        }

        for (int i = 0; i < itemOrbPositions.Count; ++i) 
        {
            if (itemOrbPositions[i] == obj.transform.position) 
            {
                itemOrbPositions.RemoveAt(i);

                break;
            }
        }

        orbPositions.RemoveAt(randomPosition);
    }
}
