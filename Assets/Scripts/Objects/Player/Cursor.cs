using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour 
{
    public BlinkStun Blink { get; private set; }
    public Vector3 MuzzlePosition { get; private set; } 

    private const int moveSpeed = 4700;
    private const float muzzleOffset = 0.75f;
    private const float blinkDelay = 0.3f;
    private const float blinkRate = 0.5f;
    private const int maxBlinkCount = 6;

    private Move move;

    private void Awake() 
    {
        EventManager.Instance.BulletEnd += OnBulletEnd;
    }

    private void OnEnable() 
    {
        if (move) 
        {
            EnableMovement();
        }
    }

    private void Start() 
    {
        Blink = GetComponent<BlinkStun>();
        Blink.StartDelay = blinkDelay;
        Blink.Rate = blinkRate;
        Blink.MaxBlinkCount = maxBlinkCount;

        move = GetComponent<Move>();
        move.Speed = moveSpeed;

        EnableMovement();
    }

    private void Update() 
    {
        MuzzlePosition = transform.position + muzzleOffset * transform.right;
    }

    private void OnDisable() 
    {
        DisableMovement();
    }

    private void OnDestroy() 
    {
        EventManager.Instance.BulletEnd -= OnBulletEnd;
    }

    private void EnableMovement() 
    {
        EventManager.Instance.WKeyDown += move.MoveUp;
        EventManager.Instance.AKeyDown += move.MoveLeft;
        EventManager.Instance.SKeyDown += move.MoveDown;
        EventManager.Instance.DKeyDown += move.MoveRight;
        EventManager.Instance.WKeyUp += move.MoveDown;
        EventManager.Instance.AKeyUp += move.MoveRight;
        EventManager.Instance.SKeyUp += move.MoveUp;
    }

    private void DisableMovement() 
    {
        EventManager.Instance.WKeyDown -= move.MoveUp;
        EventManager.Instance.AKeyDown -= move.MoveLeft;
        EventManager.Instance.SKeyDown -= move.MoveDown;
        EventManager.Instance.DKeyDown -= move.MoveRight;
        EventManager.Instance.WKeyUp -= move.MoveDown;
        EventManager.Instance.AKeyUp -= move.MoveRight;
        EventManager.Instance.SKeyUp -= move.MoveUp;
        EventManager.Instance.DKeyUp -= move.MoveLeft;
    }

    private void OnBulletEnd() 
    {
        gameObject.SetActive(false);
    }
}
