using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InteractableObject : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void MoveObject(Vector3 position)
    {
        rb.velocity = (position - transform.position) * 10f;
    }

    public void SetAngularVelocityToZero()
    {
        rb.angularVelocity = Vector3.zero;
    }
}
