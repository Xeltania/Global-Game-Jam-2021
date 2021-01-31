using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables.
    [Header("Rigidbody Component")]
    public Rigidbody rb;

    [Header("Collider Component")]
<<<<<<< HEAD
    public Collider CapCol;
    public Collider SphCol;
=======
    public Collider col;
>>>>>>> main

    [Header("Movement Speed")]
    public float speed;
    public float acceleration;

<<<<<<< HEAD
    [Header("Sheep")]
    public int sheepInHand;
    public GameObject Sheep;

    private float curSpeed = 0;
    private bool canScore;
=======
    public float curSpeed = 0;
>>>>>>> main

    //Start is called before the first frame update.
    void Start()
    {
        rb = GetComponent<Rigidbody>();
<<<<<<< HEAD
=======
        col = GetComponent<Collider>();
>>>>>>> main
    }

    //Update is called once per frame.
    void Update()
    {
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        if ((xMov != 0) || (zMov !=0))
        {
            IncreaseSpeed();
        }
        else
        {
            DecreaseSpeed();
        }
<<<<<<< HEAD

        if (sheepInHand > 0)
        {
            Transform Sheep = this.transform.GetChild(0);
            if (Input.GetKeyDown(KeyCode.E) && canScore==true)
            {
                sheepInHand = 0;
                Destroy(Sheep.transform.gameObject);
                Debug.Log("E");

            }
        }
=======
>>>>>>> main
    }

    //Increase speed function
    void IncreaseSpeed()
    {
        curSpeed = Mathf.MoveTowards(curSpeed, speed, acceleration * Time.deltaTime);

        if (curSpeed > speed)
        {
            curSpeed = speed;
        }

<<<<<<< HEAD
        rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * curSpeed * Time.deltaTime;
=======
        rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), rb.velocity.y, Input.GetAxisRaw("Vertical")) * curSpeed * Time.deltaTime;
>>>>>>> main
    }

    //Decrease speed function
    void DecreaseSpeed()
    {
        curSpeed = Mathf.MoveTowards(curSpeed, 0, acceleration * Time.deltaTime);

        if (curSpeed < 0)
        {
            curSpeed = 0;
        }

        rb.velocity = rb.velocity.normalized * curSpeed * Time.deltaTime;
    }
<<<<<<< HEAD

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Sheep"))
        {
            SheepMovement Behaviour = col.gameObject.GetComponent<SheepMovement>();
            
            if (sheepInHand<1 && !Behaviour.captured)
            {
                sheepInHand++;
                Behaviour.Caught();
            }else
            {
                Debug.Log("You Fucked Up");
            }
        }
        if (col.gameObject.CompareTag("Goal"))
        {
            canScore = true;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Goal"))
        {
            canScore = false;
        }
    }
    

}       
    
=======
}
>>>>>>> main
