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
    public PlayerStats playerStats;

    void Start()
    {
        
        listAttaque = player.GetComponent<ListAttaque>();
        listKatara = player.GetComponent<ListKatara>();
        newGameButton.SetActive(false);
        loadButton.SetActive(false);
        playerStats = player.GetComponent<PlayerStats>();

        Cursor.visible = true;

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
        if (PlayerPrefs.GetInt("canLoad", 0) == 0)
        {
            loadButton.SetActive(false);

        }


        
        
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("canLoad", 0);
        listAttaque.ClearList();
        listKatara.ClearList();
        PlayerPrefs.SetFloat("positionX",  -24f);
        PlayerPrefs.SetFloat("positionY",  4.25f);
        PlayerPrefs.SetFloat("positionZ",  0);
        PlayerPrefs.SetFloat("currenthealth", playerStats.maxhealth);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Starter");
        
    }


    public void LoadGame()
    {
        if (SceneManager.GetActiveScene().name != "Fight")
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("scene"));
            PlayerPrefs.SetInt("Loaded", 1);

            Debug.Log("Load Game");
        }
        
    }


}
