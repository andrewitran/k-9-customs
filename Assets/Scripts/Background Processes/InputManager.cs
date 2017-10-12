using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour 
{
    public static InputManager Instance { get; private set; }

    private InputManager() 
    {

    }

    private void Awake() 
    {
        Instance = this;
    }

    private void Update() 
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            EventManager.Instance.OnLeftMouseDown();
        }

        if (Input.GetMouseButtonDown(1)) 
        {
            EventManager.Instance.OnRightMouseDown();
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            EventManager.Instance.OnLeftMouseUp();
        }

        if (Input.GetMouseButtonUp(1)) 
        {
            EventManager.Instance.OnRightMouseUp();
        }

        if (Input.GetKey(KeyCode.W)) 
        {
            EventManager.Instance.OnWKeyDown();
        }

        if (Input.GetKey(KeyCode.A)) 
        {
            EventManager.Instance.OnAKeyDown();
        }

        if (Input.GetKey(KeyCode.S)) 
        {
            EventManager.Instance.OnSKeyDown();
        }

        if (Input.GetKey(KeyCode.D)) 
        {
            EventManager.Instance.OnDKeyDown();
        }

        if (Input.GetKeyUp(KeyCode.W)) 
        {
            EventManager.Instance.OnWKeyUp();
        }

        if (Input.GetKeyUp(KeyCode.A)) 
        {
            EventManager.Instance.OnAKeyUp();
        }

        if (Input.GetKeyUp(KeyCode.S)) 
        {
            EventManager.Instance.OnSKeyUp();
        }

        if (Input.GetKeyUp(KeyCode.D)) 
        {
            EventManager.Instance.OnDKeyUp();
        }

        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            EventManager.Instance.OnQKeyDown();
        }

        if (Input.GetKeyDown(KeyCode.E)) 
        {
            EventManager.Instance.OnEKeyDown();
        }
    }
}
