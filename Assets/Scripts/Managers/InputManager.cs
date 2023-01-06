using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    public enum InputType
    {
        Started,
        Performed,
        Canceled
    }


    private PlayerInputActions playerInputAction;

    public InputAction Movement => playerInputAction.Player.Movement;
    public InputAction Look => playerInputAction.Player.Look;
    public InputAction Jump => playerInputAction.Player.Jump;
    public InputAction Sprint => playerInputAction.Player.Sprint;
    public InputAction Grab => playerInputAction.Interaction.Grab;
    public InputAction Throw => playerInputAction.Interaction.Throw;
    protected override void Awake()
    {
        base.Awake();
        playerInputAction = new PlayerInputActions();
    }

    


    private void Start()
    {
        EnablePlayerInputs(true);
    }

    public void EnablePlayerInputs(bool condition)
    {
        if(condition)
        {
            playerInputAction.Enable();
        } 
        else
        {
            playerInputAction.Disable();
        }
    }




}
