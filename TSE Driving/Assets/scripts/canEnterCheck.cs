using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canEnterCheck : MonoBehaviour
{
    public GameplayManager gameManager;

    private void Start()
    {
        GameObject gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameplayManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" )
        {
            //for (int i = 0;  i < this.GetComponentsInChildren<ParticleSystem>().Length; i++)
            //{
            //    this.GetComponentsInChildren<ParticleSystem>()[i].Play();
            //}
            gameManager.canEnter = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            gameManager.canEnter = false;
        }
    }
}
