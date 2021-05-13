using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    public float movementSpeed = 5f;
    public float gravity = 9.8f;
    private Vector3 velocity = new Vector3(0, 0, 0);
    public float jumpHeight = 2.0f;
    public float jumpForce = 1.0f;
    private bool isGrounded = false;
    public Transform groundChecker;
    public LayerMask ground;
    public float groundDistance = 0.1f;
    public GameObject objectForRotate;

    private Vector3 camForward;
    private Transform cam;
    private Vector3 moveVector;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, ground, QueryTriggerInteraction.Ignore);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0.0f;
        }

        // player movement - forward, backward, left, right
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //characterController.Move((Camera.main.transform.right * horizontal + Camera.main.transform.forward * vertical) * Time.deltaTime);

        GetComponentInParent<Transform>().rotation = Quaternion.Euler(0, objectForRotate.transform.rotation.eulerAngles.y, 0);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = 5f;
        }
        else
        {
            movementSpeed = 3f;
        }

        cam = Camera.main.transform;
        camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
        moveVector = (vertical * camForward + horizontal * Camera.main.transform.right);
        //moveVector *= Time.deltaTime;
        moveVector *= movementSpeed;

        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            velocity.y += Mathf.Sqrt(jumpHeight * jumpForce * gravity);
        }

        velocity.y -= gravity * Time.deltaTime;

        velocity.Set(moveVector.x, velocity.y, moveVector.z);
        characterController.Move(velocity * Time.deltaTime);


    }
}
