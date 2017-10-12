using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkDestroy : BlinkEffect 
{
    private PooledObject pooledObject;
    
    protected override void Start() 
    {
        pooledObject = GetComponent<PooledObject>();

        base.Start();
    }

    protected override void StopBlinking() 
    {
        CancelInvoke("BlinkProcess");

        if (pooledObject) 
        {
            pooledObject.ReturnToPool();
        } 
        else 
        {
            gameObject.SetActive(false);
        }
    }
}
