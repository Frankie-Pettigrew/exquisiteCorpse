using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovePlayer : MonoBehaviour {

    NavMeshAgent playerNavMove;

    public Transform[] movementPoints;

    public int currentDest = 0;

    public GameObject airship;
    public float fallSpeed;
    public Transform crashDestination;

    public AudioSource planeSource;

	void Start () {
        playerNavMove = GetComponent<NavMeshAgent>();
        planeSource = airship.GetComponent<AudioSource>();
        planeSource.Play();
        SetDestination();
	}
	
	void Update () {

        if(Vector3.Distance(transform.position, movementPoints[currentDest].position) > 3f)
        {
            //move when mouse down
            if (Input.GetMouseButton(0))
            {
                playerNavMove.isStopped = false;
                Time.timeScale = 0.5f;
                airship.transform.position = Vector3.MoveTowards(airship.transform.position, crashDestination.position, fallSpeed * Time.deltaTime);
                if(!planeSource.isPlaying)
                    planeSource.UnPause();

            }
            //stay stopped
            else
            {
                playerNavMove.isStopped = true;
                Time.timeScale = 0.1f;
                planeSource.Pause();
            }
        }
        else
        {
            currentDest++;
            SetDestination();
        }
	}

    void SetDestination()
    {
        playerNavMove.SetDestination(movementPoints[currentDest].position);
    }
}
