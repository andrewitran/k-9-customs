using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour 
{
    public static BulletGenerator Instance { get; private set; }

    private const int maxNormalBullets = 3;
    private const int maxWeakBullets = 2;
    private const int maxTrapBullets = 120;
    private const int normalBulletCost = 1;

    [SerializeField]
    private GameObject normalBulletPrefab;
    [SerializeField]
    private GameObject weakBulletPrefab;
    [SerializeField]
    private GameObject trapBulletPrefab;
    [SerializeField]
    private GameObject damageBoxPrefab;
    [SerializeField]
    private ObjectPool normalBulletPool;
    [SerializeField]
    private ObjectPool weakBulletPool;
    [SerializeField]
    private ObjectPool trapBulletPool;
    [SerializeField]
    private ObjectPool damageBoxPool;
    [SerializeField]
    private Cursor cursor;
    [SerializeField]
    private Shield shield;

    private bool bulletCanFire = true;

    private BulletGenerator() 
    {

    }

    private void Awake() 
    {
        Instance = this;
    }

    private void Start() 
    {
        CreateBullets(); 

        EventManager.Instance.BulletStart += OnBulletStart;
        EventManager.Instance.BulletEnd += OnBulletEnd;
    }

    private void OnDisable() 
    {
        EventManager.Instance.BulletStart += OnBulletStart;
        EventManager.Instance.BulletEnd += OnBulletEnd;
        
        if (StatsTracker.Instance.BulletInProgress) 
        {
            EventManager.Instance.LeftMouseDown -= OnLeftMouseDown;
        }
    }

    private void OnBulletStart() 
    {
        EventManager.Instance.LeftMouseDown += OnLeftMouseDown;
    }

    private void OnLeftMouseDown() 
    {
        Fire();
    }

    private void OnBulletEnd() 
    {
        EventManager.Instance.LeftMouseDown -= OnLeftMouseDown;
    }

    private void CreateBullets() 
    {
        normalBulletPool.AddToPool(normalBulletPrefab, maxNormalBullets);
        weakBulletPool.AddToPool(weakBulletPrefab, maxWeakBullets);
        trapBulletPool.AddToPool(trapBulletPrefab, maxTrapBullets);
        damageBoxPool.AddToPool(damageBoxPrefab, maxNormalBullets);
    }

    private void Fire() 
    {
        if (!cursor.Blink.IsStunned && !shield.gameObject.activeInHierarchy && bulletCanFire) 
        {
            PlayerBullet bullet;
            bulletCanFire = false;

            if (StatsTracker.Instance.Energy > 0) 
            {
                bullet = GetNormalBullet();
                bullet.damageBox = GetDamageBox();
                StatsTracker.Instance.Energy = Mathf.Clamp(StatsTracker.Instance.Energy -= normalBulletCost, 0, StatsTracker.MaxEnergy);

                EventManager.Instance.OnNormalBulletFire();

                // Used to create a 0.1 second delay before the next shot.
                Invoke("BulletWait", 0.1f);
            } 
            else 
            {
                bullet = GetWeakBullet();
                bullet.damageBox = GetDamageBox();

                EventManager.Instance.OnWeakBulletFire();

                // Used to create a 0.4 second delay before the next shot.
                Invoke("BulletWait", 0.4f);
            }
        }
    }

    private NormalBullet GetNormalBullet() 
    {
        GameObject normalBullet = normalBulletPool.GetObject(cursor.MuzzlePosition, cursor.transform.rotation);

        return normalBullet.GetComponent<NormalBullet>();
    }

    private WeakBullet GetWeakBullet() 
    {
        GameObject weakBullet = weakBulletPool.GetObject(cursor.MuzzlePosition, cursor.transform.rotation);

        return weakBullet.GetComponent<WeakBullet>();
    }

    private DamageBox GetDamageBox() 
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 colliderPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        colliderPosition.z = cursor.transform.position.z;

        GameObject damageBox = damageBoxPool.GetObject(colliderPosition, Quaternion.identity);

        return damageBox.GetComponent<DamageBox>();
    }

    private void BulletWait() 
    {
        bulletCanFire = true;
    }
}
