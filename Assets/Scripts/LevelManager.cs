using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //public float levelDuration = 10.0f;
    //public Text timerText;
    public Text gameText;
    public Text score;

    //public AudioClip gameOverSFX;
    //public AudioClip gameWonSFX;

    public static bool isGameOver = false;

    public string nextLevel;

    // float countDown;

    int totalKills = 0;

    void Start()
    {
        isGameOver = false;

        //countDown = levelDuration;

        //SetTimerText();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            LevelLost();
        }
        /*if (!isGameOver)
        {
            if (countDown > 0)
            {
                countDown -= Time.deltaTime;
            }
            else
            {
                countDown = 0.0f;
                LevelLost();
            }
            SetTimerText();

        } */

    }

    /*void SetTimerText()
    {
        timerText.text = countDown.ToString("f2");
    } */

    public void SetScoreText()
    {
        totalKills += 1;

        score.text = "Score: " + totalKills.ToString();
    }

    public void LevelLost()
    {
        isGameOver = true;
        gameText.text = "GAME OVER!";
        gameText.gameObject.SetActive(true);

        //Camera.main.GetComponent<AudioSource>().pitch = 1;
        //AudioSource.PlayClipAtPoint(gameOverSFX, Camera.main.transform.position);

        Invoke("LoadCurrentLevel", 2);

    }

    public void LevelBeat()
    {
        isGameOver = true;
        gameText.text = "YOU WIN!";
        gameText.gameObject.SetActive(true);

        //Camera.main.GetComponent<AudioSource>().pitch = 2;
        //AudioSource.PlayClipAtPoint(gameWonSFX, Camera.main.transform.position);

        if (!string.IsNullOrEmpty(nextLevel))
        {
            Invoke("LoadLevel", 2);
        }
    }

    void LoadLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
