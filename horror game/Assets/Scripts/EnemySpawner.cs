using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyClone;
    public int enemyCount = 50;
    float range = 400;
    Vector3 spawnPoint;


    void Start()
    {
        
    }

    public void Spawn(){
	for(int i=0; i<enemyCount; i++)
        {
            getRandomSpawnPoint();
            Instantiate(enemyClone, spawnPoint, Quaternion.identity);
        }
    }

    void getRandomSpawnPoint()
    {
        while(true)
        {
            Vector3 randomPoint = Random.insideUnitSphere * range + transform.position;

            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas))
            {
                spawnPoint = hit.position;
                return;
            }
        }
    } 
    
}
