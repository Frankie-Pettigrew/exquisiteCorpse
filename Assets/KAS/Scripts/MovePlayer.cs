using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovePlayer : MonoBehaviour {

    NavMeshAgent playerNavMove;

    public Transform[] movementPoints, crashPoints;

    public int currentDest = 0, currentCrashPoint = 0;

    public GameObject airship, crashingSmokes;
    public float fallSpeed;

    public AudioSource planeSource;

    float smokeSpawnTimer;
    public float smokeSpawnTotal = 1;

	void Start () {
        playerNavMove = GetComponent<NavMeshAgent>();
        planeSource = airship.GetComponent<AudioSource>();
        planeSource.Play();
        SetDestination();
        smokeSpawnTimer = smokeSpawnTotal;
	}
	
	void Update () {

        if(Vector3.Distance(transform.position, movementPoints[currentDest].position) > 3f)
        {
            //move when mouse down
            if (Input.GetMouseButton(0))
            {
                playerNavMove.isStopped = false;
                Time.timeScale = 0.5f;
                ShipsFall();
                if (!planeSource.isPlaying)
                    planeSource.UnPause();

                //play fast character dialogue ( voice and text)
                //play fast character animations

            }
            //stay stopped
            else
            {
                playerNavMove.isStopped = true;
                Time.timeScale = 0.1f;
                planeSource.Pause();

                //play normal character dialogue ( voice and text)
                //play normal character animations
            }
        }
        else
        {
            currentDest++;
            SetDestination();
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
            float randomRotate = Random.Range(0, 5f);

            airship.transform.Rotate(0, 0, randomRotate);
        }

        //always move towards next crash point at fallSpeed
        if (Vector3.Distance(airship.transform.position, crashPoints[currentCrashPoint].position) > 0.25f)
        {
            airship.transform.position = Vector3.MoveTowards(airship.transform.position, crashPoints[currentCrashPoint].position, fallSpeed * Time.deltaTime);
        }
        else
        {
            currentCrashPoint++;
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
