using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLook : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 180;
    [SerializeField] private float clampY = 90f;

    [SerializeField] private Camera gameCamera;
    private Vector3 currentLook;

    private void OnEnable()
    {
        GameStateManager.Instance.OnGameStateChanged += Instance_OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameStateManager.Instance.OnGameStateChanged -= Instance_OnGameStateChanged;
    }
    private void Awake()
    {
        currentLook = transform.rotation.y * Vector3.up + gameCamera.transform.rotation.x * Vector3.right;
    }
    private void Instance_OnGameStateChanged(GameStateManager.GameState state)
    {
        if (state == GameStateManager.GameState.Playing)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    

    public void SetCharacterLook(Vector2 input)
    {
        Vector3 rotation = rotateSpeed * Time.deltaTime * new Vector3(-input.y, input.x, 0);
        currentLook += rotation;
        currentLook.x = Mathf.Clamp(currentLook.x, -clampY, clampY);
        transform.eulerAngles = Vector3.up * currentLook.y;
        gameCamera.transform.eulerAngles = Vector3.right * currentLook.x + transform.eulerAngles;
    }

}
