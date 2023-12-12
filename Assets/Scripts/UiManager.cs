using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{
    #region Private Variables
    [SerializeField]
    private Button continueButton;
    [SerializeField]
    private Button mainMenuButton;
    [SerializeField]
    private Button exitButton;
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button muteButton;
    [SerializeField]
    private Button howToPlayButton;
    [SerializeField]
    private Button howToPlay2Button;
    [SerializeField]
    private Button howToPlay3Button;
    [SerializeField]
    private Button howToPlay4Button;
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private Button creditsButton;
    [SerializeField]
    private GameObject muteCross;
    [SerializeField]
    private GameObject pausePopUp;
    #endregion

    public static bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(BackToLevel);
        }
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(GoToMainMenu);
            Time.timeScale = 1f;
            isPaused = false;
        }
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PositionPausePopUp();
            TogglePausePopUp();
        }
    }
    public void BackToLevel()
    {
        pausePopUp.SetActive(false);
        Time.timeScale = 1f;
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Saindo do jogo");
    }
    public void TogglePausePopUp()
    {
        Debug.Log("calling: TogglePausePopUp");
        if (pausePopUp != null)
        {
            if (isPaused == false)
            {
                Debug.Log("check: isPaused == false");
                pausePopUp.SetActive(true);
                Time.timeScale = 0f;
                isPaused = true;
            }
            else
            {
                Debug.Log("check: isPaused != false");
                pausePopUp.SetActive(false);
                Time.timeScale = 1f;
                isPaused = false;
            }
        }
    }
    void PositionPausePopUp()
    {
        Debug.Log("calling: PositionPausePopUp");
        Vector3 screenCenter = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 worldCenter = Camera.main.ViewportToWorldPoint(screenCenter);
        pausePopUp.transform.position = worldCenter;
    }
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(1);
        Debug.Log("clicou em play");
    }
    public void OnHowToPlayButtonClicked()
    {
        SceneManager.LoadScene("HowToPlay1");
        Debug.Log("clicou em how to play");
    }
    public void OnHowToPlay2ButtonClicked()
    {
        SceneManager.LoadScene("HowToPlay2");
        Debug.Log("clicou em how to play");
    }
    public void OnHowToPlay3ButtonClicked()
    {
        SceneManager.LoadScene("HowToPlay3");
        Debug.Log("clicou em how to play");
    }
    public void OnHowToPlay4ButtonClicked()
    {
        SceneManager.LoadScene("HowToPlay4");
        Debug.Log("clicou em how to play");
    }
    public void OnQuitButtonClicked()
    {
        Debug.Log("clicou em quit");
        Application.Quit();
    }
    public void OnCreditsButtonClicked()
    {
        Debug.Log("clicou em Credits");
        SceneManager.LoadSceneAsync("Credits");
    }
}