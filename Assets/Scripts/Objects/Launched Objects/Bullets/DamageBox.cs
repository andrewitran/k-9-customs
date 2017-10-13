using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox : PooledObject 
{
    private const float orbTolerance = 0.3f;

    private List<Target> targets = new List<Target>();
    private bool hasDestroyedACloud;

    private void OnEnable() 
    {
        targets.Clear();
        hasDestroyedACloud = false;

        EventManager.Instance.BulletEnd += OnBulletEnd;
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Cloud") 
        {
            targets.Add(collision.gameObject.GetComponent<Target>());
        } 
        else if (collision.gameObject.tag == "Orb") 
        {
            if (OrbIsInRange(collision.transform)) 
            {
                targets.Add(collision.gameObject.GetComponent<Target>());
            }
        }
    }

    private void OnDisable() 
    {
        EventManager.Instance.BulletEnd -= OnBulletEnd;
    }

    public void Damage(int damage) 
    {
        for (int i = 0; i < targets.Count; ++i) 
        {
            if (targets[i].gameObject.activeInHierarchy) 
            {
                targets[i].Damage(damage);
            }

            if (targets[i].gameObject.tag == "Cloud" && !hasDestroyedACloud && targets[i].health <= 0) 
            {
                hasDestroyedACloud = true;
                EventManager.Instance.OnCloudDestroyed();
            }
        }

        ReturnToPool();
    }

    private bool OrbIsInRange(Transform orb) 
    {
        return Mathf.Abs(orb.position.x - transform.position.x) <= orbTolerance && Mathf.Abs(orb.position.y - transform.position.y) <= orbTolerance;
    }

    private void OnBulletEnd() 
    {
        ReturnToPool();
    }
}
