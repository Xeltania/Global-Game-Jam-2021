using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class DogMovement : MonoBehaviour
{
    public LayerMask isGround;
    NavMeshAgent agent;
    public bool bark;
    public float timeBetweenBarks;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        bark = false;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
           
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100,isGround))
                {
                    agent.destination = hit.point;
                }
           
        }

        if (Input.GetMouseButtonDown(1) && !bark) 
        {
            bark = true;            
            Invoke(nameof(BarkReset), timeBetweenBarks);
        }
    }

    private void BarkReset()
    {
        bark = false;
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Sheep"))
        {
            SheepMovement sheep = col.gameObject.GetComponent<SheepMovement>();
            if (bark)
            {
                sheep.dogInsight = true;
            }
        }
    }
}
