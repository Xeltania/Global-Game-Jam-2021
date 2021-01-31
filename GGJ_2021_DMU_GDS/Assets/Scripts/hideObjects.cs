using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideObjects : MonoBehaviour
{
    public bool occupied;
    public int sheepCount;
    private void Awake()
    {
        sheepCount = 0;
        occupied = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(occupied)
        {
            //do animation
        }
        if(!occupied)
        {
            //stop animation
        }
        if (sheepCount == 0)
        {
            occupied = false;
        }
        else
            occupied = true;
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject.CompareTag("Sheep"))
        {
            occupied = false;
        }
    }
}
