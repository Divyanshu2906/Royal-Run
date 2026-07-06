using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject FencePrefab;
    [SerializeField] GameObject ApplePrefab;
    [SerializeField] GameObject CoinPrefab;
    [SerializeField] float CoinSeperationLength = 2f;
    [SerializeField] float AppleSpawnChance = .3f;
    [SerializeField] float CoinSpawnChance = .5f;
    [SerializeField] float[] lanes = {-2.5f, 0f, 2.5f};
    LevelGenerator levelGenerator;
    ScoreManager scoreManager;
    List<int>availabeLanes = new List<int> {0,1,2};

    void Start()
    {
        SpawnFence();
        SpawnApples();
        SpawnCoins();
    }

    public void Init(LevelGenerator levelGenerator, ScoreManager scoreManager)
    {
        this.levelGenerator = levelGenerator;
        this.scoreManager = scoreManager;
    }

    void SpawnFence()
    {

        List<int>availabeLanes = new List<int> {0,1,2};
        int FencesToSpawn = Random.Range(0, lanes.Length);

        for (int i = 0; i < FencesToSpawn; i++)
        {
            if (availabeLanes.Count <= 0) break;

            int SelectedLane = SelectLane(availabeLanes);

            Vector3 SpawnPosition = new Vector3(lanes[SelectedLane], transform.position.y, transform.position.z);
            Instantiate(FencePrefab, SpawnPosition, Quaternion.identity, this.transform);
        }

    }


    void SpawnApples()
    {
       if(Random.value > AppleSpawnChance || availabeLanes.Count <= 0) return; 

       int SelectedLane = SelectLane(availabeLanes);

       Vector3 SpawnPosition = new Vector3(lanes[SelectedLane], transform.position.y, transform.position.z);
       Apple newApple = Instantiate(ApplePrefab, SpawnPosition, Quaternion.identity, this.transform).GetComponent<Apple>();
       newApple.Init(levelGenerator);
    }

     void SpawnCoins()
    {
       if(Random.value > CoinSpawnChance || availabeLanes.Count <= 0) return; 

       int SelectedLane = SelectLane(availabeLanes);
       int MaxCoinsToSpawn = 6;
       int CoinsToSpawn = Random.Range(1,MaxCoinsToSpawn);
       float TopOfChunkPos = transform.position.z + (CoinSeperationLength * 2); 

       for (int i = 0; i < CoinsToSpawn; i++)
       {
            float SpawnPositionZ = TopOfChunkPos - (i * CoinSeperationLength);
            Vector3 SpawnPosition = new Vector3(lanes[SelectedLane], transform.position.y, SpawnPositionZ);
            Coin newcoin = Instantiate(CoinPrefab, SpawnPosition, Quaternion.identity, this.transform).GetComponent<Coin>();
            newcoin.Init(scoreManager); 
       }

    }

    int SelectLane(List<int> availabeLanes)
    {
        int RandomLaneIndex = Random.Range(0, availabeLanes.Count);
        int SelectedLane = availabeLanes[RandomLaneIndex];
        availabeLanes.RemoveAt(RandomLaneIndex);
        return SelectedLane;
    }
}
