using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogActionArea : MonoBehaviour
{
    DogMovement dog;
    private void Start()
    {
         dog = gameObject.GetComponentInParent<DogMovement>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Sheep"))
        {
            Debug.Log("collided");
            SheepMovement sheep = col.gameObject.GetComponent<SheepMovement>();
            if(sheep && dog.bark)
            {
                Debug.Log("Barking");
                sheep.dogInsight = true;
            }
        }
    }
}

