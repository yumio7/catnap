using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //public float levelDuration = 10.0f;
    //public Text timerText;
    [SerializeField] private Text gameText;
    [SerializeField] private Text scoreText;

    //public AudioClip gameOverSFX;
    //public AudioClip gameWonSFX;

    public static bool isGameOver = false;

    public string nextLevel;

    // float countDown;

    private int totalKills = 0;

    private void Start()
    {

        isGameOver = false;

        totalKills = 0;
        
        RestartScore();
    }

    // Update is called once per frame
    /*
    private void Update()
    {
        if (isGameOver)
        {
            LevelLost();
        } 

    }*/
    

    public void SetScoreText()
    {
        totalKills += 1;

        scoreText.text = "Score: " + totalKills.ToString();
    }
    
    public void RestartScore()
    {
        totalKills = 0;

        scoreText.text = "Score: " + totalKills.ToString();
    }

    public void LevelLost()
    {
        isGameOver = true;
        gameText.text = "GAME OVER!";
        gameText.gameObject.SetActive(true);

        Invoke(nameof(LoadCurrentLevel), 2);

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
            Invoke(nameof(LoadLevel), 2);
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    private void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
