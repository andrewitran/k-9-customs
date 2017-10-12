using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour 
{
    private const float blinkRate = 0.2f;
    private const int maxBlinkCount = 6;

    public BlinkEffect Blink { get; private set; }

    private void Start() 
    {
        Blink = GetComponent<BlinkEffect>();
        Blink.Rate = blinkRate;
        Blink.MaxBlinkCount = maxBlinkCount;
    }
}
