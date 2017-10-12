using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour 
{
    private const int heldSortingOffset = 1;
    private const bool heldSpriteWillFlip = true;

    public MoveTo move { get; private set; }
    public Grabber grabber { get; private set; }

    private Vector3 holdOffset = new Vector3(-0.2f, 0.8f, 0);
    private Quaternion holdRotation = Quaternion.Euler(0, 0, 90);

    private void Start() 
    {
        move = GetComponent<MoveTo>();

        grabber = GetComponent<Grabber>();
        grabber.HoldOffset = holdOffset;
        grabber.HoldRotation = holdRotation;
        grabber.SortingOffset = heldSortingOffset;
        grabber.SpriteWillFlip = heldSpriteWillFlip;
    }
}
