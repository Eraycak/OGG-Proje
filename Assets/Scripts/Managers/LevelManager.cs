using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    GameObject[] pressurePlates = null;

    private void Start()
    {
        pressurePlates = GameObject.FindGameObjectsWithTag("PressurePlate");
    }

    private void LateUpdate()
    {
        pressurePlates = GameObject.FindGameObjectsWithTag("PressurePlate");
        if (pressurePlates.Length == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
