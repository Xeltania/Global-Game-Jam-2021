using UnityEngine;
using UnityEngine.AI;

public enum State { wondering, caught, hiding, flee};

public class SheepMovement : MonoBehaviour
{
    //Variables.
    [Header("Game Objects")]
    public GameObject player;
    public GameObject dog;
    public GameObject scoringArea;
    public Player playerScript;

    [Header("NavMesh")]
    public NavMeshAgent agent;
    public LayerMask isGround, isPlayer, isScore;

    [Header("For Movement")]
    public Vector3 walkPoint, hidePoint;
    bool walkPointSet;
    public float walkPointRange;

    [Header("Flee Settings")]
    public float timeBetweenFlees;
    public bool alreadyFlee;
    public float sightRange;
    public bool playerInSight;
    public bool dogInsight;

    [Header("States")]
    public State _State;
    public bool captured;

    [Header("Map Edges")]
    public GameObject map;
    public float topEdge;
    public float bottomEdge;
    public float leftEdge;
    public float rightEdge;

    private void Awake()
    {
        dog = GameObject.FindGameObjectWithTag("Dog");
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        _State = State.wondering;
        playerScript = player.GetComponent<Player>();
        captured = false;
    }

    // Update is called once per frame
    void Update()
    {
        //check if player is in sightrange
        //playerInSight = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        //dogInsight = Physics.CheckSphere(transform.position, sightRange, isDog);

        if (_State != State.caught && !captured)
        {


            //Wondering
            if (!playerInSight && _State == State.wondering && _State != State.hiding)
            {
                agent.acceleration = 3;
                agent.speed = 2;
                Wondering();
            }

            //Fleeing
            if (_State == State.wondering && _State != State.hiding)
            {
                
                if(playerInSight)
                {
                    Flee(player.transform.position);
                }
                if(dogInsight)
                {
                    Debug.Log("Someone Barked!");
                    Flee(dog.transform.position);
                }
            }
            //Hiding
            if (_State == State.hiding)
            {
                Hide();
            }

        }
        else
        {
            float yOff = 2.5f;
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + yOff, player.transform.position.z);
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
        walkPoint = new Vector3( Mathf.Clamp(transform.position.x + randomX, leftEdge, rightEdge), transform.position.y, Mathf.Clamp(transform.position.z + randomZ, bottomEdge, topEdge));
       // Debug.Log(walkPoint.x + " " + walkPoint.y + " " + walkPoint.z);
        return walkPoint;
    }

    private void Flee(Vector3 runFrom)
    {
        agent.acceleration = 6;
        agent.speed = 5;

        if (!alreadyFlee)
        {
            _State = State.flee;
            runFrom = runFrom - transform.position;
            Vector3 newFleeTarget = (transform.position + - (runFrom.normalized) * 10);
            agent.SetDestination(newFleeTarget);
            alreadyFlee = true;
            Invoke(nameof(ResetFlee), timeBetweenFlees);
        }
    }

    

    private void ResetFlee()
    {
        if (_State != State.caught)
        {
            agent.acceleration = 3;
            agent.speed = 2;
            alreadyFlee = false;
            dogInsight = false;
            _State = State.wondering;
        }
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
        captured = true;       
        transform.parent = player.transform;
       
    }   


    //Collision with other sheep find new waypoint
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sheep")
        {
            walkPointSet = false;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("HideObj"))
        {            
            //Get the script from the hidding obj
            hideObjects hoScript = col.gameObject.GetComponent<hideObjects>();
            //Check if the script is associated 
            if(hoScript)
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