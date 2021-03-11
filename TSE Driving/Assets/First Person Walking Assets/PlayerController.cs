using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    public float movementSpeed = 1f;
    public float gravity = 9.8f;
    private Vector3 velocity = new Vector3(0, 0, 0);
    public float jumpHeight = 2.0f;
    public float jumpForce = 1.0f;
    private bool isGrounded = false;
    public Transform groundChecker;
    public LayerMask ground;
    public float groundDistance = 0.1f;

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
        float horizontal = Input.GetAxis("Horizontal") * movementSpeed;
        float vertical = Input.GetAxis("Vertical") * movementSpeed;
        characterController.Move((Camera.main.transform.right * horizontal + Camera.main.transform.forward * vertical) * Time.deltaTime);

        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            velocity.y += Mathf.Sqrt(jumpHeight * jumpForce * gravity);
        }

        velocity.y -= gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);


    }
}
