using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour 
{
    public static ItemGenerator Instance { get; private set; }

    private const int maxItems = 3;

    [SerializeField]
    private GameObject[] contrabandItemPrefab;
    [SerializeField]
    private GameObject[] legalItemPrefab;
    [SerializeField]
    private ObjectPool contrabandItemPool;
    [SerializeField]
    private ObjectPool legalItemPool;

    private Vector3 spawnPosition = new Vector3(-0.64f, -2.631f, -9);
    private Quaternion[] spawnRotation = new Quaternion[maxItems];
    private int[] contrabandChance = new int[maxItems];
    private int itemsSpawned;

    private ItemGenerator() 
    {
        spawnRotation[0] = Quaternion.identity;
        spawnRotation[1] = Quaternion.Euler(0, 0, 5);
        spawnRotation[2] = Quaternion.Euler(0, 0, -5);

        contrabandChance[0] = 0;
        contrabandChance[1] = 65;
        contrabandChance[2] = 85;
    }

    private void Awake() 
    {
        Instance = this;
    }

    private void Start() 
    {
        CreateItems();

        EventManager.Instance.BulletStart += OnBulletStart;
        EventManager.Instance.ItemOrbDestroyed += OnItemOrbDestroyed;
        EventManager.Instance.AlertCorrect += OnAlert;
        EventManager.Instance.AlertWrong += OnAlert;
    }

    private void OnDisable() 
    {
        EventManager.Instance.BulletStart -= OnBulletStart;
        EventManager.Instance.ItemOrbDestroyed -= OnItemOrbDestroyed;
        EventManager.Instance.AlertCorrect -= OnAlert;
        EventManager.Instance.AlertWrong -= OnAlert;
    }

    private void OnBulletStart() 
    {
        Configure();
    }

    private void OnItemOrbDestroyed() 
    {
        GetItem();
    }

    private void OnAlert() 
    {
        while (StatsTracker.Instance.Items > 0) 
        {
            --StatsTracker.Instance.Items;
            GetItem();
        }
    }

    private void CreateItems() 
    {
        contrabandItemPool.AddToPool(contrabandItemPrefab, maxItems);
        legalItemPool.AddToPool(legalItemPrefab, maxItems); 
    }

    private void Configure() 
    {
        itemsSpawned = 0;
        int randomChance = Random.Range(0, 100);
        int randomCheck = Random.Range(0, 100);

        if (randomChance >= randomCheck) 
        {
            StatsTracker.Instance.HasContraband = true;
            StatsTracker.Instance.HasContrabandRemaining = true;
        } 
        else 
        {
            StatsTracker.Instance.HasContraband = false;
            StatsTracker.Instance.HasContrabandRemaining = false;
        }
    }

    private void GetItem() 
    {
        ObjectPool objectPool;

        if (StatsTracker.Instance.HasContrabandRemaining) 
        {
            int randomChance = Random.Range(0, 100);

            if (randomChance >= contrabandChance[StatsTracker.Instance.Items]) 
            {
                objectPool = contrabandItemPool;
                StatsTracker.Instance.HasContrabandRemaining = false;
            } 
            else 
            {
                objectPool = legalItemPool;
            }
        } 
        else 
        {
            objectPool = legalItemPool;
        }

        objectPool.GetRandomObject(spawnPosition, spawnRotation[itemsSpawned]);
        ++itemsSpawned;
    }
}
