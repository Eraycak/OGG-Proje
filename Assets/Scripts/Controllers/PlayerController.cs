using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterMover mover;
    private FPSLook look;

    private void Awake()
    {
        mover = GetComponent<CharacterMover>();
        look = GetComponent<FPSLook>();
    }


    private void Update()
    {
        mover.MoveToDirection(InputManager.Instance.Movement.ReadValue<Vector2>());
        look.SetCharacterLook(InputManager.Instance.Look.ReadValue<Vector2>());
        if (InputManager.Instance.Jump.IsPressed())
            mover.Jump();
    }
}
