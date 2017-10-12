using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchedObject : PooledObject 
{
    protected Vector3 launchDirection;
    protected float launchSpeed;

    private Rigidbody2D rigid2D;

    protected virtual void OnEnable() 
    {
        Launch();
    }

    protected virtual void Start() 
    {
        rigid2D = GetComponent<Rigidbody2D>();

        Launch();
    }

    private void Launch() 
    {
        if (rigid2D) 
        {
            rigid2D.AddForce(launchDirection * launchSpeed);
        }
    }
}
