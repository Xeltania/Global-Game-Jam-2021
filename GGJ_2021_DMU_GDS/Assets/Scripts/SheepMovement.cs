using UnityEngine;
using UnityEngine.AI;

public enum State { wondering, caught, hiding, flee };

public class SheepMovement : MonoBehaviour
{
    //Variables.
    [Header("Game Objects")]
    public GameObject player;
    public Player playerScript;

    [Header("NavMesh")]
    public NavMeshAgent agent;
    public LayerMask isGround, isPlayer;

    [Header("For Movement")]
    public Vector3 walkPoint, hidePoint;
    bool walkPointSet;
    public float walkPointRange;

    [Header("Flee Settings")]
    public float timeBetweenFlees;
    public bool alreadyFlee;
    public float sightRange;
    public bool playerInSight;

    [Header("States")]
    public State _State;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        _State = State.wondering;
        playerScript = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if player is in sightrange
        playerInSight = Physics.CheckSphere(transform.position, sightRange, isPlayer);

        //Wondering
        if (!playerInSight && _State == State.wondering)
        {
            agent.acceleration = 3;
            agent.speed = 2;
            Wondering();
        }

        //Fleeing
        if (playerInSight && _State == State.wondering)
        {
            agent.acceleration = 6;
            agent.speed = 5;
            Flee();
        }

        //Caught
        if (_State == State.caught)
        {
            Follow();
        }

        //Hiding
        if (_State == State.hiding)
        {
            Hide();
        }
    }

    private void Wondering()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint Reached
        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }

    private Vector3 SearchWalkPoint()
    {
        //calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        
        if (Physics.Raycast(walkPoint, -transform.up, 2f, isGround)) walkPointSet = true;
        
        return walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
    }

    private void Flee()
    {
        if (!alreadyFlee)
        {
            _State = State.flee;

            walkPoint = SearchWalkPoint(); //sets a random location for a sheep to spawn at
            Vector3 distance = walkPoint - player.transform.position;
            do
            {
                walkPoint = SearchWalkPoint();
                distance = walkPoint - player.transform.position;
            } while (distance.magnitude < 5);

            agent.SetDestination(walkPoint);
            alreadyFlee = true;
            Invoke(nameof(ResetFlee), timeBetweenFlees);
        }
    }

    private void ResetFlee()
    {
        SearchWalkPoint();
        alreadyFlee = false;
        _State = State.wondering;
    }

    private void Follow()
    {
        agent.acceleration = 3;
        agent.speed = 2;
        agent.SetDestination(walkPoint);
    }

    private void Hide()
    {
        if (transform.position != hidePoint)
        {
            agent.SetDestination(hidePoint);
        }
    }

    public void Caught()
    {
        _State = State.caught;
    }

    //Collision with other sheep find new waypoint
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sheep")
        {
            walkPointSet = false;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("HideObj"))
        {
            //Get the script from the hidding obj
            hideObjects hoScript = col.gameObject.GetComponent<hideObjects>();
            //Check if the script is associated 
            if (hoScript)
            {
                //check if the sheep can use this hide object to hide
                if (hoScript.occupied == false)
                {
                    hidePoint = col.transform.position;
                    _State = State.hiding;
                    hoScript.occupied = true;
                }
            }
        }
    }
}
