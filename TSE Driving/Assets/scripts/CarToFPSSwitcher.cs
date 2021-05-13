using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarToFPSSwitcher : MonoBehaviour
{
    public GameObject carCam;
    public GameObject player;
    public CastController castControl;
    public GameplayManager gameManager;
    public CarController car;
    public GameObject carUI;
    public GameObject playerCarSpawnPoint;
    public GameObject reticle;
    public GameObject enterCheckLeft;
    public GameObject enterCheckRight;
    public bool isWalking = true;
    public bool canEnter = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isWalking && !castControl.isHolding && gameManager.canEnter)
            {
                isWalking = false;
                player.SetActive(false);
                carCam.SetActive(true);
                car.isDriving = true;
                carUI.SetActive(true);
                reticle.SetActive(false);
            }
            else if (castControl.isHolding || !gameManager.canEnter)
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
                reticle.SetActive(true);
                player.SetActive(true);
            }
        }
    }
}
