using UnityEngine;
using UnityEngine.SceneManagement;

public class backbutton : MonoBehaviour
{


    public void buttonback() {
        SceneManager.LoadScene("Menu");
        Debug.Log("back Button Press");
        
    }
    

}
