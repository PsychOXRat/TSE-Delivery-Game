using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameplayManager : MonoBehaviour
{
    public int maxDeliveryZones;
    public int currentDelivered = 0;
    public bool started = false;
    public Timer timer;
    public GameObject gameUI;
    public GameObject startUI;
    public GameObject endUI;
    public GameObject endLoseUI;
    public Text deliveryText;
    public CastController castControl;
    public MouseHandler mouseHandler;
    public PlayerController playerControl;

    // Start is called before the first frame update
    void Start()
    {
        castControl.enabled = false;
        mouseHandler.enabled = false;
        playerControl.enabled = false;
        Time.timeScale = 0;
        timer = this.GetComponent<Timer>();
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        if(started)
        {
            if (timer.timeRemaining == 0)
            {
                gameUI.SetActive(false);
                endLoseUI.SetActive(true);
                castControl.enabled = false;
                mouseHandler.enabled = false;
                playerControl.enabled = false;
                Cursor.lockState = CursorLockMode.None;
            }
            else if (currentDelivered == maxDeliveryZones)
            {
                gameUI.SetActive(false);
                timer.timerIsRunning = false;
                endUI.SetActive(true);
                castControl.enabled = false;
                mouseHandler.enabled = false;
                playerControl.enabled = false;
                Cursor.lockState = CursorLockMode.None;
            }
            deliveryText.text = string.Format("Deliveries: {0}/{1}", currentDelivered, maxDeliveryZones);
        }
    }
    public void GameStart()
    {
        startUI.SetActive(false);
        gameUI.SetActive(true);
        castControl.enabled = true;
        mouseHandler.enabled = true;
        playerControl.enabled = true;
        Time.timeScale = 1;
        timer.timerIsRunning = true;
        started = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
