using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectKataBorea : MonoBehaviour
{
    public int jauge = 0;
    
    /// <summary>
    /// Add one to each grass we touch if the jauge is equal to 100 load the scene fight
    /// and save its scene and position x,y,z
    /// </summary>
    /// <param name="other"> The player who enter on colision trigger</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "grass")
        {
            jauge+=1;
            print(jauge);
        }
      
        if (jauge >= 100 && other.gameObject.tag == "grass")
        {
            jauge = 0;
            PlayerPrefs.SetString("scene", SceneManager.GetActiveScene().name);
            PlayerPrefs.SetFloat("positionX",  transform.position.x);
            PlayerPrefs.SetFloat("positionY",  transform.position.y);
            PlayerPrefs.SetFloat("positionZ",  transform.position.z);
            PlayerPrefs.SetInt("canLoad",  1);
            PlayerPrefs.Save();
            Debug.Log("Save");
            SceneManager.LoadScene("fight");
        }
            
    }


}
