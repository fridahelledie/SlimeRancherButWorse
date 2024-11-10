using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera playerCamera;

    public float aimDistance = 100f;

   
    void Update()
    {
        
    }

    void AimAtCenter()
    {
        //get center of the screen
        Ray centerRay = playerCamera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));

        //calculate the targetpoint in the world based on the aim distance
        Vector3 targetPoint = centerRay.GetPoint(aimDistance);

        //rotate gun tp look at the target point
        transform.LookAt(targetPoint);
    }
}
