using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luggage : PooledObject 
{
    public BlinkDestroy Blink { get; private set; }

    private const float blinkDelay = 0.25f;
    private const float blinkRate = 0.25f;
    private const int maxBlinkCount = 6;

    private void Start() 
    {
        Blink = GetComponent<BlinkDestroy>();
        Blink.StartDelay = blinkDelay;
        Blink.Rate = blinkRate;
        Blink.MaxBlinkCount = maxBlinkCount;
    }
}
