using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public string destination;
    public float waitOnPickup = 0.2f;
    public float breakForce = 35f;
    [HideInInspector] public bool pickedUp = false;
    [HideInInspector] public CastController CastController;
    private Vector3 safePos;

    private void Start()
    {
        safePos = transform.localPosition;
        safePos.y = safePos.y + 1;
    }

    private void Update()
    {
        
        if (transform.position.y < -20)
        {
            Debug.Log(safePos.ToString());
            this.GetComponent<Rigidbody>().MovePosition(safePos);
            Debug.Log(transform.position.y.ToString());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (pickedUp)
        {
            if (collision.relativeVelocity.magnitude > breakForce)
            {
                CastController.BreakConnection();
            }

        }
    }

    //this is used to prevent the connection from breaking when you just picked up the object as it sometimes fires a collision with the ground or whatever it is touching
    public IEnumerator PickUp()
    {
        yield return new WaitForSecondsRealtime(waitOnPickup);
        pickedUp = true;

    }
}
