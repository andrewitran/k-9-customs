using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour 
{
    public static EventManager Instance { get; private set; }

    public delegate void GameEvent();

    // Guard animation events.
    public event GameEvent GuardIdle;
    public event GameEvent GuardSniff;
    public event GameEvent GuardBark;
    public event GameEvent GuardCall;
    public event GameEvent GuardWhimper;
    public event GameEvent GuardWave;
    public event GameEvent GuardWatch;

    // Round events.
    public event GameEvent NewRound;
    public event GameEvent BulletLoad;
    public event GameEvent BulletStart;
    public event GameEvent BulletEnd;

    // Input events.
    public event GameEvent LeftMouseDown;
    public event GameEvent RightMouseDown;
    public event GameEvent LeftMouseUp;
    public event GameEvent RightMouseUp;
    public event GameEvent WKeyDown;
    public event GameEvent AKeyDown;
    public event GameEvent SKeyDown;
    public event GameEvent DKeyDown;
    public event GameEvent QKeyDown;
    public event GameEvent EKeyDown;
    public event GameEvent WKeyUp;
    public event GameEvent AKeyUp;
    public event GameEvent SKeyUp;
    public event GameEvent DKeyUp;

    // Player events.
    public event GameEvent CursorHit;
    public event GameEvent ShieldRaised;
    public event GameEvent ShieldLowered;
    public event GameEvent ShieldHit;

    // Bullet events.
    public event GameEvent NormalBulletFire;
    public event GameEvent WeakBulletFire;

    // Target events.
    public event GameEvent EnergyOrbDestroyed;
    public event GameEvent ItemOrbDestroyed;
    public event GameEvent TrapOrbDestroyed;
    public event GameEvent CloudDestroyed;

    // Verdict events.
    public event GameEvent AlertCorrect;
    public event GameEvent AlertWrong;
    public event GameEvent PassCorrect;
    public event GameEvent PassWrong;
    public event GameEvent TimeOut;
    public event GameEvent Caught;

    // UI update events.
    public event GameEvent TimeUpdate;
    public event GameEvent EnergyUpdate;
    public event GameEvent ItemsUpdate;
    public event GameEvent LifeDecrease;
    public event GameEvent LevelUpdate;

    // Difficulty increase events.
    public event GameEvent MaxItemsIncrease;
    public event GameEvent MaxTimeDecrease;

    // Game over events.
    public event GameEvent GameOverRegular;
    public event GameEvent GameOverTimeOut;
    
    private EventManager() 
    {

    } 
        
    private void Awake() 
    {
        Instance = this;
    }

    public void OnGuardIdle() 
    {
        if (GuardIdle != null) 
        {
            GuardIdle();
        }
    }

    public void OnGuardSniff() 
    {
        if (GuardSniff != null) 
        {
            GuardSniff();
        }
    }

    public void OnGuardBark() 
    {
        if (GuardBark != null) 
        {
            GuardBark();
        }
    }

    public void OnGuardCall() 
    {
        if (GuardCall != null) 
        {
            GuardCall();
        }
    }

    public void OnGuardWhimper() 
    {
        if (GuardWhimper != null) 
        {
            GuardWhimper();
        }
    }

    public void OnGuardWave() 
    {
        if (GuardWave != null) 
        {
            GuardWave();
        }
    }

    public void OnGuardWatch() 
    {
        if (GuardWatch != null) 
        {
            GuardWatch();
        }
    }

    public void OnNewRound() 
    {
        if (NewRound != null) 
        {
            NewRound();
        }
    }
    
    public void OnBulletLoad() 
    {
        if (BulletLoad != null) 
        {
            BulletLoad();
        }

        Invoke("OnBulletStart", 0.285f);
    }

    public void OnBulletStart() 
    {
        if (StatsTracker.Instance.FirstRoundStarted) 
        {
            ++StatsTracker.Instance.Level;
        }

        if (BulletStart != null) 
        {
            BulletStart();
        }

        StatsTracker.Instance.BulletInProgress = true;
    }

    public void OnBulletEnd() 
    {
        StatsTracker.Instance.BulletInProgress = false;

        if (BulletEnd != null) 
        {
            BulletEnd();
        }
    }

    public void OnLeftMouseDown() 
    {
        if (LeftMouseDown != null) 
        {
            LeftMouseDown();
        }
    }

    public void OnRightMouseDown() 
    {
        if (RightMouseDown != null) 
        {
            RightMouseDown();
        }
    }

    public void OnLeftMouseUp() 
    {
        if (LeftMouseUp != null) 
        {
            LeftMouseUp();
        }
    }

    public void OnRightMouseUp() 
    {
        if (RightMouseUp != null) 
        {
            RightMouseUp();
        }
    }

    public void OnWKeyDown() 
    {
        if (WKeyDown != null) 
        {
            WKeyDown();
        }
    }
    
    public void OnAKeyDown() 
    {
        if (AKeyDown != null) 
        {
            AKeyDown();
        }
    }

    public void OnSKeyDown() 
    {
        if (SKeyDown != null) 
        {
            SKeyDown();
        }
    }
    
    public void OnDKeyDown() 
    {
        if (DKeyDown != null) 
        {
            DKeyDown();
        }
    }

    public void OnQKeyDown() 
    {
        if (QKeyDown != null) 
        {
            QKeyDown();
        }
    }

    public void OnEKeyDown() 
    {
        if (EKeyDown != null) 
        {
            EKeyDown();
        }
    }

    public void OnWKeyUp() 
    {
        if (WKeyUp != null) 
        {
            WKeyUp();
        }
    }

    public void OnAKeyUp() 
    {
        if (AKeyUp != null) 
        {
            AKeyUp();
        }
    }

    public void OnSKeyUp() 
    {
        if (SKeyUp != null) 
        {
            SKeyUp();
        }
    }

    public void OnDKeyUp() 
    {
        if (DKeyUp != null) 
        {
            DKeyUp();
        }
    }

    public void OnCursorHit() 
    {
        if (CursorHit != null) 
        {
            CursorHit();
        }
    }

    public void OnShieldRaised() 
    {
        if (ShieldRaised != null) 
        {
            ShieldRaised();
        }
    }

    public void OnShieldLowered() 
    {
        if (ShieldLowered != null) 
        {
            ShieldLowered();
        }
    }

    public void OnShieldHit() 
    {
        if (ShieldHit != null) 
        {
            ShieldHit();
        }
    }

    public void OnNormalBulletFire() 
    {
        if (NormalBulletFire != null) 
        {
            NormalBulletFire();
        }
    }

    public void OnWeakBulletFire() 
    {
        if (WeakBulletFire != null) 
        {
            WeakBulletFire();
        }
    }

    public void OnEnergyOrbDestroyed() 
    {
        if (EnergyOrbDestroyed != null) 
        {
            EnergyOrbDestroyed();
        }
    }

    public void OnItemOrbDestroyed() 
    {
        if (ItemOrbDestroyed != null) 
        {
            ItemOrbDestroyed();
        }
    }

    public void OnTrapOrbDestroyed() 
    {
        if (TrapOrbDestroyed != null) 
        {
            TrapOrbDestroyed();
        }
    }

    public void OnCloudDestroyed() 
    {
        if (CloudDestroyed != null) 
        {
            CloudDestroyed();
        }
    }

    public void OnAlertCorrect() 
    {
        if (AlertCorrect != null) 
        {
            AlertCorrect();
        }
    }

    public void OnAlertWrong() 
    {
        if (AlertWrong != null) 
        {
            AlertWrong();
        }
    }

    public void OnPassCorrect() 
    {
        if (PassCorrect != null) 
        {
            PassCorrect();
        }
    }

    public void OnPassWrong() 
    {
        if (PassWrong != null) 
        {
            PassWrong();
        }
    }

    public void OnTimeOut() 
    {
        if (TimeOut != null) 
        {
            TimeOut();
        }
    }

    public void OnCaught() 
    {
        if (Caught != null) 
        {
            Caught();
        }
    }

    public void OnTimeUpdate() 
    {
        if (TimeUpdate != null) 
        {
            TimeUpdate();
        }
    }

    public void OnEnergyUpdate() 
    {
        if (EnergyUpdate != null) 
        {
            EnergyUpdate();
        }
    }

    public void OnItemsUpdate() 
    {
        if (ItemsUpdate != null) 
        {
            ItemsUpdate();
        }
    }

    public void OnLifeDecrease() 
    {
        if (LifeDecrease != null) 
        {
            LifeDecrease();
        }
    }

    public void OnLevelUpdate() 
    {
        if (LevelUpdate != null) 
        {
            LevelUpdate();
        }
    }

    public void OnMaxTimeDecrease() 
    {
        if (MaxTimeDecrease != null) 
        {
            MaxTimeDecrease();
        }
    }

    public void OnMaxItemsIncrease() 
    {
        if (MaxItemsIncrease != null) 
        {
            MaxItemsIncrease();
        }
    }
    
    public void OnGameOverRegular() 
    {
        if (GameOverRegular != null) 
        {
            GameOverRegular();
        }
    }

    public void OnGameOverTimeOut() 
    {
        if (GameOverTimeOut != null) 
        {
            GameOverTimeOut();
        }
    }
}
