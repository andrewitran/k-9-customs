using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour 
{
    public MoveTo Move { get; private set; }

    private void Start() 
    {
        Move = GetComponent<MoveTo>();

        EventManager.Instance.GameOverRegular += OnGameOverRegular;
        EventManager.Instance.GameOverTimeOut += OnGameOverTimeOut;
    }

    private void OnDisable() 
    {
        EventManager.Instance.LeftMouseUp -= OnLeftMouseUp;
        EventManager.Instance.GameOverRegular -= OnGameOverRegular;
        EventManager.Instance.GameOverTimeOut -= OnGameOverTimeOut;
    }

    private void OnGameOverRegular() 
    {
        Invoke("EnableReturnToMenu", 4.5f);
    }

    private void OnGameOverTimeOut() 
    {
        Invoke("EnableReturnToMenu", 5.5f);
    }

    private void EnableReturnToMenu() 
    {
        EventManager.Instance.LeftMouseUp += OnLeftMouseUp;
    }

    private void OnLeftMouseUp() 
    {
        // There are two instances of the container GameObject "Background Processes" in the game scene due to
        // carryover from the last scene. This for loop will find and destroy both of them before returning to the start menu.
        for (int i = 0; i < 2; ++i) 
        {
            Destroy(GameObject.Find("Background Processes"));
        }

        SceneManager.LoadScene("SCN_Start", LoadSceneMode.Single);
    }
}
