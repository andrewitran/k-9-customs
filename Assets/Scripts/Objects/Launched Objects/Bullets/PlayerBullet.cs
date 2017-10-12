using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : LaunchedObject 
{
    private const float centerTolerance = 0.5f;

    public DamageBox damageBox { private get; set; } 

    protected int damage;

    public PlayerBullet() 
    {
        launchSpeed = 1150;
    }

    protected override void OnEnable() 
    {
        launchDirection = transform.right;

        EventManager.Instance.BulletEnd += OnBulletEnd;

        base.OnEnable();
    }

    private void Update() 
    {
        if (BulletHasReachedDestination()) 
        {
            damageBox.Damage(damage);
            ReturnToPool();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Window Border") 
        {
            damageBox.ReturnToPool();
            ReturnToPool();
        }
    }

    private void OnDisable() 
    {
        EventManager.Instance.BulletEnd -= OnBulletEnd;
    }

    private bool BulletHasReachedDestination() 
    {
        return Mathf.Pow(damageBox.transform.position.x - transform.position.x, 2) + Mathf.Pow(damageBox.transform.position.y - transform.position.y, 2) <= Mathf.Pow(centerTolerance, 2);
    }

    private void OnBulletEnd() 
    {
        ReturnToPool();
    }
}
