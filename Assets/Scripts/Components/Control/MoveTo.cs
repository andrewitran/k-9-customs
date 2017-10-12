using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour 
{
    private PersonAnimator personAnimator;
    private Vector3 movePosition;
    private float speed;
    private bool isMoving;

    private void Start() 
    {
        personAnimator = GetComponent<PersonAnimator>();
    }

    private void Update() 
    {
        if (isMoving) 
        {
            Move(movePosition, speed);
        }
    }

    public void Move(Vector3 movePosition, float speed) 
    {
        if (transform.position == movePosition) 
        {
            return;
        }

        if (this.movePosition != movePosition) 
        {
            this.movePosition = movePosition;
        }

        if (this.speed != speed) 
        {
            this.speed = speed;
        }

        if (!isMoving) 
        {
            StartMoving();
        }

        transform.position = Vector3.MoveTowards(transform.position, this.movePosition, this.speed * Time.deltaTime);

        if (transform.position == this.movePosition) 
        {
            StopMoving();
        }
    }

    private void StartMoving() 
    {
        isMoving = true;

        if (personAnimator) 
        {
            personAnimator.Walk();
        }
    } 

    private void StopMoving() 
    {
        isMoving = false;

        if (personAnimator) 
        {
            personAnimator.Stand();
        }
    }
}
