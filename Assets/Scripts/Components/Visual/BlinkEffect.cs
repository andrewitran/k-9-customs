using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkEffect : MonoBehaviour 
{
    public float StartDelay { get; set; }
    public float Rate { get; set; }
    public int MaxBlinkCount { get; set; }

    protected SpriteRenderer sprite;
    protected Text text;
    private int blinkCount;

    protected virtual void Start() 
    {
        sprite = GetComponent<SpriteRenderer>();
        text = GetComponent<Text>();
    }

    public virtual void StartBlinking() 
    {
        InvokeRepeating("BlinkProcess", StartDelay, Rate); 
    }

    protected virtual void StopBlinking() 
    {
        CancelInvoke("BlinkProcess");
    }

    protected void BlinkProcess() 
    {
        Blink();
        ++blinkCount;

        if (blinkCount == MaxBlinkCount) 
        {
            StopBlinking();
            blinkCount = 0;
        }
    }

    private void Blink() 
    {
        if (sprite) 
        {
            sprite.enabled = !sprite.enabled;
        }

        if (text) 
        {
            text.enabled = !text.enabled;
        }
    }
}
