using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager1 : Singleton<GameManager>
{
    public TextMeshProUGUI scoreText;
    public GameObject gameoverUI;
    public bool isGameOver = false;


    private int score;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && isGameOver)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GetCoin()
    {
        score++;
        scoreText.text = "SCORE : " + score;
    }
}
