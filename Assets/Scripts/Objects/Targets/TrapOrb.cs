using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapOrb : Target 
{
    private const float bulletRadius = 0.25f;
    private const float bulletSpawnZ = -0.57f;

    private ObjectPool trapBulletPool;
    private int bulletSpread = 30; 
    private bool isQuitting;

    public TrapOrb() 
    {
        health = 3;
    }

    private void Start() 
    {
        trapBulletPool = GameObject.Find("Trap Bullet Pool").GetComponent<ObjectPool>();
    }

    private void OnApplicationQuit() 
    {
        isQuitting = true;
    }

    protected override void OnDisable() 
    {
        base.OnDisable();

        if (StatsTracker.Instance.BulletInProgress && !isQuitting) 
        {
            Configure();
            Fire();  
            EventManager.Instance.OnTrapOrbDestroyed();
        }
    }

    private void Configure() 
    {
        switch (StatsTracker.Instance.Level) 
        {
            case 4:
                bulletSpread = 20;

                break;
            case 8:
                bulletSpread = 15;

                break;
        }   
    }

    private void Fire() 
    {
        for (int i = 0; i <= 180 / bulletSpread; ++i) 
        {
            float bulletOffsetX = bulletRadius * Mathf.Cos(i * bulletSpread * Mathf.PI / 180);
            float bulletOffsetY = bulletRadius * Mathf.Sin(i * bulletSpread * Mathf.PI / 180);

            Vector3 bulletSpawnPosition = new Vector3(transform.position.x + bulletOffsetX, transform.position.y + bulletOffsetY, bulletSpawnZ);
            Quaternion bulletRotation = Quaternion.Euler(0, 0, i * bulletSpread);

            // The mirrored spawn positions and rotations facilitate bullet spawn in a full circle around the orb.
            Vector3 bulletSpawnPositionMirrored = new Vector3(transform.position.x + bulletOffsetX, transform.position.y - bulletOffsetY, bulletSpawnZ);
            Quaternion bulletRotationMirrored = Quaternion.Euler(0, 0, -i * bulletSpread);

            trapBulletPool.GetObject(bulletSpawnPosition, bulletRotation).SetActive(true);

            // This if statement will prevent bullets from being doubly spawned due to mirroring at 0 and 180 degrees.
            if (bulletRotation != Quaternion.Euler(Vector3.zero) && bulletRotation != Quaternion.Euler(0, 0, 180)) 
            {
                trapBulletPool.GetObject(bulletSpawnPositionMirrored, bulletRotationMirrored);
            }
        }     
    }
}
