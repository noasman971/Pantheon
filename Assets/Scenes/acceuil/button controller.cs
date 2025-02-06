using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttoncontroller : MonoBehaviour
{
    public GameObject newGameButton;
    public GameObject loadButton;

    void Start()
    {
        newGameButton.SetActive(false);
        loadButton.SetActive(false);


    }

    public void SettingsButton() {
        SceneManager.LoadScene("settings");
        Debug.Log("Settings Button Press");
        
        }
    public void ExitButton(){
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public void PlayButton()
    {
        Debug.Log("Play Game");
        GameObject.Find("PLAY BUTTON").SetActive(false);
        GameObject.Find("SETTINGS BTN").SetActive(false);
        GameObject.Find("EXIT BTN").SetActive(false);
        newGameButton.SetActive(true);
        loadButton.SetActive(true);


        
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene("FirstZone");
        Debug.Log("Start Game");
    }


}
