using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideObjects : MonoBehaviour
{
    public bool occupied;
    private void Awake()
    {
        occupied = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (occupied)
        {
            //do animation
        }
        if (!occupied)
        {
            //stop animation
        }
    }
}
   
