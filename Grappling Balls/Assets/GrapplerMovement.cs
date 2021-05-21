using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplerMovement : MonoBehaviour
{
    public float movementSpeed = 3f;

    private float horizontal;
    private float vertical;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * horizontal * movementSpeed * Time.deltaTime);
        transform.Translate(Vector3.up * vertical * movementSpeed * Time.deltaTime);

        if (transform.position.x < 0)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
    }
}
