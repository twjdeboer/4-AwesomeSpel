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
		string url = "http://drproject.twi.tudelft.nl:8084/validateuser?username=" + username + "&password=" + password;
		WWW www = new WWW(url);
		StartCoroutine(GETLogin(www));
	}



	IEnumerator GETLogin(WWW www)
	{
		yield return www;
		
		// check for errors
		if (www.error == null)
		{
			string response = www.text;
			string[] result = response.Substring(1, response.Length -2).Split (',');
			string msg = result[0].Split(':')[1];
			msg = msg.Substring (1, msg.Length-2);
			switch(msg){
				case ("SUCCESS"):
					string userid = result[1].Split(':')[1];
					Debug.Log (userid);
					ShowStartMenu(false);
					Application.LoadLevel(firstPlayScene);
					break;
				case ("INVALID USER"):
					GameObject.Find("errortext").GetComponent<Text>().text = "invalid username";
					GameObject.Find("password").GetComponent<InputField>().text = "";
					break;
				case ("INVALID PASSWORD"):
					GameObject.Find("errortext").GetComponent<Text>().text = "invalid password";
					GameObject.Find("password").GetComponent<InputField>().text = "";
					break;
				default:
					Debug.Log ("Fatal Error!");
					break;
			}
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
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
