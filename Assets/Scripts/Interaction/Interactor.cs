using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    //camera that ray casted from
    [SerializeField] private Camera fpsCamera;
    //distance between camera and object
    [SerializeField] private float grabDistance;
    //object in our hand
    private InteractableObject currentInteractable;

    private void Awake()
    {
        InputManager.Instance.Grab.performed += Grab_performed;
        InputManager.Instance.Grab.canceled += Grab_canceled;
        InputManager.Instance.Throw.performed += Throw_performed;
    }

    private void Throw_performed(InputAction.CallbackContext obj)
    {
        if (currentInteractable == null) return;
        Debug.Log("asep");
        ReleaseObject();
    }

    private void Grab_performed(InputAction.CallbackContext context)
    {
        Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out RaycastHit hit);
        if(hit.transform != null)
        {
            InteractableObject interactable = hit.transform.GetComponent<InteractableObject>();
            if (interactable != null)
                currentInteractable = interactable;
        }
    }

    private void Grab_canceled(InputAction.CallbackContext context)
    {
        if (currentInteractable == null) return;
        ReleaseObject();
    }

    private void Update()
    {
        HandleInteractableMovement();
    }

    private void HandleInteractableMovement()
    {
        if (currentInteractable == null) return;
        //alttaki 2 satir degisecek
        currentInteractable.MoveObject(fpsCamera.transform.position + fpsCamera.transform.forward * grabDistance);
        currentInteractable.transform.LookAt(fpsCamera.transform);
    }

    private void ReleaseObject()
    {
        currentInteractable.SetAngularVelocityToZero();
        currentInteractable = null;
    }


}
