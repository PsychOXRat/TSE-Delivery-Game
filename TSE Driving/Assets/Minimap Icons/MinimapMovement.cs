using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapMovement : MonoBehaviour
{
    public Transform player;
    public Transform car;
    public CarToFPSSwitcher drivingCheckObject;
    private Vector3 newPosition;
    private void LateUpdate()
    {
        if (drivingCheckObject.isWalking)
        {
            newPosition = player.transform.position;
        }
        else
        {
            newPosition = car.transform.position;
        }
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
}
