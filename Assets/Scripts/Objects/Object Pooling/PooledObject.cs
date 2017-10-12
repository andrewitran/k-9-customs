using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour 
{
    public ObjectPool Pool { get; set; }

    public void ReturnToPool() 
    {
        if (gameObject.activeInHierarchy) 
        {
            if (Pool) 
            {
                Pool.ReturnObject(gameObject);
            } 
            else 
            {
                Destroy(gameObject);
            }
        }
    }
}
