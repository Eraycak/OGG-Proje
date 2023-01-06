using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    GameObject door;
    float doorSpeed = 1.5f;


    bool isOpened = false;
    bool isPlayerTouching;
    private void Start()
    {
        isPlayerTouching = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        PressPlate();
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("çýktý");
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!isOpened)
    //    {
    //        MovePlate();
    //        StartCoroutine(MoveDoor());
    //        isOpened = true;
    //    }

    //}

    public void PressPlate()
    {
        Debug.Log("girdi");
        if (!isOpened)
        {
            MovePlate();
            StartCoroutine(MoveDoor());
            isOpened = true;
        }
    }
    IEnumerator MoveDoor()
    {
        Vector3 startposition = door.transform.position;
        Vector3 endposition = new Vector3(0, 3, 2);
        float interpolation = 0f;
        while (interpolation < 1f)
        {
            interpolation += Time.deltaTime * doorSpeed;
            door.transform.position = Vector3.Lerp(startposition, endposition, interpolation);
            yield return new WaitForEndOfFrame();
        }

    }
    void MovePlate()
    {
        transform.position = new Vector3(0, -0.4f, 0);
    }

    public void SetIsPlayerTouching(bool condition)
    {
        isPlayerTouching = condition;
    }


}