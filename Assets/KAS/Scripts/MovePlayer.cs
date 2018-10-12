using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovePlayer : MonoBehaviour {

    NavMeshAgent playerNavMove;

    public Transform[] movementPoints;

    public int currentDest = 0;
    

	void Start () {
        playerNavMove = GetComponent<NavMeshAgent>();
        
        SetDestination();
	}
	
	void Update () {

        if(Vector3.Distance(transform.position, movementPoints[currentDest].position) > 3f)
        {
            //move when mouse down
            if (Input.GetMouseButton(0))
            {
                playerNavMove.isStopped = false;
            }
            //stay stopped
            else
            {
                playerNavMove.isStopped = true;
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
