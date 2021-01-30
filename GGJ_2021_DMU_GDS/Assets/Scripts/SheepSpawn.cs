using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SheepSpawn : MonoBehaviour
{

    LayerMask ground;
    [Header("Game Objects")]
    public GameObject Sheep; // uses sheep prefab
    public GameObject Player; // will be used to create a ring around the player in which sheep cant spawn
    [Header("Map size")]
    public float MapsizeX; //map bounds
    public float MapsizeZ; //map bounds
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
        if (Time.time > SpawnTime && currentSheep < MaxSheep) //if timer passes a time and there are less than max sheep spawned, a new sheep will be spawned
        {
            Spawn();
            currentSheep++;
            SpawnTime += Time.time; //standardises spawn timers
        }
    }

    private void Spawn()
    {
        Vector3 SpawnPos = GetRandomLocation(); //sets a random location for a sheep to spawn at
        Vector3 distance = SpawnPos - Player.transform.position;

        do
        {
            SpawnPos = GetRandomLocation();
            distance = SpawnPos - Player.transform.position;
        }while(distance.magnitude < 5); //uses the magnitude of the difference between the sheeps potential spawn and the player to see if the sheep is far enough away from the player to spawn

            Instantiate(Sheep, SpawnPos, Quaternion.identity);
        
    }

    Vector3 GetRandomLocation()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        // Pick the first indice of a random triangle in the nav mesh
        int t = Random.Range(0, navMeshData.indices.Length - 3);

        // Select a random point on it
        Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t + 1]], Random.value);
        Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t + 2]], Random.value);

        return point;
    }
}
