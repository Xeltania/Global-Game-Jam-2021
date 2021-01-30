using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawn : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject Sheep; // uses sheep prefab
    public GameObject Player; // will be used to create a ring around the player in which sheep cant spawn
    [Header("Map size")]
    public float MapsizeX; //map bounds
    public float MapsizeZ; //map bounds
    private float Elevation;
    [Header("Sheep Count")]
    public int MaxSheep; //spawn limiter
    public int currentSheep; //current number of sheep spawned
    [Header("Spawn Timer")]
    public float SpawnTime; //time taken for next sheep to spawn



    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < MaxSheep; i++) //spawns initial sheep
        {
            Spawn();
            currentSheep++; //increment current number of sheep
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > SpawnTime) //if timer passes a time and there are less than max sheep spawned, a new sheep will be spawned
        {
            Spawn();
            currentSheep++;
            SpawnTime += Time.time; //standardises spawn timers
        }
    }

    private void Spawn()
    {
        Vector3 SpawnPos = new Vector3(Random.Range(-MapsizeX, MapsizeX), 0, Random.Range(-MapsizeZ, MapsizeZ)); //trying to figure out a way to make it so that y is never out of bounds
        Instantiate(Sheep, SpawnPos, Quaternion.identity);
    }

}
