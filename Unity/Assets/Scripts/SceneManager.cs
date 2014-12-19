using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    private bool gamePaused = false;
    private GameObject pauseMenu;
    private GameObject startMenu;
    public string firstPlayScene;
    public string startScene;

	// Use this for initialization
	void Start () {
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.GetComponent<CanvasGroup>().alpha = 0;
        startMenu = GameObject.Find("StartMenu");
        startMenu.GetComponent<CanvasGroup>().alpha = 0;
        CheckStartMenu();
	
	}

    void CheckStartMenu()
    {
        if (Application.loadedLevelName == startScene)
            ShowStartMenu(true);
        else
            ShowStartMenu(false);
    }

    void ShowStartMenu(bool show)
    {
        if(show)
        {
            startMenu.GetComponent<CanvasGroup>().alpha = 1;
            startMenu.GetComponent<CanvasGroup>().interactable = true;
            startMenu.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else
        {
            startMenu.GetComponent<CanvasGroup>().alpha = 0;
            startMenu.GetComponent<CanvasGroup>().interactable = false;
            startMenu.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
	
    void SetPause(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0;
            GameObject.Find("PlayerModel").GetComponent<Animator>().speed = 0;              
            gamePaused = true;
        }
        else
        {
            Time.timeScale = 1;
            GameObject.Find("PlayerModel").GetComponent<Animator>().speed = 1;   
            gamePaused = false;

        }
    }

    void ToggleMenu(bool toggle)
    {
        if(toggle)
        {
            pauseMenu.GetComponent<CanvasGroup>().alpha = 1;
            pauseMenu.GetComponent<CanvasGroup>().interactable = true;
            pauseMenu.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else
        {
            pauseMenu.GetComponent<CanvasGroup>().alpha = 0;
            pauseMenu.GetComponent<CanvasGroup>().interactable = false;
            pauseMenu.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    void TogglePause()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(!gamePaused)
            {
                SetPause(true);
                ToggleMenu(true);
            }
            else
            {
                ToggleMenu(false);
                SetPause(false);
            }
        }
    }
    void Play()
    {
        ShowStartMenu(false);
        Application.LoadLevel(firstPlayScene);
    }

    void Resume()
    {
        SetPause(false);
        ToggleMenu(false);
    }

    void Quit()
    {
        Application.Quit();
    }

	// Update is called once per frame
	void Update () {

        TogglePause();
	}
}
