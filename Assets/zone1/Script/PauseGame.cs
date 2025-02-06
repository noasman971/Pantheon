using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseGame:MonoBehaviour {
    private bool pause = false;
    public PlayerStamina playerstamina;
    
    void Start()
    { 
        GameObject.Find("pauseMenu").GetComponent<Canvas>().enabled = false;
        playerstamina = GameObject.Find("hero_7").GetComponent<PlayerStamina>();

    }
    
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
        
    
    }
 
    public void Resume()
    {
        pause = false;
        playerstamina.paused = pause;
        GameObject.Find("hero_7").GetComponent<hero>().enabled = true;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.Find("pauseMenu").GetComponent<Canvas>().enabled = false;
    }


    public void Settings()
    {
        Debug.Log("Settings");
    }

    public void MainMenu()
    {
        GameObject.Find("hero_7").GetComponent<hero>().enabled = true;
        Time.timeScale = 1;
        Cursor.visible = true;
        SceneManager.LoadScene("Badis");

    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyUp(KeyCode.JoystickButton0))
        {
            pause = !pause;
            playerstamina.paused = pause;



            if (pause)
            {  GameObject.Find("hero_7").GetComponent<hero>().enabled = false;
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





}
