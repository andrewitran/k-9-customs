using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkStun : BlinkEffect 
{
    public bool IsStunned { get; private set; }

    private void OnEnable() 
    {
        StopBlinking();

        if (sprite) {
            sprite.enabled = true;
        }

        if (text) {
            text.enabled = true;
        }
    }

    public override void StartBlinking() 
    {
        IsStunned = true;
        InvokeRepeating("BlinkProcess", StartDelay, Rate);
    }

    protected override void StopBlinking() 
    {
        IsStunned = false;
        CancelInvoke("BlinkProcess"); 
    }
}
