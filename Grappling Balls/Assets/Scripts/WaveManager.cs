using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private GameObject enemy = null;

    private bool isSpawned = true;

    public GameObject enemyPrefab;

    private int waveCount = 0;

    //private GameObject enemyToBeSpawned;

    //private int howManySpawns;

    // Start is called before the first frame update
    void Start()
    {
        //howManySpawns = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (howManySpawns == 0)
        {
            enemyToBeSpawned = enemyPrefabs[0];
        }
        else if (howManySpawns > 0)
        {
            enemyToBeSpawned = enemyPrefabs[1];
        }*/

        SearchForEnemy();

        if (enemy != null)
        {
            return;
        }
        else if (enemy == null)
        {
            Debug.Log("NO ENEMY");
            if (isSpawned)
            {
                StartCoroutine(SpawnEnemy());
            }
        }

    }

    void SearchForEnemy()
    {
        if (enemy == null)
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy");
        }

    }

    IEnumerator SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            isSpawned = false;
            for (int i = 0; i < waveCount; i++)
            {
                Instantiate(enemyPrefab, transform.position, transform.rotation);
            }
            waveCount++;
            //howManySpawns++;
            yield return new WaitForSeconds(1);
            isSpawned = true;
        }

    }
}
