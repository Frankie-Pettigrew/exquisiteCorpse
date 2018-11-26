using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{

    Rigidbody rb;
    public float speed;
    public Material skyboxColors;
    public Transform speedSphere;
    public helice h;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;

        rb = GetComponent<Rigidbody>();
        h.speed = speed * -10000;

        //skyboxColors.SetVector("_scrollSpeed", new Vector4(speed, speed, 0, 0));
    }



    float refreshTim;
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;

        speed += Time.deltaTime * 0.005f;

        //if (refreshTim > 10)
        //{
        //skyboxColors.SetVector("_scrollSpeed", new Vector4(speed, speed, 0, 0));
        speedSphere.localScale = Vector3.one * speed * 2;
        h.speed = speed * -15000;
        refreshTim = 0;
        //}
        //else
        //{
        //    refreshTim += Time.deltaTime;
        //}


    }

    void FixedUpdate()
    {


        rb.MovePosition(transform.position + Vector3.forward * speed);


    }
}
