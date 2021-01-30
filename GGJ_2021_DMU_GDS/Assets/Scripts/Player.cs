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

    public float curSpeed = 0;

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

        rb.velocity = new Vector3(xMov, rb.velocity.y, zMov) * curSpeed * Time.deltaTime;

        curSpeed = Mathf.MoveTowards(curSpeed, speed, acceleration);

        if (curSpeed > speed)
        {
            curSpeed = speed;
        }
    }
}
