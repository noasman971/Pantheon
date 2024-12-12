using UnityEngine;
using UnityEngine.SceneManagement;

public class buttoncontroller : MonoBehaviour
{


    public void SettingsButton() {
        Debug.Log("Settings Button Press");
        
        }
    public void ExitButton(){
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public void StartButton()
    {
        SceneManager.LoadScene("FirstZone");
        Debug.Log("Start Game");
    }


}
