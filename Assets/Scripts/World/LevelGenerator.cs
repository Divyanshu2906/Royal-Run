using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] CameraController cameraController;
    [SerializeField] GameObject chunkprefab; // to assign it in the inspector
    [SerializeField] int startingchunkamount = 12; // only 12 chunks will be loaded initially 
    [SerializeField] Transform chunkparent; //now the new chunks will automatically become the child of the parent chunk
    [SerializeField] float chunklength = 10f; // chunks will be loading after ever z = 10 interval
    [SerializeField] float movespeed = 8f; // the speed at which the chunks will move
    [SerializeField] float Minmovespeed = 2f; 
    [SerializeField] float destroyDistance = 20f;
    

    List<GameObject> chunks = new List<GameObject>();  //List

    void Start() // it will run once will the scene begins
    {
        SpawnStartingChunks();
    }

    void Update() // it will run every frame
    {
        movechunk();
    }

    public void ChangeChunkMoveSpeed(float SpeedAmount)
    {
        movespeed += SpeedAmount;

        if(movespeed < Minmovespeed)
        {
            movespeed = Minmovespeed;
        }
        Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, Physics.gravity.z - SpeedAmount);

        cameraController.ChangeCameraFOV(SpeedAmount);
    }

    void SpawnStartingChunks()
    {
        for (int i = 0; i < startingchunkamount; i++) 
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        float spawnpositionZ = CalculateSpawnPosition();
        Vector3 chunkspawnpos = new Vector3(transform.position.x, transform.position.y, spawnpositionZ); // a new vector3 varibale 
        GameObject newchunk = Instantiate(chunkprefab, chunkspawnpos, quaternion.identity, chunkparent);

        chunks.Add(newchunk);
    }

    float CalculateSpawnPosition()
    {
        float spawnpositionZ; // stores the z position where the chunk will be loaded

        if (chunks.Count == 0)
        {
            spawnpositionZ = transform.position.z; // if transform is (0,0,0) then the first chunk will be exactly where the generator object is
        }
        else
        {
            //spawnpositionZ = transform.position.z + (i * chunklength); // else the chunk will be loaded at every interval of 10
            spawnpositionZ = chunks[chunks.Count - 1].transform.position.z + chunklength;
        }

        return spawnpositionZ;
    }

    void movechunk()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(-transform.forward * (movespeed * Time.deltaTime)); 

            // if(chunk.transform.position.z <= Camera.main.transform.position.z)
            // {
            //     chunks.Remove(chunk);
            //     Destroy(chunk);
            //     SpawnChunk();
            // }
            if (chunk.transform.position.z <= Camera.main.transform.position.z - destroyDistance)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                SpawnChunk();
            }
        }
    }
}
