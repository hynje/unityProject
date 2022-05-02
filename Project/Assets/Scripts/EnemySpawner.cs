using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private MapData mapData;
    [SerializeField]
    private GameObject enemyPrefab1;
    [SerializeField]
    private GameObject enemyPrefab2;
    [SerializeField]
    private GameObject enemyPrefab3;
    [SerializeField]
    private GameObject enemyPrefab4;
    float spawnTime;
    float runningTime = 0.0f;
    private void Awake()
    {
        StartCoroutine("SpawnEnemy");
    }

    private void Update()
    {
        runningTime += Time.deltaTime;
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            float positionX = Random.Range(mapData.LimitMin.x, mapData.LimitMax.x); //积己 困摹
            int gen = Random.Range(1, 101);    //积己 犬伏

            if(runningTime <= 15)
            {
                spawnTime = 5.0f;
                if (gen <= 65)
                {
                    Instantiate(enemyPrefab1, new Vector3(positionX, mapData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
                }
                else
                {
                    Instantiate(enemyPrefab2, new Vector3(positionX, mapData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
                }
                yield return new WaitForSeconds(spawnTime);
            }
            else if(runningTime > 15 && runningTime <= 30)
            {
                spawnTime = 4.0f;
                if (gen <= 40)
                {
                    Instantiate(enemyPrefab1, new Vector3(positionX, mapData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
                }
                else if (gen > 40 && gen <= 80)
                {
                    Instantiate(enemyPrefab2, new Vector3(positionX, mapData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
                }
                else
                {
                    Instantiate(enemyPrefab3, new Vector3(positionX, mapData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
                }
                yield return new WaitForSeconds(spawnTime);
            }
            else if(runningTime > 30 && runningTime <= 45)
            {
                spawnTime = 4.0f;
                if (gen <= 25)
                {
                    Instantiate(enemyPrefab1, new Vector3(positionX, mapData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
                }
                else if (gen > 25 && gen <= 70)
                {
                    Instantiate(enemyPrefab2, new Vector3(positionX, mapData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
                }
                else if (gen > 70 && gen <= 90)
                {
                    Instantiate(enemyPrefab3, new Vector3(positionX, mapData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
                }
                else
                {
                    Instantiate(enemyPrefab4, new Vector3(positionX, mapData.LimitMax.y, 0.0f), Quaternion.identity);
                }
                yield return new WaitForSeconds(spawnTime);
            }
            else if(runningTime > 45 && runningTime <= 60)
            {
                spawnTime = 3.0f;
                if (gen <= 20)
                {
                    Instantiate(enemyPrefab1, new Vector3(positionX, mapData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
                }
                else if (gen > 20 && gen <= 55)
                {
                    Instantiate(enemyPrefab2, new Vector3(positionX, mapData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
                }
                else if (gen > 55 && gen <= 90)
                {
                    Instantiate(enemyPrefab3, new Vector3(positionX, mapData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
                }
                else
                {
                    Instantiate(enemyPrefab4, new Vector3(positionX, mapData.LimitMax.y, 0.0f), Quaternion.identity);
                }
                yield return new WaitForSeconds(spawnTime);
            }
            else
            {
                spawnTime = 3.0f;
                if (gen <= 15)
                {
                    Instantiate(enemyPrefab1, new Vector3(positionX, mapData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
                }
                else if (gen > 15 && gen <= 50)
                {
                    Instantiate(enemyPrefab2, new Vector3(positionX, mapData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
                }
                else if (gen > 50 && gen <= 85)
                {
                    Instantiate(enemyPrefab3, new Vector3(positionX, mapData.LimitMax.y + 1.0f, 0.0f), Quaternion.identity);
                }
                else
                {
                    Instantiate(enemyPrefab4, new Vector3(positionX, mapData.LimitMax.y, 0.0f), Quaternion.identity);
                }
                yield return new WaitForSeconds(spawnTime);
            }
            
        }
    }
}
