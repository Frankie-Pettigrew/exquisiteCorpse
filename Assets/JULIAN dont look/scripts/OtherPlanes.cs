using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlanes : MonoBehaviour
{

    public PlaneMovement mainPlane;
    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    float randomSpeedOffset = 1, tim;
    void FixedUpdate()
    {
        if (tim < 10)
            tim += Time.deltaTime;
        else
        {
            tim = 0;
            randomSpeedOffset = Random.Range(0.6f, 1.4f);
        }

        rb.MovePosition(transform.position + Vector3.forward * mainPlane.speed * randomSpeedOffset);


    }
}
