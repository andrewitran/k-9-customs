using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerdictSign : MonoBehaviour 
{
    private BlinkDestroy blink;
    private float blinkRate = 0.35f; 
    private int maxBlinkCount = 5;

    private void OnEnable() 
    {
        if (blink) 
        {
            blink.StartBlinking();
        }
    }

    private void Start() 
    {
        blink = GetComponent<BlinkDestroy>();
        blink.MaxBlinkCount = maxBlinkCount;
        blink.Rate = blinkRate;
        
        blink.StartBlinking();
    }
}
