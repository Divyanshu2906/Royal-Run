using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkprefab; // to assign it in the inspector
    [SerializeField] int startingchunkamount = 12; // only 12 chunks will be loaded initially 
    [SerializeField] Transform chunkparent; //now the new chunks will automatically become the child of the parent chunk
    [SerializeField] float chunklength = 10f; // chunks will be loading after ever z = 10 interval
    [SerializeField] float movespeed = 8f; // the speed at which the chunks will move
    List<GameObject> chunks = new List<GameObject>();  //List

    void Start() // it will run once will the scene begins
    {
        SpawnChunks();
    }

    void Update() // it will run every frame
    {
        movechunk();
    }

    void SpawnChunks()
    {
        for (int i = 0; i < startingchunkamount; i++) //for loop
        {
            float spawnpositionZ = CalculateSpawnPosition(i);
            Vector3 chunkspawnpos = new Vector3(transform.position.x, transform.position.y, spawnpositionZ); // a new vector3 varibale 
            GameObject newchunk =  Instantiate(chunkprefab, chunkspawnpos, quaternion.identity, chunkparent);

            chunks.Add(newchunk);
        }
    }

    float CalculateSpawnPosition(int i)
    {
        float spawnpositionZ; // stores the z position where the chunk will be loaded

        if (i == 0)
        {
            spawnpositionZ = transform.position.z; // if transform is (0,0,0) then the first chunk will be exactly where the generator object is
        }
        else
        {
            spawnpositionZ = transform.position.z + (i * chunklength); // else the chunk will be loaded at every interval of 10
        }

        return spawnpositionZ;
    }

    void movechunk()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            chunks[i] .transform.Translate(-transform.forward * (movespeed * Time.deltaTime));
        }
    }
}
