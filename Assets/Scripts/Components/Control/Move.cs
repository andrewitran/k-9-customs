using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour 
{
    public float Speed { private get; set; }

    private Rigidbody2D rigid2D;

    private void Start() 
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    public void MoveUp() 
    {
        rigid2D.AddForce(Vector3.up * Speed);
    }

    public void MoveLeft() 
    {
        rigid2D.AddForce(-Vector3.right * Speed);
    }

    public void MoveDown() 
    {
        rigid2D.AddForce(-Vector3.up * Speed);
    }

    public void MoveRight() 
    {
        rigid2D.AddForce(Vector3.right * Speed);
    }
}

