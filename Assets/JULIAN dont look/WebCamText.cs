using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCamText : MonoBehaviour
{

    public WebCamTexture webcamTexture;


    public Material screenMat;

    // Use this for initialization
    void Start()
    {

        webcamTexture = new WebCamTexture();

        screenMat.mainTexture = webcamTexture;
        webcamTexture.Play();

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(webcamTexture.width);
        Debug.Log(webcamTexture.didUpdateThisFrame);
    }
}
