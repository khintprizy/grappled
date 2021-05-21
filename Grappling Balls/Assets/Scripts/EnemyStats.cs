using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public int enemyStartHealth = 5;
    public int enemyHealth;

    public Image healthBar;

    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = enemyStartHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject != null)
        {
            if (enemyHealth < 1)
            {
                GameObject _deathEffect = (GameObject)Instantiate(deathEffect, transform.position, transform.rotation);
                Destroy(_deathEffect, 2);
                UIManager _uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
                _uiManager.EnemyDestroyed(1);
                Destroy(gameObject);
            }
        }

    }
}
