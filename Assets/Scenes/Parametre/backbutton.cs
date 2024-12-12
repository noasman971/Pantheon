using UnityEngine;
using UnityEngine.SceneManagement;

public class backbutton : MonoBehaviour
{


    public void buttonback() {
        SceneManager.LoadScene("Badis");
        Debug.Log("back Button Press");
        
    }
    

}
