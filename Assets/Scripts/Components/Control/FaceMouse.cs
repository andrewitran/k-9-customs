using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMouse : MonoBehaviour 
{
    private void Update() 
    {
        Vector3 mousePosition;
        Vector3 facingPosition;
        float facingAngle;

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        facingPosition = mousePosition - transform.position;
        facingPosition.z = 0;  

        facingAngle = Mathf.Atan2(facingPosition.y, facingPosition.x) * Mathf.Rad2Deg; 

        transform.rotation = Quaternion.Euler(0, 0, facingAngle);
    }
}
