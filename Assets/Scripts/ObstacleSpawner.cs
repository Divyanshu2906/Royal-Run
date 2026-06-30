using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject obstacleprefab;
    [SerializeField] float waittime = 1f;

    void Start()
    {
        StartCoroutine(SpawnObstaclesRountine());
    }

    IEnumerator SpawnObstaclesRountine()
    {
        while (true)
        {
            yield return new WaitForSeconds(waittime);
            Instantiate(obstacleprefab, transform.position, Random.rotation);
        }   
    }
}
