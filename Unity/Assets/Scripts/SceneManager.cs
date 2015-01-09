using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneManager : MonoBehaviour {

    private bool gamePaused = false;
    private GameObject pauseMenu;
    private GameObject startMenu;
	private GameObject loginMenu;
	private GameObject createMenu;
    public string firstPlayScene;
    public string startScene;

	// Use this for initialization
	void Start () {
        pauseMenu = GameObject.Find("PauseMenu");
			if (pauseMenu != null) setCanvasInactive (pauseMenu);
        startMenu = GameObject.Find("StartMenu");
			if (startMenu != null) {
				if (Application.loadedLevelName == startScene)
					setCanvasActive(startMenu);
				else
					setCanvasInactive(startMenu);		
			}
		loginMenu = GameObject.Find ("Login");
			if (loginMenu != null) setCanvasInactive (loginMenu);
		createMenu = GameObject.Find ("Create");
			if (pauseMenu != null) setCanvasInactive (createMenu);


	}

	void SetPause(bool pause) {
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

    void Play() {
		setCanvasInactive(startMenu);
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

	void CreateAccount() {
		setCanvasInactive(loginMenu);
		setCanvasActive(createMenu);
	}

	void BackToLogin() {
		setCanvasActive(loginMenu);
		setCanvasInactive(createMenu);
	}

	void CheckLogin() {
		string username = GameObject.Find("username").GetComponent<InputField>().text;
		string password = GameObject.Find("password").GetComponent<InputField>().text;
		GameObject.Find("errortext").GetComponent<Text>().text = "";
		string url = "http://drproject.twi.tudelft.nl:8084/validateuser?username=" + username + "&password=" + password;
		WWW www = new WWW(url);
		StartCoroutine(GETLogin(www));
	}
	
	IEnumerator GETLogin(WWW www){
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
					setCanvasInactive(startMenu);
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

	void Create(){
		string username = GameObject.Find("username").GetComponent<InputField>().text;
	}

    void setCanvasActive(GameObject canvas)
    {
        if (canvas != null)
        {
            canvas.GetComponent<CanvasGroup>().alpha = 1.0f;
            canvas.GetComponent<CanvasGroup>().interactable = true;
            canvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }

    void setCanvasInactive(GameObject canvas)
    {
        if (canvas != null)
        {
            canvas.GetComponent<CanvasGroup>().alpha = 0.0f;
            canvas.GetComponent<CanvasGroup>().interactable = false;
            canvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }
}
