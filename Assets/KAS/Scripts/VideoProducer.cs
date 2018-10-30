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
    VideoClipList vidList;

    public int octaveBand;
    
    public float levelMin;

    private VideoPlayer vidPlayer;

    public List<VideoClip> myClips = new List<VideoClip>();

    public bool inOrOut;

    public bool hasReset;
    float resetTimer;

    public int direction;

    public float shiftSpeed;

    AudioSource screenAudio;
    //public AudioClip[] staticBuzzes;

    void Start () {
        vidPlayer = GetComponent<VideoPlayer>();
        spectrum = GameObject.FindGameObjectWithTag("ScreenWall").GetComponent<AudioSpectrum>();
        vidList = spectrum.GetComponent<VideoClipList>();
        screenAudio = GetComponent<AudioSource>();
        Random.InitState(System.DateTime.Now.Millisecond);
        ChangeVidClip();
    }
    
    void Update () {
        CompareLevels();
        SwitchScreenDepth();

        if (!vidPlayer.isPlaying)
        {
            if (!screenAudio.isPlaying)
                screenAudio.Play();
        }
        else
        {
            if(screenAudio.isPlaying)
                screenAudio.Stop();
        }
	}
    
    void SwitchScreenDepth()
    {
        switch (direction)
        {
            //forward
            case 0:
                if (inOrOut && transform.localPosition.z < 64f)
                {
                    transform.Translate(0, 0, spectrum.MeanLevels[octaveBand] * Time.deltaTime * shiftSpeed) ;
                }
                else if (!inOrOut && transform.localPosition.z > -24f)
                {
                    transform.Translate(0, 0, -spectrum.MeanLevels[octaveBand] * Time.deltaTime * shiftSpeed);
                }
                break;
            ////left
            //case 1:
            //    if (inOrOut && transform.localPosition.z < 121f)
            //    {
            //        transform.Translate(0, 0, spectrum.MeanLevels[octaveBand] / 2);
            //    }
            //    else if (!inOrOut && transform.localPosition.z > 30f)
            //    {
            //        transform.Translate(0, 0, -spectrum.MeanLevels[octaveBand] / 2);
            //    }
            //    break;
            ////right
            //case 2:
            //    if (inOrOut && transform.localPosition.z < 88f)
            //    {
            //        transform.Translate(0, 0, spectrum.MeanLevels[octaveBand] / 2);
            //    }
            //    else if (!inOrOut && transform.localPosition.z > -8f)
            //    {
            //        transform.Translate(0, 0, -spectrum.MeanLevels[octaveBand] / 2);
            //    }
            //    break;
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
        int randomClip = Random.Range(0, myClips.Count);

        //rerun until we find a clip that isn't being used
        if (vidList.clipsBeingUsed[randomClip])
        {
            ChangeVidClip();
        }
        //clip is not being used, so we switch it
        else
        {
            //uncheck last clip being used
            int index = myClips.IndexOf(vidPlayer.clip);
            vidList.clipsBeingUsed[index] = false;

            //set new clip and check it as being used
            vidPlayer.clip = myClips[randomClip];
            vidList.clipsBeingUsed[randomClip] = true;

            //play clip, switch depth movement
            vidPlayer.Play();
            inOrOut = !inOrOut;
            resetTimer = Random.Range(0.5f, 5f);
            hasReset = true;

            //PlayStatic();
            Debug.Log("changed");
        }
        
    }

    //void PlayStatic()
    //{
    //    int randomStatic = Random.Range(0, staticBuzzes.Length);
    //    screenAudio.PlayOneShot(staticBuzzes[randomStatic]);
    //}
    
}
