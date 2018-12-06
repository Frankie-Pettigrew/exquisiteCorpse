using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Timeline;


public class JulianWorldMan : MonoBehaviour
{

    public Material halftoneMat;
    public Text initText;
    public AudioSource planeEngineSound, music;
    public AudioClip engineLoop;
    CamLook c;


    // Use this for initialization
    void Start()
    {
        halftoneMat.SetFloat("_colorStrength", 0);
        initText.color = Color.clear;
        c = Camera.main.GetComponent<CamLook>();
        c.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Opening();

    }

    float openingTim, htColorStrength;

    public void Opening()
    {

        openingTim += Time.deltaTime;

        if (openingTim > 10)
        {
            if (openingTim > 20)
            {

                if (initText.color.a > 0)
                    initText.color -= new Color(0, 0, 0, Time.deltaTime);
                else
                {
                    addHalfToneColorStrength(0.5f, 1.5f);
                    if (!music.isPlaying)
                        music.PlayDelayed(1);
                }

                if (!planeEngineSound.isPlaying)
                {
                    planeEngineSound.clip = engineLoop;
                    planeEngineSound.loop = true;
                    planeEngineSound.Play();
                }

                c.enabled = true;

                if (planeEngineSound.volume > 0.6f)
                    planeEngineSound.volume -= Time.deltaTime * 0.5f;

            }
            else
            {
                if (initText.color.a < 1)
                    initText.color += new Color(0, 0, 0, Time.deltaTime);
            }

        }

    }

    void addHalfToneColorStrength(float speed, float max)
    {

        if (htColorStrength < max)
        {
            htColorStrength += Time.deltaTime * speed;
            halftoneMat.SetFloat("_colorStrength", htColorStrength);
        }

    }

}
