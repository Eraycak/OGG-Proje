using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressurePlateV2 : MonoBehaviour
{
    [SerializeField]
    private GameObject stairsParent;
    [SerializeField]
    private GameObject bridgeParent;
    [SerializeField]
    private GameObject slopeParent;
    [SerializeField]
    private GameObject bridgeArmParent;
    private float appearingSpeed = 1.5f;

    GameObject[] pressurePlates = null;

    private void Start()
    {
        pressurePlates = GameObject.FindGameObjectsWithTag("PressurePlate");
        int sceneNo = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(sceneNo);
        if (sceneNo == 0)
        {
            bridgeArmParent = GameObject.FindGameObjectWithTag("BridgeArmParent").gameObject;
        }
        else if (sceneNo == 1)
        {
            bridgeParent = GameObject.FindGameObjectWithTag("BridgeParent").gameObject;
        }
        else if(sceneNo == 2)
        {
            stairsParent = GameObject.FindGameObjectWithTag("StairsParent").gameObject;
            slopeParent = GameObject.FindGameObjectWithTag("SlopeParent").gameObject;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
            MovePlate();
            PressPlate();
    }

    private void MovePlate()
    {
        transform.position += new Vector3(0, -1f, 0);
    }

    private void PressPlate()
    {
        int sceneNo = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(sceneNo);
        if (sceneNo == 0)
        {
            bridgeArmParent = GameObject.FindGameObjectWithTag("BridgeArmParent").gameObject;
            StartCoroutine(MoveBridgeArmParent());
        }
        else if (sceneNo == 1)
        {
            bridgeParent = GameObject.FindGameObjectWithTag("BridgeParent").gameObject;
            StartCoroutine(MoveBridgeParent());
        }
        else if (sceneNo == 2)
        {
            stairsParent = GameObject.FindGameObjectWithTag("StairsParent").gameObject;
            slopeParent = GameObject.FindGameObjectWithTag("SlopeParent").gameObject;
            pressurePlates = GameObject.FindGameObjectsWithTag("PressurePlate");
            if (pressurePlates.Length == 3)
            {
                StartCoroutine(MoveSlopeParent());
            }
            else if (pressurePlates.Length == 2)
            {
                StartCoroutine(MoveStairsParent());
            }
        }
        Destroy(this.gameObject, 1f);
    }

    private IEnumerator MoveStairsParent()
    {
        Vector3 startposition = stairsParent.transform.position;
        Vector3 endposition = new Vector3(0, 0, 0);
        float interpolation = 0f;
        while (interpolation < 1f)
        {
            interpolation += Time.deltaTime * appearingSpeed;
            stairsParent.transform.position = Vector3.Lerp(startposition, endposition, interpolation);
        }
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator MoveBridgeParent()
    {
        for (int i = 0; i < bridgeParent.transform.childCount; i++)
        {
            Vector3 startposition = bridgeParent.transform.position;
            Vector3 endposition = startposition;
            GameObject bridgePart = bridgeParent.transform.GetChild(i).gameObject;
            startposition = new Vector3(bridgePart.transform.position.x, startposition.y, bridgePart.transform.position.z);
            endposition = new Vector3(bridgePart.transform.position.x, 0, bridgePart.transform.position.z);
            float interpolation = 0f;
            while (interpolation < 1f)
            {
                interpolation += Time.deltaTime * appearingSpeed;
                bridgePart.transform.position = Vector3.Lerp(startposition, endposition , interpolation);
            }
        }
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator MoveSlopeParent()
    {
        Vector3 targetRotation = new Vector3(0f, 0f, 0f);
        slopeParent.transform.eulerAngles = Vector3.Lerp(slopeParent.transform.eulerAngles, targetRotation, Time.deltaTime * 50);
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator MoveBridgeArmParent()
    {
        bridgeArmParent = GameObject.Find("BridgeArmParent");
        float targetRotation = bridgeArmParent.transform.rotation.eulerAngles.y - 90;
        Vector3 to = new Vector3(0, targetRotation, 0);
        bridgeArmParent.transform.eulerAngles = Vector3.Lerp(bridgeArmParent.transform.rotation.eulerAngles, to, Time.deltaTime * 1000);
        yield return new WaitForEndOfFrame();
    }
}
