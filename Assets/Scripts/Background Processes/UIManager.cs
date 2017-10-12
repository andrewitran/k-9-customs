using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
    public static UIManager Instance { get; private set; }

    // Blinkable UI Icons.
    [SerializeField]
    private Counter timeIcon;
    [SerializeField]
    private Counter itemsIcon;
    [SerializeField]
    private Counter lifeIcon;

    // Blinkable UI Counters.
    [SerializeField]
    private Counter timeCounter;
    [SerializeField]
    private Counter itemsCounter;
    [SerializeField]
    private Counter lifeCounter;

    // UI Counter Text.
    [SerializeField]
    private Text timeCounterText;
    [SerializeField]
    private Text energyCounterText;
    [SerializeField]
    private Text itemsCounterText;
    [SerializeField]
    private Text lifeCounterText;
    [SerializeField]
    private Text levelCounterText;

    // Verdict signs.
    [SerializeField]
    private VerdictSign correct;
    [SerializeField]
    private VerdictSign wrong;
    [SerializeField]
    private DoubtfulSign doubtful;

    // Misc. UI.
    [SerializeField]
    private GameObject[] background = new GameObject[2];
    [SerializeField]
    private BulletWindow window;
    [SerializeField]
    private GameOverMenu gameOver;

    private Vector3 gameOverPosition = new Vector3(0, 0, -12); 
    private int gameOverSpeed = 5; 

    private UIManager() 
    {
        
    }

    private void Awake() 
    {
        Instance = this;
    }

    private void Start() 
    {
        EventManager.Instance.BulletLoad += OnBulletLoad;
        EventManager.Instance.BulletEnd += OnBulletEnd;
        EventManager.Instance.AlertCorrect += OnAlertCorrect;
        EventManager.Instance.AlertWrong += OnAlertWrong;
        EventManager.Instance.PassCorrect += OnPassCorrect;
        EventManager.Instance.PassWrong += OnPassWrong;
        EventManager.Instance.TimeOut += OnTimeOut;
        EventManager.Instance.TimeUpdate += OnTimeUpdate;
        EventManager.Instance.EnergyUpdate += OnEnergyUpdate;
        EventManager.Instance.ItemsUpdate += OnItemsUpdate;
        EventManager.Instance.LifeDecrease += OnLifeDecrease;
        EventManager.Instance.LevelUpdate += OnLevelUpdate;
        EventManager.Instance.MaxItemsIncrease += OnMaxItemsIncrease;
        EventManager.Instance.MaxTimeDecrease += OnMaxTimeDecrease;
        EventManager.Instance.GameOverRegular += OnGameOverRegular;
        EventManager.Instance.GameOverTimeOut += OnGameOverTimeOut;
    }

    private void OnDisable() 
    {
        EventManager.Instance.BulletLoad -= OnBulletLoad;
        EventManager.Instance.BulletEnd -= OnBulletEnd;
        EventManager.Instance.AlertCorrect -= OnAlertCorrect;
        EventManager.Instance.AlertWrong -= OnAlertWrong;
        EventManager.Instance.PassCorrect -= OnPassCorrect;
        EventManager.Instance.PassWrong -= OnPassWrong;
        EventManager.Instance.TimeOut -= OnTimeOut;
        EventManager.Instance.TimeUpdate -= OnTimeUpdate;
        EventManager.Instance.EnergyUpdate -= OnEnergyUpdate;
        EventManager.Instance.ItemsUpdate -= OnItemsUpdate;
        EventManager.Instance.LifeDecrease -= OnLifeDecrease;
        EventManager.Instance.LevelUpdate -= OnLevelUpdate;
        EventManager.Instance.MaxItemsIncrease -= OnMaxItemsIncrease;
        EventManager.Instance.MaxTimeDecrease -= OnMaxTimeDecrease;
        EventManager.Instance.GameOverRegular -= OnGameOverRegular;
        EventManager.Instance.GameOverTimeOut -= OnGameOverTimeOut;
    }

    private void OnBulletLoad() 
    {
        OpenBulletWindow();
        Invoke("SwitchBackground", 0.285f);
    }

    private void OnBulletEnd() 
    {
        SwitchBackground();
    }

    private void OnAlertCorrect() 
    {
        Invoke("ShowCorrect", 1.75f);
    }

    private void OnAlertWrong() 
    {
        Invoke("ShowWrong", 1.75f);
    }

    private void OnPassCorrect() 
    {
        Invoke("ShowCorrect", 1.75f);
    }

    private void OnPassWrong() 
    {
        Invoke("ShowDoubtful", 1.5f);
    }

    private void OnTimeOut() 
    {
        Invoke("ShowWrong", 0.35f);
    }

    private void OnTimeUpdate() 
    {
        if (timeCounterText.enabled) 
        {
            timeCounterText.text = StatsTracker.Instance.Time.ToString();
        }
    }

    private void OnEnergyUpdate() 
    {
        if (energyCounterText.enabled) 
        {
            energyCounterText.text = Mathf.Round(StatsTracker.Instance.Energy).ToString();
        }
    }

    private void OnItemsUpdate() 
    {
        if (itemsCounterText.enabled) 
        {
            itemsCounterText.text = StatsTracker.Instance.Items.ToString();
        }
    }

    private void OnLifeDecrease() 
    {
        if (lifeCounterText.enabled) 
        {
            lifeCounterText.text = StatsTracker.Instance.Life.ToString();
        }

        BlinkLife();
    }

    private void OnLevelUpdate() 
    {
        if (levelCounterText.enabled) 
        {
            levelCounterText.text = StatsTracker.Instance.Level.ToString();
        }
    }

    private void OnMaxTimeDecrease() 
    {
        BlinkTime();
    }

    private void OnMaxItemsIncrease() 
    {
        BlinkItems();
    }

    private void OnGameOverRegular() 
    {
        Invoke("ShowGameOver", 2.75f);
    }

    private void OnGameOverTimeOut() 
    {
        Invoke("ShowGameOver", 3.25f);
    }

    private void OpenBulletWindow() 
    {
        window.gameObject.SetActive(true);
    }

    private void SwitchBackground() 
    {
        background[0].SetActive(!background[0].activeInHierarchy);
        background[1].SetActive(!background[1].activeInHierarchy);
    }

    private void ShowCorrect() 
    {
        correct.gameObject.SetActive(true);
    }

    private void ShowWrong() 
    {
        wrong.gameObject.SetActive(true);
    }

    private void ShowDoubtful() 
    {
        doubtful.gameObject.SetActive(true);
    }

    private void BlinkTime() 
    {
        timeIcon.Blink.StartBlinking();
        timeCounter.Blink.StartBlinking();
    }

    private void BlinkItems() 
    {
        itemsIcon.Blink.StartBlinking();
        itemsCounter.Blink.StartBlinking();
    }

    private void BlinkLife() 
    {
        lifeIcon.Blink.StartBlinking();
        lifeCounter.Blink.StartBlinking();
    }

    private void ShowGameOver() 
    {
        gameOver.Move.Move(gameOverPosition, gameOverSpeed);
    }
}
