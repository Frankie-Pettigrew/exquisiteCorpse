using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public Transform parachuteHandle;
    float lerpVal;
    // Use this for initialization
    void Start()
    {

    }
    float tim;
    bool activatedHandle, activatedParachute;
    // Update is called once per frame
    void Update()
    {
        if (!activatedParachute)
            DoHandleBar();
        else
            DoParachute();

    }

    void DoHandleBar()
    {
        if (!activatedHandle)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (lerpVal < 0.6f)
                    lerpVal += Time.deltaTime * 0.2f;
                else
                {
                    activatedHandle = true;
                }
            }
            else
            {
                if (lerpVal > 0)
                    lerpVal -= Time.deltaTime * 2;
            }
        }
        else
        {
            if (tim < 1f)
            {
                tim += Time.deltaTime;
                lerpVal += Time.deltaTime * 0.05f;
            }
            else
            {
                if (lerpVal < 1)
                    lerpVal += Time.deltaTime * 2f;
                else
                {
                    activatedParachute = true;
                }
            }
        }
        parachuteHandle.localEulerAngles = new Vector3(Mathf.Lerp(0, 60, lerpVal), 0, 0);
    }

    void DoParachute()
    {

    }
}
