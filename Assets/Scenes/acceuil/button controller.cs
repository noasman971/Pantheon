using UnityEngine;

public class buttoncontroller : MonoBehaviour
{
    public void StartButton() {
    Debug.Log("Start Game");
        }

    public void SettingsButton() {
        Debug.Log("Settings Button Press");
        }
    public void ExitButton(){
        Debug.Log("Exit Game");
        Application.Quit ();
        }


}
