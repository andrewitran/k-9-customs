using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsTracker : MonoBehaviour 
{
    public static StatsTracker Instance { get; private set; }
    public const int MaxEnergy = 10;

    public int Life { get; private set; }
    public bool HasContraband { private get; set; }
    public bool HasContrabandRemaining { get; set; }
    public bool BulletInProgress { get; set; }
    public bool FirstRoundStarted { get; private set; }
    public bool AnswerIsCorrect { get; private set; }

    private int time;
    private float energy;
    private int items;
    private int level;
    private int[] maxTimeSetting = new int[5];
    private int maxTime;

    private StatsTracker() 
    { 
        maxTimeSetting[0] = 20; 
        maxTimeSetting[1] = 15; 
        maxTimeSetting[2] = 12; 
        maxTimeSetting[3] = 10; 
        maxTimeSetting[4] = 8; 

        maxTime = maxTimeSetting[0];

        Time = maxTime;
        Energy = 10;
        Life = 3;
        Level = 1;
    }

    public int Time 
    { 
        get 
        {
            return time;
        }
        private set 
        {
            time = value;
            EventManager.Instance.OnTimeUpdate();
        }
    }

    public float Energy 
    { 
        get 
        {
            return energy;
        }
        set 
        {
            energy = value;
            EventManager.Instance.OnEnergyUpdate();
        }
    }

    public int Items 
    { 
        get 
        {
           return items;
        }
        set 
        {
            items = value;
            EventManager.Instance.OnItemsUpdate();
        }
    }

    public int Level 
    { 
        get 
        {
            return level;
        } 
        set 
        {
            level = value;
            EventManager.Instance.OnLevelUpdate();
        }
    }

    private void Awake() 
    {
        Instance = this;

        EventManager.Instance.BulletStart += OnBulletStart;
    }

    private void OnDisable() 
    {
        EventManager.Instance.BulletStart -= OnBulletStart;

        if (BulletInProgress) 
        {
            EventManager.Instance.QKeyDown -= Alert;
            EventManager.Instance.EKeyDown -= Pass; 
        }
    }

    private void OnBulletStart() 
    {
        FirstRoundStarted = true;
        AnswerIsCorrect = false;

        SetTime();
        ResetTime();
        ResetEnergy();

        StartTime();

        EventManager.Instance.QKeyDown += Alert;
        EventManager.Instance.EKeyDown += Pass; 
    }

    private void SetTime() 
    {
        int previousMaxTime = maxTime;

        switch (Level) 
        {
            case 5:
                maxTime = maxTimeSetting[1];
                
                break;
            case 9:
                maxTime = maxTimeSetting[2];

                break;
            case 13:
                maxTime = maxTimeSetting[3];

                break;
            case 20:
                maxTime = maxTimeSetting[4];

                break;
        } 

        if (maxTime != previousMaxTime) 
        {
            EventManager.Instance.OnMaxTimeDecrease();
        }
    }

    private void ResetTime() 
    {
        Time = maxTime;
    }

    private void ResetEnergy() 
    {
        Energy = MaxEnergy;
    }

    private void StartTime() 
    {
        InvokeRepeating("TickTime", 1, 1);
    }

    private void TickTime() 
    {
        Time -= 1;

        if (Time == 0) 
        {
            BulletEnd();
            EventManager.Instance.OnTimeOut();
            Invoke("DecreaseLife", 0.35f);
        }
    }

    private void StopTime() 
    {
        CancelInvoke("TickTime");
    }

    private void Alert() 
    {
        BulletEnd();

        if (HasContraband) 
        {
            EventManager.Instance.OnAlertCorrect();
            AnswerIsCorrect = true;
        } 
        else 
        {
            EventManager.Instance.OnAlertWrong();
            Invoke("DecreaseLife", 1.75f);
        } 
    }

    private void Pass() 
    {
        BulletEnd();

        if (!HasContraband) 
        {
            EventManager.Instance.OnPassCorrect();
            AnswerIsCorrect = true;
        } 
        else 
        {
            EventManager.Instance.OnPassWrong();
            Invoke("DecreaseLife", 1.5f);
        }
    }

    private void BulletEnd() 
    {
        EventManager.Instance.QKeyDown -= Alert;
        EventManager.Instance.EKeyDown -= Pass;

        StopTime();
        EventManager.Instance.OnBulletEnd();
    }

    private void DecreaseLife() 
    {
        --Life;
        EventManager.Instance.OnLifeDecrease();
        
        if (Life == 0 && Time != 0) 
        {
            EventManager.Instance.OnGameOverRegular();
        } 
        else if (Life == 0 && Time == 0) 
        {
            EventManager.Instance.OnGameOverTimeOut();
        }
    }
}
