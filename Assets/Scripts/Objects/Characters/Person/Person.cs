using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : PooledObject 
{
    private const int heldSortingOffset = -1;

    public PersonAnimator personAnimator { get; private set; }
    public MoveTo move { get; private set; }
    public Grabber grabber { get; private set; }

    private Vector3 holdOffset = new Vector3(-0.61f, -0.59f, 0); 

    private void Start() 
    {
        personAnimator = GetComponent<PersonAnimator>();
        move = GetComponent<MoveTo>();

        grabber = GetComponent<Grabber>();
        grabber.HoldOffset = holdOffset;
        grabber.SortingOffset = heldSortingOffset;
    }

    private void OnEnable() 
    {
        if (personAnimator) 
        {
            personAnimator.Stand();
        }
    }
}
