using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables.
    [Header("Rigidbody Component")]
    public Rigidbody rb;

    [Header("Collider Component")]
    public Collider col;

    [Header("Movement Speed")]
    public float speed;
    public float acceleration;

    [Header("Sheep")]
    public bool sheepInHand;

    private float curSpeed = 0;

    //Start is called before the first frame update.
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
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
    }

    //Increase speed function
    void IncreaseSpeed()
    {
        curSpeed = Mathf.MoveTowards(curSpeed, speed, acceleration * Time.deltaTime);

        if (curSpeed > speed)
        {
            curSpeed = speed;
        }

        rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), rb.velocity.y, Input.GetAxisRaw("Vertical")) * curSpeed * Time.deltaTime;
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

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Sheep")
        {
            if (!sheepInHand)
            {
                sheepInHand = true;
            }
        }
    }
}