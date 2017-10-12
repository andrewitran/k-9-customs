using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour 
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField]
    private Cursor cursor;
    [SerializeField]
    private Shield shield;

    private Vector3 spawnPosition = new Vector3(0.42f, -0.83f, -9);
    private Quaternion spawnRotation = Quaternion.identity;

    private PlayerManager() 
    {

    }

    private void Awake() 
    {
        Instance = this;
    }

    private void Start() 
    {
        EventManager.Instance.BulletStart += OnBulletStart;
        EventManager.Instance.BulletEnd += OnBulletEnd;
    }

    private void OnDisable() 
    {
        EventManager.Instance.BulletStart -= OnBulletStart;
        EventManager.Instance.BulletEnd -= OnBulletEnd;

        if (StatsTracker.Instance.BulletInProgress) 
        {
            EventManager.Instance.RightMouseDown -= OnRightMouseDown;
            EventManager.Instance.RightMouseUp -= OnRightMouseUp;
        }
    }

    private void OnBulletStart() 
    {
        EnableCursor();

        EventManager.Instance.RightMouseDown += OnRightMouseDown;
        EventManager.Instance.RightMouseUp += OnRightMouseUp;
    }

    private void OnBulletEnd() 
    {
        EventManager.Instance.RightMouseDown -= OnRightMouseDown;
        EventManager.Instance.RightMouseUp -= OnRightMouseUp;

        DisableCursor();
        DisableShield();
    }

    private void OnRightMouseDown() 
    {
        if (!cursor.Blink.IsStunned) 
        {
            EnableShield();
        }
    }

    private void OnRightMouseUp() 
    {
        if (!cursor.Blink.IsStunned) 
        {
            DisableShield();
        }
    }

    private void EnableCursor() 
    {
        cursor.transform.position = spawnPosition;
        cursor.transform.rotation = spawnRotation;

        cursor.gameObject.SetActive(true);
    }

    private void EnableShield() 
    {
        shield.gameObject.SetActive(true);
    }

    private void DisableCursor() 
    {
        cursor.gameObject.SetActive(false);
    }

    private void DisableShield() 
    {
        shield.gameObject.SetActive(false);
    }
}
