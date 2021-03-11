using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    public string destination;
    private bool delivered = false;
    private DeliveryZone deliveryZone;

    private void Start()
    {
        deliveryZone = this.GetComponent<DeliveryZone>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Cargo" && other.GetComponent<PhysicsObject>().pickedUp == false && other.GetComponent<PhysicsObject>().destination == destination)
        {
            other.gameObject.layer = LayerMask.NameToLayer("CargoDelivered");
            for (int i = 0;  i < this.GetComponentsInChildren<ParticleSystem>().Length; i++)
            {
                this.GetComponentsInChildren<ParticleSystem>()[i].Play();
            }
            deliveryZone.enabled = false;
        }
    }
}
