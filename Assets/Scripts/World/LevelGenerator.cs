using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CameraController cameraController;
    [SerializeField] GameObject[] chunkprefabs; // to assign it in the inspector
    [SerializeField] GameObject Checkpointchunkprefab;
    [SerializeField] Transform chunkparent; //now the new chunks will automatically become the child of the parent chunk
    [SerializeField] ScoreManager scoreManager;

    [Header("Level Settings")]
    [SerializeField] int startingchunkamount = 12; // only 12 chunks will be loaded initially 
    [SerializeField] float chunklength = 10f; // chunks will be loading after ever z = 10 interval
    [SerializeField] int checkpointchunkinterval = 8;
    [SerializeField] float movespeed = 8f; // the speed at which the chunks will move
    [SerializeField] float Minmovespeed = 2f; 
    [SerializeField] float Maxmovespeed = 20f;
    [SerializeField] float mingravityZ = 2f;
    [SerializeField] float maxgravityZ = 2f;
    [SerializeField] float destroyDistance = 20f;
    

    List<GameObject> chunks = new List<GameObject>();  //List
    int chunkspawned = 0;

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
        float newMovespeed = movespeed + SpeedAmount;
        newMovespeed = Mathf.Clamp(newMovespeed,Minmovespeed,Maxmovespeed);

        if(newMovespeed != movespeed)
        {
            movespeed = newMovespeed;
            float newgravityZ = Physics.gravity.z - SpeedAmount;
            newgravityZ = Mathf.Clamp(newgravityZ,mingravityZ,maxgravityZ);
            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, newgravityZ);
            cameraController.ChangeCameraFOV(SpeedAmount);
        }
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

        GameObject chunktospawn = ChooseChunkToSpawn();

        GameObject newchunkgo = Instantiate(chunktospawn, chunkspawnpos, Quaternion.identity, chunkparent);

        chunks.Add(newchunkgo);
        Chunk newchunk = newchunkgo.GetComponent<Chunk>();
        newchunk.Init(this, scoreManager);

        chunkspawned++;
    }

    private GameObject ChooseChunkToSpawn()
    {
        GameObject chunktospawn;

        if (chunkspawned % checkpointchunkinterval == 0 && chunkspawned != 0)
        {
            chunktospawn = Checkpointchunkprefab;
        }
        else
        {
            chunktospawn = chunkprefabs[Random.Range(0, chunkprefabs.Length)];
        }

        return chunktospawn;
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
