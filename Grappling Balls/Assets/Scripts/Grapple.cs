using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public float startSpeed = 5f;
    //public float speed;
    //private float randomSpeed;

    private LineRenderer lr;
    private GameObject grapplePoint = null;

    private SpringJoint joint;

    private Rigidbody rb;

    public float jointSpring = 4.5f;
    public float jointDamper = 7f;
    public float jointMassScale = 4.5f;
    public float jointMaxDistanceMult = 0.8f;
    public float jointMinDistanceMult = 0.2f;

    public float throwForce = 15f;

    private GameObject[] enemyGO = null;
    //private float distanceToEnemy = 100;

    EnemyStats enemyStatsScript = null;

    public GameObject damageEffect;

    UIManager _uiManager;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 0;

        //randomSpeed = Random.Range(70, 130) / 100;
        //startSpeed = startSpeed * randomSpeed;
        rb = GetComponent<Rigidbody>();

        Destroy(gameObject, 15);

        _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    private void Update()
    {
        if (transform.position.x < -13 || transform.position.x > 13)
        {
            Destroy(gameObject);
        }

        
        if (_uiManager.isGameOver == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (joint == null)
                {
                    StartGrapple();
                }
                else if (joint != null)
                {
                    StopGrapple();
                }
            }
        }



        if (enemyGO != null)
        {
            for (int i = 0; i < enemyGO.Length; i++)
            {
                enemyStatsScript = enemyGO[i].GetComponent<EnemyStats>();

                if (enemyGO[i] != null)
                {
                    float distanceE = Vector3.Distance(transform.position, enemyGO[i].transform.position);

                    if (distanceE < 1.5)
                    {
                        if (enemyStatsScript != null)
                        {
                            enemyStatsScript.enemyHealth--;
                            enemyStatsScript.healthBar.fillAmount = (float)enemyStatsScript.enemyHealth / enemyStatsScript.enemyStartHealth;
                            GameObject _damageEffect = (GameObject)Instantiate(damageEffect, transform.position, transform.rotation);
                            Destroy(_damageEffect, 2);
                        }
                        Destroy(gameObject);
                    }
                }
                
            }

        }







        /*if (enemyGO != null)
        {
            for (int i = 0; i < enemyGO.Length; i++)
            {
                
            }

            distanceToEnemy = Vector3.Distance(transform.position, enemyGO.transform.position);

            if (distanceToEnemy < 1.5)
            {
                if (enemyStatsScript != null)
                {
                    enemyStatsScript.enemyHealth--;
                    enemyStatsScript.healthBar.fillAmount = (float)enemyStatsScript.enemyHealth / enemyStatsScript.enemyStartHealth;
                    GameObject _damageEffect = (GameObject)Instantiate(damageEffect, transform.position, transform.rotation);
                    Destroy(_damageEffect, 2);
                }
                Destroy(gameObject);
            }
        } */  
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * startSpeed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        if (grapplePoint != null)
        {
            joint = this.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint.transform.position;


            float distanceFromPoint = Vector3.Distance(transform.position, grapplePoint.transform.position);

            joint.maxDistance = distanceFromPoint * jointMaxDistanceMult;
            joint.minDistance = distanceFromPoint * jointMinDistanceMult;

            joint.spring = jointSpring;
            joint.damper = jointDamper;
            joint.massScale = jointMassScale;

            lr.positionCount = 2;


            rb.useGravity = true;
            startSpeed = 0;
            
            rb.AddForce(Vector3.right * throwForce, ForceMode.Impulse);
        }
        else
        {
            Debug.Log("NOT IN RANGE");
        }
    }

    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

    void DrawRope()
    {
        if (joint == null) return;

        lr.SetPosition(0, this.transform.position);
        lr.SetPosition(1, grapplePoint.transform.position);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GrapplePoint")
        {
            grapplePoint = other.gameObject;


            enemyGO = GameObject.FindGameObjectsWithTag("Enemy");
            /*if (enemyGO != null)
            {
                for (int i = 0; i < enemyGO.Length; i++)
                {
                    enemyStatsScript = enemyGO[i].GetComponent<EnemyStats>();
                }

            }*/



            //GrapplePoint grapplePointScript = gameObject.GetComponent<GrapplePoint>();
            //grapplePointScript.speed = 0;

            Debug.Log("GRAPPLE POINT FOUND");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "GrapplePoint")
        {
            Destroy(joint);
            lr.positionCount = 0;
            Debug.Log("GRAPPLE POINT EXIT");
            grapplePoint = null;

            
        }
    }
}
