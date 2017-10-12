using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonAnimator : MonoBehaviour 
{
    private Animator animator;

    private void Start () 
    {
        animator = GetComponent<Animator>();
    }

    public void Stand() 
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Stand", true);
    }

    public void Walk() 
    {
        animator.SetBool("Stand", false);
        animator.SetBool("Walk", true);
    }

    public void Alert() 
    {
        animator.SetBool("Stand", false);
        animator.SetBool("Alert", true);
    }
}
