using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoProducer : MonoBehaviour {

    //movement of screens back and forth
    //change the screens footage in relation to volume of x frequency range
    //array of video clips (these will be different for different prefabs)

    AudioSpectrum spectrum;

    public int octaveBand;
    
    public float levelMin;

    private VideoPlayer vidPlayer;

    public VideoClip[] myClips;

    public bool inOrOut;

    public bool hasReset;
    float resetTimer;

    public int direction;

    void Start () {
        vidPlayer = GetComponent<VideoPlayer>();
        spectrum = GameObject.FindGameObjectWithTag("ScreenWall").GetComponent<AudioSpectrum>();
        Random.InitState(System.DateTime.Now.Millisecond);
        ChangeVidClip();
    }
    
    void Update () {
        CompareLevels();
        SwitchScreenDepth();
	}
    
    void SwitchScreenDepth()
    {
        switch (direction)
        {
            //forward
            case 0:
                if (inOrOut && transform.localPosition.z < 64f)
                {
                    transform.Translate(0, 0, spectrum.MeanLevels[octaveBand] / 2);
                }
                else if (!inOrOut && transform.localPosition.z > -24f)
                {
                    transform.Translate(0, 0, -spectrum.MeanLevels[octaveBand] / 2);
                }
                break;
            //left
            case 1:
                if (inOrOut && transform.localPosition.z < 121f)
                {
                    transform.Translate(0, 0, spectrum.MeanLevels[octaveBand] / 2);
                }
                else if (!inOrOut && transform.localPosition.z > 30f)
                {
                    transform.Translate(0, 0, -spectrum.MeanLevels[octaveBand] / 2);
                }
                break;
            //right
            case 2:
                if (inOrOut && transform.localPosition.z < 88f)
                {
                    transform.Translate(0, 0, spectrum.MeanLevels[octaveBand] / 2);
                }
                else if (!inOrOut && transform.localPosition.z > -8f)
                {
                    transform.Translate(0, 0, -spectrum.MeanLevels[octaveBand] / 2);
                }
                break;
        }
       
    }

    void CompareLevels()
    {
        Debug.Log("compared");
        if (hasReset)
        {
            resetTimer -= Time.deltaTime;

            if (resetTimer < 0)
            {
                hasReset = false;
            }
        }

        if (spectrum.MeanLevels[octaveBand] > levelMin)
        {
            if (!hasReset)
            {
                ChangeVidClip();
            }
            
        }
    }

    //call this to change the clip
    public void ChangeVidClip()
    {
        int randomClip = Random.Range(0, myClips.Length);
        vidPlayer.clip = myClips[randomClip];
        vidPlayer.Play();
        inOrOut = !inOrOut;
        resetTimer = Random.Range(1f, 10f);
        hasReset = true;
        Debug.Log("changed");
    }
    
}
