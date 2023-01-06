using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObjects : MonoBehaviour
{

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        PressurePlate plate = hit.collider.GetComponent<PressurePlate>();
        //plate.SetIsPlayerTouching(plate != null);
    }

    
}
