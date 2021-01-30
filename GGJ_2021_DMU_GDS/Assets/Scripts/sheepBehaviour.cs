using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class sheepBehaviour : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    public LayerMask isGround, isPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public float timeBetweenFlees;

    //Flee mode
    bool alreadyFlee;

    //States
    public float sightRange;
    public bool playerInSight;
    public bool isCaught;


    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //check if player is in sightrange
        playerInSight = Physics.CheckSphere(transform.position, sightRange, isPlayer);

        if (!playerInSight) Wondering();
        if (playerInSight) Flee();
        if (isCaught) Caught();
    }
    private void Wondering()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint Reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, isGround))
            walkPointSet = true;
    }

    private void Flee()
    {
        

        if (!alreadyFlee)
        {
            agent.SetDestination(-player.position);
            alreadyFlee = true;
            Invoke(nameof(ResetFlee), timeBetweenFlees);
        }
    }

    private void ResetFlee()
    {
        alreadyFlee = false;
    }

    private void Caught()
    {
        agent.SetDestination(player.position);
    }
}
