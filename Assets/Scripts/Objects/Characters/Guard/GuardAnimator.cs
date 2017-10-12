using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAnimator : MonoBehaviour 
{
    private Animator animator;

    private void Start() 
    {
        animator = GetComponent<Animator>();

        EventManager.Instance.BulletLoad += OnBulletLoad;
        EventManager.Instance.AlertCorrect += OnAlertCorrect;
        EventManager.Instance.AlertWrong += OnAlertWrong;
        EventManager.Instance.PassCorrect += OnPassCorrect;
        EventManager.Instance.PassWrong += OnPassWrong;
        EventManager.Instance.TimeOut += OnTimeOut;
        EventManager.Instance.GameOverRegular += OnGameOverRegular;
        EventManager.Instance.GameOverTimeOut += OnGameOverTimeOut;

        Invoke("Idle", 0.01f);
    }

    private void OnDisable() 
    {
        EventManager.Instance.BulletLoad -= OnBulletLoad;
        EventManager.Instance.AlertCorrect -= OnAlertCorrect;
        EventManager.Instance.AlertWrong -= OnAlertWrong;
        EventManager.Instance.PassCorrect -= OnPassCorrect;
        EventManager.Instance.PassWrong -= OnPassWrong;
        EventManager.Instance.TimeOut -= OnTimeOut;
        EventManager.Instance.GameOverRegular -= OnGameOverRegular;
        EventManager.Instance.GameOverTimeOut -= OnGameOverTimeOut;
    }

    private void OnAlertCorrect() 
    {
        Bark();
        Invoke("Call", 1.75f);
        Invoke("Pet", 2.75f);
        Invoke("Idle", 4);
    }

    private void OnAlertWrong() 
    {
        Bark();
        Invoke("Whimper", 1.75f);
        Invoke("Idle", 2.75f);
    }

    private void OnPassCorrect() 
    {
        Wave();
        Invoke("Idle", 1.75f);
    }

    private void OnPassWrong() 
    {
        Wave();
        Invoke("Watch", 0.75f);
        Invoke("Idle", 2.5f);
    }

    private void OnTimeOut() 
    {
        Invoke("Whimper", 0.35f);
        Invoke("Idle", 1.85f);
    }

    private void OnGameOverRegular() 
    {
        Invoke("Whimper", 2);
    }

    private void OnGameOverTimeOut() 
    {
        Invoke("Whimper", 2.5f);
    }

    private void OnBulletLoad() 
    {
        Sniff();
    }

    private void Idle() 
    {
        animator.SetBool("Pet", false);
        animator.SetBool("Whimper", false);
        animator.SetBool("Wave", false);
        animator.SetBool("Watch", false);
        animator.SetBool("Idle", true);

        EventManager.Instance.OnGuardIdle();
    }

    private void Sniff() 
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Sniff", true);

        EventManager.Instance.OnGuardSniff();
    }

    private void Bark() 
    {
        animator.SetBool("Sniff", false);
        animator.SetBool("Bark", true);

        EventManager.Instance.OnGuardBark();
    }

    private void Call() 
    {
        animator.SetBool("Bark", false);
        animator.SetBool("Call", true);

        EventManager.Instance.OnGuardCall();
    }

    private void Pet() 
    {
        animator.SetBool("Call", false);
        animator.SetBool("Pet", true);
    }

    private void Whimper() 
    {
        animator.SetBool("Bark", false);
        animator.SetBool("Sniff", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Whimper", true);

        EventManager.Instance.OnGuardWhimper();
    } 

    private void Wave() 
    {
        animator.SetBool("Sniff", false);
        animator.SetBool("Wave", true);

        EventManager.Instance.OnGuardWave();
    }

    private void Watch() 
    {
        animator.SetBool("Wave", false);
        animator.SetBool("Watch", true);

        EventManager.Instance.OnGuardWatch();
    }
}
