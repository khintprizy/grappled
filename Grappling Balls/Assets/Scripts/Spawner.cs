using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private bool isReady = true;

    public GameObject bomb;


    void Update()
    {
        if (isReady)
        {
            StartCoroutine(SpawnBomb());
        }
    }

    IEnumerator SpawnBomb()
    {
        isReady = false;
        Instantiate(bomb, transform.position, transform.rotation);
        float wait = Random.Range(1, 3);
        yield return new WaitForSeconds(wait);
        isReady = true;
    }
}
