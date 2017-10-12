using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMover : MonoBehaviour 
{
    public static LineMover Instance { get; private set; }

    private const int lineSpeed = 2;
    private const int lineLength = 3;
    private const float crowdSpeed = 3.75f;

    [SerializeField]
    private GameObject[] personPrefab;
    [SerializeField]
    private GameObject[] luggagePrefab;
    [SerializeField]
    private Crowd crowd;
    [SerializeField]
    private ObjectPool personPool;
    [SerializeField]
    private ObjectPool luggagePool;

    private Person[] person = new Person[lineLength];
    private Luggage[] luggage = new Luggage[lineLength];
    private Vector3[] linePosition = new Vector3[lineLength];
    private Vector3 lineRoot = new Vector3(-0.05f, -1.9f, -9); 
    private Vector3 personOffset = new Vector3(1.37f, 0, 0);
    private Vector3 leftBoundary = new Vector3(-3.73f, -1.9f, -9);
    private Vector3 rightBoundary = new Vector3(5.51f, -1.9f, -9);
    private Vector3 spawnPosition;
    private Vector3 crowdSpawnPosition = new Vector3(-4.43f, -1.9f, -9);
    private bool lineIsSet;

    private LineMover() 
    {
        for (int i = 0; i < lineLength; ++i) 
        {
            linePosition[i] = lineRoot + i * personOffset;
        }

        spawnPosition = lineRoot + lineLength * personOffset;
    }

    private void Awake() 
    {
        Instance = this;
    }

    private void Start() 
    {
        CreateLine();
        Invoke("GrabLuggages", 0.05f);
        Invoke("MoveLine", 0.1f);

        EventManager.Instance.NewRound += OnNewRound;
        EventManager.Instance.AlertCorrect += OnAlertCorrect;
        EventManager.Instance.AlertWrong += OnAlertWrong;
        EventManager.Instance.PassCorrect += OnPassCorrect;
        EventManager.Instance.PassWrong += OnPassWrong;
        EventManager.Instance.TimeOut += OnTimeOut;
    }

    private void Update() 
    {
        if (person[0].transform.position == leftBoundary) 
        {
            person[0].grabber.Break();
            person[0].transform.position = spawnPosition;
            person[0].ReturnToPool();

            EventManager.Instance.OnNewRound();
        }

        if (person[0].transform.position == linePosition[0]) 
        {
            if (!lineIsSet) 
            {
                lineIsSet = true;

                if (StatsTracker.Instance.Life > 0) 
                {
                    Invoke("BulletLoad", 0.5f);
                }
            }
        }

        if (crowd.gameObject.activeInHierarchy && crowd.transform.position.x >= person[0].transform.position.x) 
        {
            if (!crowd.grabber.HeldObj) 
            {
                crowd.grabber.Grab(person[0].transform);
                luggage[0].Blink.StartBlinking();
                EventManager.Instance.OnCaught();
            }
        }

        if (crowd.transform.position == rightBoundary) 
        {
            crowd.grabber.Break();
            crowd.transform.position = crowdSpawnPosition;
            crowd.gameObject.SetActive(false);

            EventManager.Instance.OnNewRound();
        }
    }

    private void OnDisable() 
    {
        EventManager.Instance.NewRound -= OnNewRound;
        EventManager.Instance.AlertCorrect -= OnAlertCorrect;
        EventManager.Instance.AlertWrong -= OnAlertWrong;
        EventManager.Instance.PassCorrect -= OnPassCorrect;
        EventManager.Instance.PassWrong -= OnPassWrong;
        EventManager.Instance.TimeOut -= OnTimeOut;
    }

    private void OnNewRound() 
    {
        ShiftLine();
        Invoke("GrabLuggage", 0.05f);
        Invoke("MoveLine", 0.1f);
    }

    private void OnAlertCorrect() 
    {
        Alert();
        Invoke("GetCrowd", 2.75f);
    }

    private void OnAlertWrong() 
    {
        Alert();
        Invoke("GetCrowd", 0.75f);
    }

    private void OnPassCorrect() 
    {
        Invoke("Pass", 1.75f);
    }

    private void OnPassWrong() 
    {
        Invoke("Pass", 1.5f);
    }

    private void OnTimeOut() 
    {
        Invoke("Pass", 1.85f);
    } 

    private void CreateLine() 
    {
        personPool.AddToPool(personPrefab);
        luggagePool.AddToPool(luggagePrefab);

        for (int i = 0; i < lineLength; ++i) 
        {
            person[i] = GetPerson(spawnPosition + i * personOffset);
            luggage[i] = GetLuggage(spawnPosition);
        }
    }

    private void ShiftLine() 
    {
        lineIsSet = false;

        for (int i = 1; i < lineLength; ++i) 
        {
            person[i - 1] = person[i];
            luggage[i - 1] = luggage[i];
        }

        person[lineLength - 1] = GetPerson(spawnPosition);
        luggage[lineLength - 1] = GetLuggage(spawnPosition);
    }

    private void GrabLuggage() 
    {
        person[lineLength - 1].grabber.Grab(luggage[lineLength - 1].transform);
    }

    private void GrabLuggages() 
    {
        for (int i = 0; i < lineLength; ++i) 
        {
            person[i].grabber.Grab(luggage[i].transform);
        }
    }

    private void MoveLine() 
    {
        for (int i = 0; i < lineLength; ++i) 
        {
            person[i].move.Move(linePosition[i], lineSpeed);
        }
    }

    private Person GetPerson(Vector3 position) 
    {
        GameObject obj = personPool.GetRandomObject(position, Quaternion.identity);

        return obj.GetComponent<Person>();
    }

    private Luggage GetLuggage(Vector3 position) 
    {
        GameObject obj = luggagePool.GetRandomObject(position, Quaternion.identity);

        return obj.GetComponent<Luggage>();
    }

    private void GetCrowd() 
    {
        crowd.gameObject.SetActive(true);
        crowd.transform.position = crowdSpawnPosition;
        crowd.move.Move(rightBoundary, crowdSpeed);
    }

    private void BulletLoad() 
    {
        EventManager.Instance.OnBulletLoad();
    }

    private void Alert() 
    {
        person[0].personAnimator.Alert();
        person[0].grabber.Drop();
    }

    private void Pass() 
    {
        person[0].move.Move(leftBoundary, lineSpeed);
    }
}
