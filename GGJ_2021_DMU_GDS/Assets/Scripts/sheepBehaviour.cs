using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class sheepBehaviour : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    public LayerMask isGround, isPlayer;

    public Vector3 walkPoint, hidePoint;
    bool walkPointSet;
    public float walkPointRange;
    public float timeBetweenFlees;

    //Flee mode
    bool alreadyFlee;

    //States
    public float sightRange;
    public bool playerInSight;
    public bool isCaught;
    public bool isHidding;


    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        
    }

    private void Update()
    {
        //check if player is in sightrange
        playerInSight = Physics.CheckSphere(transform.position, sightRange, isPlayer);

        if (!playerInSight && !isHidding)
        {
            agent.acceleration = 3;
            agent.speed = 2;
            Wondering();
        }

        if (playerInSight && !isHidding)
        {
            agent.acceleration = 6;
            agent.speed = 5;
            Flee();
        }
        if (isCaught) Caught();

        if (isHidding) Hide();

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

    private void Hide()
    {
        if(transform.position != hidePoint)
            agent.SetDestination(hidePoint);    
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
        SearchWalkPoint();
        alreadyFlee = false;
    }

    private void Caught()
    {
        agent.SetDestination(player.position);
    }

    //Collision with other sheep find new waypoint
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sheep")
        {
            walkPointSet = false;
            Debug.Log("Sheep collision");
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "HideObj")
        {
            //Get the script from the hidding obj
            hideObjects hoScript = col.gameObject.GetComponent<hideObjects>();
            //Check if the script is associated 
            if (hoScript)
            {
                //check if the sheep can use this hide object to hide
                Debug.Log("I have the script");
                if (hoScript.occupied == false)
                {
                    hidePoint = col.transform.position;
                    Debug.Log("hidding");
                    isHidding = true;                  
                }
            }
        }
    }
}
