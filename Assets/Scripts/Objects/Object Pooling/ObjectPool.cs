using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour 
{
    [SerializeField]
    private GameObject[] prefab; 

    private List<GameObject> availableObjects = new List<GameObject>();

    public void AddToPool(GameObject prefab) 
    {
        GameObject obj = Instantiate(prefab, transform);

        PoolObject(obj);
    }

    public void AddToPool(GameObject prefab, int amount) 
    {
        for (int i = 0; i < amount; ++i) 
        {
            AddToPool(prefab);
        }
    }

    public void AddToPool(GameObject prefab, Vector3 position) 
    {
        GameObject obj = Instantiate(prefab, position, Quaternion.identity, transform); 

        PoolObject(obj);
    }

    public void AddToPool(GameObject[] prefabs) 
    {
        for (int i = 0; i < prefabs.Length; ++i) 
        {
            AddToPool(prefabs[i]);
        }
    }

    public void AddToPool(GameObject[] prefabs, int amount) 
    {
        for (int i = 0; i < prefabs.Length; ++i) 
        {
            AddToPool(prefabs[i], amount);
        }
    }

    public GameObject GetObject(Vector3 position, Quaternion rotation) 
    {
        int lastAvailableIndex = availableObjects.Count - 1;

        return FindObject(lastAvailableIndex, 0, position, rotation);
    }

    public GameObject GetRandomObject(Vector3 position, Quaternion rotation) 
    {
        int prefabIndex = Random.Range(0, prefab.Length);
        int randomIndex = Random.Range(0, availableObjects.Count);

        return FindObject(randomIndex, prefabIndex, position, rotation);
    }

    public void ReturnObject(GameObject obj) 
    {
        obj.SetActive(false);
        availableObjects.Add(obj);
    }

    private void PoolObject(GameObject obj) 
    {
        PooledObject pooledObj = obj.GetComponent<PooledObject>();

        pooledObj.Pool = this;
        pooledObj.ReturnToPool();
    }

    private GameObject FindObject(int listIndex, int prefabIndex, Vector3 position, Quaternion rotation) 
    {
        int lastAvailableIndex = availableObjects.Count - 1;
        GameObject obj;

        if (lastAvailableIndex >= 0) 
        {
            obj = availableObjects[listIndex];
            availableObjects.RemoveAt(listIndex);
        } 
        else 
        {
            obj = Instantiate(prefab[prefabIndex], transform);
            obj.GetComponent<PooledObject>().Pool = this;
        }

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);

        return obj;
    }
}
