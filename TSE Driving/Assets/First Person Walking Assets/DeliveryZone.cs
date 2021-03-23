using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    public string destination;
    private bool delivered = false;
    public GameObject deliveryZoneCompleted;
    public GameplayManager gameManager;

    private void Start()
    {
        GameObject gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameplayManager>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Cargo" && other.GetComponent<PhysicsObject>().pickedUp == false && other.GetComponent<PhysicsObject>().destination == destination)
        {
            other.gameObject.layer = LayerMask.NameToLayer("CargoDelivered");
            //for (int i = 0;  i < this.GetComponentsInChildren<ParticleSystem>().Length; i++)
            //{
            //    this.GetComponentsInChildren<ParticleSystem>()[i].Play();
            //}
            gameManager.currentDelivered += 1;
            deliveryZoneCompleted.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
