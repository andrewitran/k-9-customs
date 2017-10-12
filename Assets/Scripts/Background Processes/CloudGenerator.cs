using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour 
{
    public static CloudGenerator Instance { get; private set; }

    private const int amountCloudsX = 21;
    private const int amountCloudsY = 11;

    [SerializeField]
    private GameObject[] cloudPrefab;
    [SerializeField]
    private ObjectPool cloudPool;

    private Vector3 cloudRoot = new Vector3(-1.5f, 2.95f, -0.57f);
    private Vector3 cloudOffsetX = new Vector3(0.2f, 0, 0);
    private Vector3 cloudOffsetY = new Vector3(0, -0.2f, 0);
    private Vector3[,] cloudPosition = new Vector3[amountCloudsX, amountCloudsY];

    private CloudGenerator() 
    {
        for (int i = 0; i < amountCloudsX; ++i) 
        {
            for (int j = 0; j < amountCloudsY; ++j) 
            {
                cloudPosition[i, j] = cloudRoot + i * cloudOffsetX + j * cloudOffsetY;
            }
        }
    }

    private void Awake() 
    {
        Instance = this;
    }

    private void Start() 
    {
        CreateClouds();

        EventManager.Instance.BulletStart += OnBulletStart;
    }

    private void OnDisable() 
    {
        EventManager.Instance.BulletStart -= OnBulletStart;
    }

    private void OnBulletStart() 
    {
        GetClouds();
    }

    private void CreateClouds() 
    {
        for (int i = 0; i < amountCloudsX; ++i) 
        {
            for (int j = 0; j < amountCloudsY; ++j) 
            {
                int randomIndex = Random.Range(0, cloudPrefab.Length);

                cloudPool.AddToPool(cloudPrefab[randomIndex], cloudPosition[i, j]);
            }
        }
    }

    private void GetClouds() 
    {
        for (int i = 0; i < amountCloudsX; ++i) 
        {
            for (int j = 0; j < amountCloudsY; ++j) 
            {
                cloudPool.GetRandomObject(cloudPosition[i, j], Quaternion.identity);
            }
        }
    }
}
