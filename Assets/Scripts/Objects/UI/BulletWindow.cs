using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWindow : MonoBehaviour 
{
    private Animator animator;

    private void OnEnable() 
    {
        if (animator) 
        {
            animator.Play("Open");
        }

        EventManager.Instance.BulletEnd += OnBulletEnd;
    }

    private void Start() 
    {
        animator = GetComponent<Animator>(); 
        animator.Play("Open");
    }

    private void OnDisable() 
    {
        EventManager.Instance.BulletEnd -= OnBulletEnd;
    } 

    private void OnBulletEnd() 
    {
        animator.Play("Close");
        StartCoroutine("WaitToDisable");
    }

    private IEnumerator WaitToDisable() 
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        
        gameObject.SetActive(false); 
    }
}
