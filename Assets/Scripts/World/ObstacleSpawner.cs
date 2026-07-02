using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstacleprefabs;
    [SerializeField] float waittime = 1f;
    [SerializeField] Transform obstacleparent;
    [SerializeField] float SpawnWidth = 4f;
    void Start()
    {
        StartCoroutine(SpawnObstaclesRountine());
    }

    IEnumerator SpawnObstaclesRountine()
    {
        while (true)
        {
            GameObject obstacleprefab = obstacleprefabs[Random.Range(0, obstacleprefabs.Length)];
            Vector3 SpawnPosition = new Vector3(Random.Range(-SpawnWidth, SpawnWidth), transform.position.y, transform.position.z);
            yield return new WaitForSeconds(waittime);
            Instantiate(obstacleprefab, SpawnPosition, Random.rotation, obstacleparent);
        }   
    }
}
