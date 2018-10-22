using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovePlayer : MonoBehaviour {

    NavMeshAgent playerNavMove;

    public Transform[] movementPoints, crashPoints;

    public int currentDest = 0, currentCrashPoint = 0;

    public GameObject airship, crashingSmokes;
    public float moveSpeed, fallSpeed, crashSpeed;

    public AudioSource planeSource;

    float smokeSpawnTimer;
    public float smokeSpawnTotal = 1;

    SimpleClock clock;
    public float normalBPM, fastBPM;

    public bool hasCrashed, lerpingFOV;

    Camera myCam;

    //going to need to work diligently on getting character animations, dialogue and character sounds right based on timescale
    //look at deathChess for help typing out the strings one by one

	void Start () {
        playerNavMove = GetComponent<NavMeshAgent>();
        planeSource = airship.GetComponent<AudioSource>();
        planeSource.Play();
        SetDestination();
        smokeSpawnTimer = smokeSpawnTotal;
        clock = GameObject.FindGameObjectWithTag("SimpleClock").GetComponent<SimpleClock>();
        myCam = Camera.main;
	}
	
	void Update () {

        if(Vector3.Distance(transform.position, movementPoints[currentDest].position) > 3f)
        {
            //move when mouse down
            if (Input.GetMouseButton(0))
            {
                playerNavMove.isStopped = false;

                if (!hasCrashed)
                {
                    Time.timeScale = 0.5f;
                    ShipsFall();
                    if (!planeSource.isPlaying)
                    {
                        planeSource.UnPause();
                        clock.SetBPM(fastBPM);
                    }
                }

                //play fast character dialogue ( voice and text)
                //play fast character animations
                //set bool audioSpeed true == fast clips on sound producers

            }
            //stay stopped
            else
            {
                playerNavMove.isStopped = true;

               

                if (!hasCrashed)
                {
                    Time.timeScale = 0.1f;

                    if (planeSource.isPlaying)
                    {
                        planeSource.Pause();
                        clock.SetBPM(normalBPM);
                    }
                }

                //play normal character dialogue ( voice and text)
                //play normal character animations
                //set bool audioSpeed false == slow clips on sound producers
            }
        }
        else
        {
            currentDest++;
            SetDestination();
        }

        if (lerpingFOV)
        {
            myCam.fieldOfView = Mathf.Lerp(myCam.fieldOfView, 100, Time.deltaTime * 5);
            if(myCam.fieldOfView > 100)
            {
                lerpingFOV = false;
            }
        }
	}


    //called only while player is moving
    void ShipsFall()
    {
        //while falling through air
        if( currentCrashPoint < 3)
        {
            //randomly shake while falling
            float randomTranslateX = Random.Range(-1f, 1f);
            float randomTranslateZ = Random.Range(-1f, 1f);

            airship.transform.Translate(randomTranslateX, 0, randomTranslateZ);

            //spin plane around z axis
            float randomRotate = Random.Range(0f, 5f);

            airship.transform.Rotate(0, 0, randomRotate);

            moveSpeed = fallSpeed;
        }
        //hitting the ground
        else if(currentCrashPoint == 3)
        {
            airship.transform.localEulerAngles += new Vector3(0.5f, 0, 0);

            moveSpeed = crashSpeed;
        }

        //bouncing up
        else if(currentCrashPoint == 4)
        {

        }

        //always move towards next crash point at fallSpeed
        if (Vector3.Distance(airship.transform.position, crashPoints[currentCrashPoint].position) > 0.25f)
        {
            airship.transform.position = Vector3.MoveTowards(airship.transform.position, crashPoints[currentCrashPoint].position, moveSpeed * Time.deltaTime);
        }
        else
        {
            currentCrashPoint++;

            //activate tiny flame particles
            if(currentCrashPoint == 2)
            {

            }

            //reset rotation before the crash
            if(currentCrashPoint == 3)
            {
                airship.transform.localEulerAngles = new Vector3(airship.transform.localEulerAngles.x, airship.transform.localEulerAngles.y, 0f);

                //activate dirt effect on terrain
            }

            //activate more flame particles
            if (currentCrashPoint == 4)
            {
                //activate dirt effect on terrain
            }

            //activate final flame particles
            if (currentCrashPoint == 5)
            {
                //activate dirt effect on terrain
            }

            //activate final flame particles
            if (currentCrashPoint == 5)
            {
                //activate dirt effect on terrain
            }

            //final chance to add effects
            if (currentCrashPoint == 6)
            {
                
            }

            //activate final flame particles
            if (currentCrashPoint == 7)
            {
                hasCrashed = true;
                lerpingFOV = true;
                playerNavMove.speed = 30;
                Time.timeScale = 1;
            }
        }

        //spawn smoke clouds at smokeSpawnTotal interval
        smokeSpawnTimer -= Time.deltaTime;

        if(smokeSpawnTimer < 0)
        {
            GameObject smokeClone = Instantiate(crashingSmokes, airship.transform.position, Quaternion.identity);

            smokeSpawnTimer = smokeSpawnTotal;
        }
    }

    //called when player arrives at desired movement point
    void SetDestination()
    {
        playerNavMove.SetDestination(movementPoints[currentDest].position);
    }
}
