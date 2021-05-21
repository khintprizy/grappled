using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public int score;
    public Text scoreText;

    public GameObject gameOverMenu;

    public bool isGameOver = false;

    public Text gameOverScore;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText.text = score.ToString();
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyDestroyed(int amount)
    {
        score = score + amount;
        scoreText.text = score.ToString();
    }

    public void MainMenu(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void GameOver()
    {
        gameOverScore.text = score.ToString();
        gameOverMenu.SetActive(true);
        isGameOver = true;
    }

}
