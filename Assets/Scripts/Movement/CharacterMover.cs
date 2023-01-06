using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float sprintMultiplier = 1.6f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce = 10f;
    private float gravity = -9.81f;
    private Vector3 velocity;

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        InputManager.Instance.Sprint.performed += EnableSprint;
        InputManager.Instance.Sprint.canceled += DisableSprint;
    }

    private void EnableSprint(InputAction.CallbackContext context)
    {
        moveSpeed *= sprintMultiplier;
    }

    private void DisableSprint(InputAction.CallbackContext context)
    {
        moveSpeed /= sprintMultiplier;
    }

    public void MoveToDirection(Vector2 input)
    {
        //update velocity x and z axes (added isGround chect to not moving in air)
        if(characterController.isGrounded)
            velocity = (transform.right * input.x + transform.forward * input.y) * moveSpeed + velocity.y * transform.up;

        //update velocity y axis
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        //move character
        characterController.Move(velocity * Time.deltaTime);
    }
    public void Jump()
    {
        if (!characterController.isGrounded) return;
        velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
    }
}
