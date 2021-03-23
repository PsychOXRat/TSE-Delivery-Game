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

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        timer = this.GetComponent<Timer>();
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
            }
            else if (currentDelivered == maxDeliveryZones)
            {
                gameUI.SetActive(false);
                timer.timerIsRunning = false;
                endUI.SetActive(true);
            }
            deliveryText.text = string.Format("Deliveries: {0}/{1}", currentDelivered, maxDeliveryZones);
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            startUI.SetActive(false);
            gameUI.SetActive(true);
            Time.timeScale = 1;
            timer.timerIsRunning = true;
            started = true;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}
