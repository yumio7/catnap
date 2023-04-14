using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuBehavior : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject pauseMenu;
    public Slider slider;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        Camera.main.GetComponent<AudioListener>().enabled = false;
        isGamePaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        Camera.main.GetComponent<AudioListener>().enabled = true;
        isGamePaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LoadMainMenu()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var p in players)
        {
            Destroy(p);
        }
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void UpdateSlider()
    {
        Camera.main.GetComponent<MouseLook>().mouseSensitivity = slider.value;
        PlayerPrefs.SetInt("MouseSenstivity", (int) slider.value);
    }
}
