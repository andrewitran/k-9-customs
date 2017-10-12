using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubtfulSign : MonoBehaviour 
{
    private Animator animator;

    private void OnEnable() 
    {
        if (animator) 
        {
            animator.Play("Rock");
            StartCoroutine("WaitToDisable");
        }
    }

    private void Start() 
    {
        animator = GetComponent<Animator>();        

        animator.Play("Rock");
        StartCoroutine("WaitToDisable");
    }

    private IEnumerator WaitToDisable() 
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        gameObject.SetActive(false);
    }
}

