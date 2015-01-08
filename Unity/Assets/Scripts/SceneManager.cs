using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneManager : MonoBehaviour {

    private bool gamePaused = false;
    private GameObject pauseMenu;
    private GameObject startMenu;
	private GameObject loginMenu;
    public string firstPlayScene;
    public string startScene;

	// Use this for initialization
	void Start () {
        pauseMenu = GameObject.Find("PauseMenu");
		setCanvasInactive (pauseMenu);
        startMenu = GameObject.Find("StartMenu");
		setCanvasInactive (startMenu);
		loginMenu = GameObject.Find ("Login");
		setCanvasInactive (loginMenu);
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
			setCanvasActive(startMenu);
        }
        else
        {
			setCanvasInactive(startMenu);
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
			setCanvasActive(pauseMenu);
        }
        else
        {
			setCanvasInactive(pauseMenu);
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

	void BackToMain() {
			setCanvasActive(startMenu);
			setCanvasInactive(loginMenu);
		}

	void LoginMenu() {
		setCanvasInactive(startMenu);
		setCanvasActive(loginMenu);

	}

	void CheckLogin() {
		string username = GameObject.Find("username").GetComponent<InputField>().text;
		string password = GameObject.Find("password").GetComponent<InputField>().text;
		}

	void setCanvasActive(GameObject canvas) {
		canvas.GetComponent<CanvasGroup>().alpha = 1;
		canvas.GetComponent<CanvasGroup>().interactable = true;
		canvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
	}
	
	void setCanvasInactive(GameObject canvas) {
		canvas.GetComponent<CanvasGroup>().alpha = 0;
		canvas.GetComponent<CanvasGroup>().interactable = false;
		canvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
	}
}
