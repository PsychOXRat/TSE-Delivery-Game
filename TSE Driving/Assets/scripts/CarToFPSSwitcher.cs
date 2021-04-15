using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarToFPSSwitcher : MonoBehaviour
{
    public GameObject carCam;
    public GameObject player;
    public CastController castControl;
    public CarController car;
    public GameObject carUI;
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
            if (isWalking && !castControl.isHolding)
            {
                isWalking = false;
                player.SetActive(false);
                carCam.SetActive(true);
                car.isDriving = true;
                carUI.SetActive(true);
            }
            else if (castControl.isHolding)
            {

            }
            else
            {
                isWalking = true;
                car.isDriving = false;
                car.horizontalInput = 0;
                car.verticalInput = 0;
                carUI.SetActive(false);
                carCam.SetActive(false);
                player.transform.position = playerCarSpawnPoint.transform.position;
                player.SetActive(true);
            }
        }
    }
}
