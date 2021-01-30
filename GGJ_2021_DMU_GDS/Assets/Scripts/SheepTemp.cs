using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepTemp : MonoBehaviour
{
    bool stop;
    // Start is called before the first frame update
    void Start()
    {
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!stop) gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 0.5f * Time.deltaTime);
    }
    public void StopSheep()
    {
        stop = true;
    }
}
