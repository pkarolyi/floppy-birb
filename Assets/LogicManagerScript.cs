using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicManagerScript : MonoBehaviour
{
    public GameObject gameOverComponent;
    public Text[] scoreTexts;
    public int score = 0;

    public void increaseScore()
    {
        score++;
        foreach (var scoreText in scoreTexts)
        {
            scoreText.text = score.ToString();
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        gameOverComponent.SetActive(true);
    }

}
