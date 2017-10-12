using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : LaunchedObject 
{
    private const float blinkDelay = 0.25f;
    private const float blinkRate = 0.25f;
    private const int maxBlinkCount = 6;

    private BlinkDestroy blink;

    public Item() 
    {
        launchSpeed = 125;
    }

    protected override void OnEnable() 
    {
        launchDirection = transform.up;

        EventManager.Instance.Caught += DestroyItem;
        EventManager.Instance.PassCorrect += DestroyItem;
        EventManager.Instance.PassWrong += DestroyItem;
        EventManager.Instance.TimeOut += DestroyItem;

        base.OnEnable();
    }

    protected override void Start() 
    {
        blink = GetComponent<BlinkDestroy>();
        blink.StartDelay = blinkDelay;
        blink.Rate = blinkRate;
        blink.MaxBlinkCount = maxBlinkCount;

        base.Start();
    }

    private void OnDisable() 
    {
        EventManager.Instance.Caught -= DestroyItem;
        EventManager.Instance.PassCorrect -= DestroyItem;
        EventManager.Instance.PassWrong -= DestroyItem;
        EventManager.Instance.TimeOut -= DestroyItem;
    }

    private void DestroyItem() 
    {
        blink.StartBlinking();
    }
}
