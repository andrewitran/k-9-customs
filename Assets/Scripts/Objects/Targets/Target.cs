using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : PooledObject 
{
    public int health;

    private void OnEnable() 
    {
        EventManager.Instance.BulletEnd += OnBulletEnd;
    }

    protected virtual void OnDisable() 
    {
        EventManager.Instance.BulletEnd -= OnBulletEnd;
    }

    public void Damage(int damage) 
    {
        health -= damage;

        if (health <= 0) 
        {
            ReturnToPool();
        }
    }

    private void OnBulletEnd() 
    {
        ReturnToPool();
    }
}
