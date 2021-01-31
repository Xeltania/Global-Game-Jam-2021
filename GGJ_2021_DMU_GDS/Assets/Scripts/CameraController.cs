using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    [SerializeField]
    private Camera mainCam;
    private Camera p1Cam;
    private Camera p2Cam;

    [SerializeField]
    private float xOffset;
    [SerializeField]
    private float yOffset;
    [SerializeField]
    private float zOffset;

    [SerializeField]
    private float maxDistance;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        p1Cam = Instantiate<Camera>(mainCam, new Vector3(player1.transform.position.x-xOffset,player1.transform.position.y+yOffset,player1.transform.position.z-zOffset), new Quaternion(0,0,0,0), player1.transform);
        p1Cam.transform.LookAt(player1.transform.position);

        p2Cam = Instantiate<Camera>(mainCam, new Vector3(player2.transform.position.x - xOffset, player2.transform.position.y + yOffset, player2.transform.position.z - zOffset), new Quaternion(0, 0, 0, 0), player2.transform);
        p2Cam.transform.LookAt(player2.transform.position);

        p1Cam.enabled = false;
        p2Cam.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        //update mainCam position
        float newX;
        if(player1.transform.position.x > player2.transform.position.x) newX = player1.transform.position.x - player2.transform.position.x;
        else if (player2.transform.position.x > player1.transform.position.x) newX = player2.transform.position.x - player1.transform.position.x;

        float newZ;
        if (player1.transform.position.z > player2.transform.position.z) newZ = player1.transform.position.z - player2.transform.position.z;
        else if (player2.transform.position.z > player1.transform.position.z) newZ = player2.transform.position.z - player1.transform.position.z;

        if (Vector3.Distance(player1.transform.position, player1.transform.position) > maxDistance)
        {
            //Two Cams
            p1Cam.enabled = true;
            p2Cam.enabled = true;
            mainCam.enabled = false;
        }
        else
        {
            //One Cam
            mainCam.enabled = true;
            p1Cam.enabled = false;
            p2Cam.enabled = false;
        }
    }
}
