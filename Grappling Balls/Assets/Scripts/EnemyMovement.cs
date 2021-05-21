using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;

    public GameObject[] points;

    private Transform target;

    private bool isChange = true;

    public float pointUpdateTime = 1f;


    // Start is called before the first frame update
    void Start()
    {
        target = points[0].transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isChange)
        {
            StartCoroutine(GetRandomPoints());
        }
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);

       

    }

    

    IEnumerator GetRandomPoints()
    {
        isChange = false;
        int randomPoint = Random.Range(0, points.Length);
        target = points[randomPoint].transform;
        yield return new WaitForSeconds(pointUpdateTime);
        isChange = true;
    }
}
