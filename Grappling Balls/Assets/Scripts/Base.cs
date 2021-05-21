using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour
{
    private int baseLife = 20;
    public Text lifeText;

    public GameObject baseDamageEffect;
    public GameObject baseDeathEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] bomb = GameObject.FindGameObjectsWithTag("Bomb");

        for (int i = 0; i < bomb.Length; i++)
        {
            if (bomb[i] != null)
            {
                float distanceBetween = Vector3.Distance(transform.position, bomb[i].transform.position);
                if (distanceBetween < 2)
                {
                    Instantiate(baseDamageEffect, bomb[i].transform.position, transform.rotation);
                    Destroy(baseDamageEffect, 2);
                    Destroy(bomb[i]);
                    baseLife--;
                    lifeText.text = baseLife.ToString();

                    if (baseLife < 1)
                    {
                        Instantiate(baseDeathEffect, transform.position, transform.rotation);
                        Destroy(baseDeathEffect, 2);
                        Destroy(gameObject);
                        UIManager _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
                        _uiManager.GameOver();
                    }
                }
            }
        }
    }




}
