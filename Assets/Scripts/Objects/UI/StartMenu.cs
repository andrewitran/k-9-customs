using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour 
{
    private void Start() 
    {
        EventManager.Instance.LeftMouseUp += OnLeftMouseUp;
    }

    private void OnDisable() 
    {
        EventManager.Instance.LeftMouseUp -= OnLeftMouseUp;
    }

    private void OnLeftMouseUp() 
    {
        SceneManager.LoadScene("SCN_Game", LoadSceneMode.Single);
    }
}
