using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttoncontroller : MonoBehaviour
{
    public GameObject newGameButton;
    public GameObject loadButton;

    public GameObject player;
    public ListKatara listKatara;
    public ListAttaque listAttaque;

    void Start()
    {
        
        listAttaque = player.GetComponent<ListAttaque>();
        listKatara = player.GetComponent<ListKatara>();
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
        listAttaque.ClearList();
        listKatara.ClearList();
        PlayerPrefs.SetFloat("positionX",  -0.34f);
        PlayerPrefs.SetFloat("positionY",  -1.04f);
        PlayerPrefs.SetFloat("positionZ",  0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("debut map");
        
    }


    public void LoadGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("scene"));
    }


}
