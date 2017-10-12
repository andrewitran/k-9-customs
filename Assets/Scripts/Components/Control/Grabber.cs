using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour 
{
    public Transform HeldObj { get; private set; }
    public Vector3 HoldOffset { private get; set; }
    public Quaternion HoldRotation { private get; set; }
    public int SortingOffset { private get; set; }
    public bool SpriteWillFlip { private get; set; }

    private SpriteRenderer sprite;
    private SpriteRenderer heldSprite;
    private int initialSortingOrder;
    private Transform initialPool;

    private void Start() 
    {
        sprite = GetComponent<SpriteRenderer>(); 
    }

    public void Grab(Transform heldObj) 
    {
        if (!HeldObj) 
        {
            HeldObj = heldObj;
            heldSprite = HeldObj.GetComponent<SpriteRenderer>();

            initialPool = HeldObj.parent;
            initialSortingOrder = heldSprite.sortingOrder;

            HeldObj.position = transform.position + HoldOffset;
            HeldObj.rotation = HoldRotation;
            HeldObj.parent = transform;
            heldSprite.sortingOrder = sprite.sortingOrder + SortingOffset;
            heldSprite.flipX = SpriteWillFlip;
        }
    }

    public void Drop() 
    {
        if (HeldObj) 
        {
            heldSprite.sortingOrder = initialSortingOrder;
            heldSprite.flipX = false;
            HeldObj.parent = initialPool;

            initialPool = null;
            heldSprite = null;
            HeldObj = null;
        }
    }

    public void Break() 
    {
        if (HeldObj) 
        {
            PooledObject pooledObj = HeldObj.GetComponent<PooledObject>();

            if (pooledObj) 
            {
                pooledObj.ReturnToPool();
            }

            Drop();
        }
    }
}
