using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    private Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player1.transform.position, player2.transform.position);
        if(distance > 10) mainCam.transform.position = mainCam.transform.position - gameObject.transform.forward * distance;
    }
}
