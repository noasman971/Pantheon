
using Cainos.PixelArtTopDown_Basic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseGame:MonoBehaviour {
    private bool pause = false;
    
    void Start()
    { 
        GameObject.Find("pauseMenu").GetComponent<Canvas>().enabled = false;

    }
    
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
        
    
    }

    public void Resume()
    {
        pause = false;
        GameObject.Find("PF Player").GetComponent<TopDownCharacterController>().enabled = true;
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
        GameObject.Find("PF Player").GetComponent<TopDownCharacterController>().enabled = true;
        Time.timeScale = 1;
        Cursor.visible = true;
        SceneManager.LoadScene("Badis");

    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pause = !pause;

            if (pause)
            {  GameObject.Find("PF Player").GetComponent<TopDownCharacterController>().enabled = false;
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
