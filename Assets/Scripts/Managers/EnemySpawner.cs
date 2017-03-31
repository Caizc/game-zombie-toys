using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [HeaderAttribute("Spawner Properties")]
    [SerializeField]
    GameObject enemyPrefab;
    [SerializeField]
    float spawnRate = 5f;
    [SerializeField]
    int maxEnemies = 10;

    [HeaderAttribute("Debugging Properties")]
    [SerializeField]
    bool canSpawn = true;

    EnemyHealth[] enemies;
    WaitForSeconds spawnDelay;

    void Awake()
    {
        enemies = new EnemyHealth[maxEnemies];

        for (int i = 0; i < maxEnemies; i++)
        {
            GameObject obj = (GameObject)Instantiate(enemyPrefab);
            EnemyHealth enemy = obj.GetComponent<EnemyHealth>();
            obj.transform.parent = transform;
            obj.SetActive(false);
            enemies[i] = enemy;
        }

        spawnDelay = new WaitForSeconds(spawnRate);
    }

    IEnumerator Start()
    {
        while (canSpawn)
        {
            yield return spawnDelay;

            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (!enemies[i].gameObject.activeSelf)
            {
                enemies[i].transform.position = transform.position;
                enemies[i].transform.rotation = transform.rotation;
                enemies[i].gameObject.SetActive(true);

                return;
            }
        }
    }
}
