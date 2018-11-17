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
        webcamTexture.requestedFPS = 12;

        screenMat.mainTexture = webcamTexture;
        webcamTexture.Play();


        //pixelY = Mathf.RoundToInt(screenMat.GetVector("_pixels").y);
        //pixelX = Mathf.RoundToInt(screenMat.GetVector("_pixels").x);
        pixelX = 30;
        pixelY = 50;
    }

    int pixelX, pixelY;
    // Update is called once per frame
    void Update()
    {


        if (webcamTexture.didUpdateThisFrame)// && Random.Range(0, 100) > 95)
        {
            //pixelY = Mathf.RoundToInt(Mathf.Sin(Time.time) * 100);

            int offset = Mathf.RoundToInt(((100 - pixelY) / 10) - 5);
            Debug.Log(offset);
            pixelY += Random.Range(-2, 2) + offset;

            pixelX = Mathf.RoundToInt(pixelY * 0.6f);
            screenMat.SetVector("_pixels", new Vector4(pixelX, pixelY));
        }


        pixelY = Mathf.Clamp(pixelY, 0, 100);
    }
}
