using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarToFPSSwitcher : MonoBehaviour
{
    public GameObject carCam;
    public GameObject player;
    public CarController car;
    public GameObject playerCarSpawnPoint;
    public bool isWalking = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isWalking)
            {
                isWalking = false;
                player.SetActive(false);
                carCam.SetActive(true);
                car.isDriving = true;
            }
            else
            {
                isWalking = true;
                car.isDriving = false;
                car.horizontalInput = 0;
                car.verticalInput = 0;
                carCam.SetActive(false);
                player.transform.position = playerCarSpawnPoint.transform.position;
                player.SetActive(true);
            }
        }
    }
}
