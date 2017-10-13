using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBullet : LaunchedObject 
{
    public TrapBullet() 
    {
        launchSpeed = 125;
    }

    protected override void OnEnable() 
    {
        launchDirection = transform.right;

        if (StatsTracker.Instance.Level >= 24 && launchSpeed != 175)
        {
            launchSpeed = 175;
        }

        EventManager.Instance.BulletEnd += OnBulletEnd;

        base.OnEnable();
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Window Border") 
        {
            ReturnToPool();
        } 
        else if (collision.gameObject.name == "Shield") 
        {
            EventManager.Instance.OnShieldHit();
            ReturnToPool();
        } 
        else if (collision.gameObject.name == "Cursor") 
        {
            BlinkStun blink = collision.gameObject.GetComponent<BlinkStun>();

            if (!blink.IsStunned) 
            { 
                blink.StartBlinking();
                EventManager.Instance.OnCursorHit();
            }

            ReturnToPool();
        }
    }

    private void OnDisable() 
    {
        EventManager.Instance.BulletEnd -= OnBulletEnd;
    }

    private void OnBulletEnd() 
    {
        ReturnToPool();
    }
}
