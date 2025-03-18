using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseGame:MonoBehaviour {
    private bool pause = false;
    public PlayerStamina playerstamina;
    public GameObject player;
    
    void Start()
    { 
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject.Find("pauseMenu").GetComponent<Canvas>().enabled = false;
        playerstamina = player.GetComponent<PlayerStamina>();

    }
    
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
        
    
    }
 
    /// <summary>
    /// Active the movement of the player & desactive freeze time and pauseUI
    /// </summary>
    public void Resume()
    {
        pause = false;
        playerstamina.paused = pause;
        GameObject.Find("hero_7").GetComponent<Hero>().enabled = true;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.Find("pauseMenu").GetComponent<Canvas>().enabled = false;
    }


 

    /// <summary>
    /// Return to the scene meny
    /// </summary>
    public void MainMenu()
    {
        GameObject.Find("hero_7").GetComponent<Hero>().enabled = true;
        Time.timeScale = 1;
        Cursor.visible = true;
        SceneManager.LoadScene("Menu");

    }
    
    /// <summary>
    /// If we press P the game is pause and we cant move
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyUp(KeyCode.JoystickButton0))
        {
            pause = !pause;
            playerstamina.paused = pause;



            if (pause)
            {  GameObject.Find("hero_7").GetComponent<Hero>().enabled = false;
                GameObject.Find("pauseMenu").GetComponent<Canvas>().enabled = true;
                
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Resume();
            }
        }
    }

    /// <summary>
    /// Save the position x,y,z and active the button load game in menu
    /// </summary>
    public void Save()
    {
        
        if (SceneManager.GetActiveScene().name != "Fight")
        {
            PlayerPrefs.SetString("scene", SceneManager.GetActiveScene().name);
            PlayerPrefs.SetFloat("positionX",  player.transform.position.x);
            PlayerPrefs.SetFloat("positionY",  player.transform.position.y);
            PlayerPrefs.SetFloat("positionZ",  player.transform.position.z);
            PlayerPrefs.SetInt("canLoad",  1);
            PlayerPrefs.Save();
            Debug.Log("Save");
        }
    }





}
