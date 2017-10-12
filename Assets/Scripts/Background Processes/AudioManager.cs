using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour 
{
    public static AudioManager Instance { get; private set; }

    // Game music.
    [SerializeField]
    private AudioSource mainTheme;

    // Game over jingle.
    [SerializeField]
    private AudioClip gameOverTheme;

    // Bullet hell sounds.
    [SerializeField]
    private AudioSource cursorHit;
    [SerializeField]
    private AudioSource shield;
    [SerializeField]
    private AudioClip shieldHit;
    [SerializeField]
    private AudioClip normalBulletShot;
    [SerializeField]
    private AudioClip weakBulletShot;
    [SerializeField]
    private AudioClip generalOrbDestroyed;
    [SerializeField]
    private AudioClip trapOrbDestroyed;
    [SerializeField]
    private AudioClip cloudDestroyed;

    // Verdict sounds.
    [SerializeField]
    private AudioClip correct;
    [SerializeField]
    private AudioClip wrong;
    [SerializeField]
    private AudioClip doubtful;

    // Dog sounds.
    [SerializeField]
    private AudioClip dogBark;
    [SerializeField]
    private AudioClip dogCurious;
    [SerializeField]
    private AudioClip dogGrowl;
    [SerializeField]
    private AudioClip dogPant;
    [SerializeField]
    private AudioClip dogSniff;
    [SerializeField]
    private AudioClip dogWhine;

    // Officer sounds.
    [SerializeField]
    private AudioClip officerAngry;
    [SerializeField]
    private AudioClip officerCall;
    [SerializeField]
    private AudioClip officerHmm;
    [SerializeField]
    private AudioClip officerMoveAlong;

    // Clip audio source.
    private AudioSource clipSource;

    private AudioManager() 
    {

    }

    private void Awake() 
    {
        Instance = this;
    }

    private void Start() 
    {
        clipSource = GetComponent<AudioSource>();

        mainTheme.loop = true;
        shield.loop = true;

        PlayMainTheme();

        EventManager.Instance.GuardIdle += OnGuardIdle;
        EventManager.Instance.GuardSniff += OnGuardSniff;
        EventManager.Instance.GuardBark += OnGuardBark;
        EventManager.Instance.GuardCall += OnGuardCall;
        EventManager.Instance.GuardWhimper += OnGuardWhimper;
        EventManager.Instance.GuardWave += OnGuardWave;
        EventManager.Instance.GuardWatch += OnGuardWatch;
        EventManager.Instance.CursorHit += OnCursorHit;
        EventManager.Instance.ShieldRaised += OnShieldRaised;
        EventManager.Instance.ShieldHit += OnShieldHit;
        EventManager.Instance.ShieldLowered += OnShieldLowered;
        EventManager.Instance.NormalBulletFire += OnNormalBulletFire;
        EventManager.Instance.WeakBulletFire += OnWeakBulletFire;
        EventManager.Instance.EnergyOrbDestroyed += OnEnergyOrbDestroyed;
        EventManager.Instance.ItemOrbDestroyed += OnItemOrbDestroyed;
        EventManager.Instance.TrapOrbDestroyed += OnTrapOrbDestroyed;
        EventManager.Instance.CloudDestroyed += OnCloudDestroyed;
        EventManager.Instance.BulletEnd += OnBulletEnd;
        EventManager.Instance.AlertCorrect+= OnAlertCorrect;
        EventManager.Instance.AlertWrong += OnAlertWrong;
        EventManager.Instance.PassCorrect+= OnPassCorrect;
        EventManager.Instance.PassWrong += OnPassWrong;
        EventManager.Instance.TimeOut += OnTimeOut;
        EventManager.Instance.GameOverRegular += OnGameOverRegular;
        EventManager.Instance.GameOverTimeOut += OnGameOverTimeOut;
    }

    private void OnDisable() 
    {
        EventManager.Instance.GuardIdle -= OnGuardIdle;
        EventManager.Instance.GuardSniff -= OnGuardSniff;
        EventManager.Instance.GuardBark -= OnGuardBark;
        EventManager.Instance.GuardCall -= OnGuardCall;
        EventManager.Instance.GuardWhimper -= OnGuardWhimper;
        EventManager.Instance.GuardWave -= OnGuardWave;
        EventManager.Instance.GuardWatch -= OnGuardWatch;
        EventManager.Instance.CursorHit -= OnCursorHit;
        EventManager.Instance.ShieldRaised -= OnShieldRaised;
        EventManager.Instance.ShieldHit -= OnShieldHit;
        EventManager.Instance.ShieldLowered -= OnShieldLowered;
        EventManager.Instance.NormalBulletFire -= OnNormalBulletFire;
        EventManager.Instance.WeakBulletFire -= OnWeakBulletFire;
        EventManager.Instance.EnergyOrbDestroyed -= OnEnergyOrbDestroyed;
        EventManager.Instance.ItemOrbDestroyed -= OnItemOrbDestroyed;
        EventManager.Instance.TrapOrbDestroyed -= OnTrapOrbDestroyed;
        EventManager.Instance.CloudDestroyed -= OnCloudDestroyed;
        EventManager.Instance.BulletEnd -= OnBulletEnd;
        EventManager.Instance.AlertCorrect-= OnAlertCorrect;
        EventManager.Instance.AlertWrong -= OnAlertWrong;
        EventManager.Instance.PassCorrect-= OnPassCorrect;
        EventManager.Instance.PassWrong -= OnPassWrong;
        EventManager.Instance.TimeOut -= OnTimeOut;
        EventManager.Instance.GameOverRegular -= OnGameOverRegular;
        EventManager.Instance.GameOverTimeOut -= OnGameOverTimeOut;
    }

    private void OnGuardIdle() 
    {
        if (!StatsTracker.Instance.FirstRoundStarted || StatsTracker.Instance.AnswerIsCorrect) 
        {
            PlayDogPant();
        }
    }

    private void OnGuardSniff() 
    {
        PlayDogSniff();
    }

    private void OnGuardBark() 
    {
        PlayDogBark();
    }

    private void OnGuardCall() 
    {
        PlayOfficerCall();
        PlayDogPant(); 
    }

    private void OnGuardWhimper() 
    {
        PlayOfficerAngry();
        PlayDogWhine();
    }

    private void OnGuardWave() 
    {
        PlayOfficerMoveAlong();
    }

    private void OnGuardWatch() 
    {
        PlayDogCurious();
        PlayOfficerHmm();
    }

    private void OnCursorHit() 
    {
        cursorHit.Play();
    }

    private void OnShieldHit() 
    {
        PlayShieldHit();
    }

    private void OnShieldRaised() 
    {
        shield.Play();
    }

    private void OnShieldLowered() 
    {
        shield.Stop();
    }

    private void OnNormalBulletFire() 
    {
        PlayNormalBulletFire();
    }

    private void OnWeakBulletFire() 
    {
        PlayWeakBulletFire();
    } 

    private void OnTrapOrbDestroyed() 
    {
        PlayTrapOrbDestroyed();
    }

    private void OnItemOrbDestroyed() 
    {
        PlayGeneralOrbDestroyed();
    }

    private void OnEnergyOrbDestroyed() 
    {
        PlayGeneralOrbDestroyed();
    }

    private void OnCloudDestroyed() 
    {
        PlayCloudDestroyed();
    }

    private void OnBulletEnd() 
    {
        cursorHit.Stop();
    }

    private void OnAlertCorrect() 
    {
        Invoke("PlayCorrect", 1.75f);
    }

    private void OnAlertWrong() 
    {
        Invoke("PlayWrong", 1.75f);
    }

    private void OnPassCorrect() 
    {
        Invoke("PlayCorrect", 1.75f);
    }

    private void OnPassWrong() 
    {
        Invoke("PlayDoubtful", 1.5f);
    }

    private void OnTimeOut() 
    {
        Invoke("PlayWrong",0.35f); 
    }

    private void OnGameOverRegular() 
    {
        Invoke("StopMainTheme", 2);
        Invoke("PlayGameOverTheme", 2);
    }

    private void OnGameOverTimeOut() 
    {
        Invoke("StopMainTheme", 2.5f);
        Invoke("PlayGameOverTheme", 2.5f);
    }

    private void PlayMainTheme() 
    {
        mainTheme.Play();
    }

    private void StopMainTheme() 
    {
        mainTheme.Stop();
    }

    private void PlayGameOverTheme() 
    {
        clipSource.PlayOneShot(gameOverTheme);
    }

    private void PlayDogWhine() 
    {
        clipSource.PlayOneShot(dogWhine);
    }

    private void PlayDogPant() 
    {
        clipSource.PlayOneShot(dogPant);
    }

    private void PlayDogSniff() 
    {
        clipSource.PlayOneShot(dogSniff);
    }

    private void PlayDogGrowl() 
    {
        clipSource.PlayOneShot(dogGrowl);
    }

    private void PlayDogCurious() 
    {
        clipSource.PlayOneShot(dogCurious);
    }

    private void PlayDogBark() 
    {
        clipSource.PlayOneShot(dogBark);
    }

    private void PlayOfficerAngry() 
    {
        clipSource.PlayOneShot(officerAngry);
    }

    private void PlayOfficerCall() 
    {
        clipSource.PlayOneShot(officerCall);
    }

    private void PlayOfficerHmm() 
    {
        clipSource.PlayOneShot(officerHmm);
    }

    private void PlayOfficerMoveAlong() 
    {
        clipSource.PlayOneShot(officerMoveAlong);
    }

    private void PlayShieldHit() 
    {
        clipSource.PlayOneShot(shieldHit);
    }

    private void PlayNormalBulletFire() 
    {
        clipSource.PlayOneShot(normalBulletShot);
    }

    private void PlayWeakBulletFire() 
    {
        clipSource.PlayOneShot(weakBulletShot);
    }

    private void PlayTrapOrbDestroyed() 
    {
        clipSource.PlayOneShot(trapOrbDestroyed);
    }

    private void PlayGeneralOrbDestroyed() 
    {
        clipSource.PlayOneShot(generalOrbDestroyed);
    }

    private void PlayCloudDestroyed() 
    {
        clipSource.PlayOneShot(cloudDestroyed);
    }

    private void PlayDoubtful() 
    {
        clipSource.PlayOneShot(doubtful, 0.75f);
    }

    private void PlayWrong() 
    {
        clipSource.PlayOneShot(wrong, 0.75f);
    }

    private void PlayCorrect() 
    {
        clipSource.PlayOneShot(correct, 0.75f);
    }
}
