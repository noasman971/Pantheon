using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePlayerInfo : MonoBehaviour
{
    
    
    /// <summary>
    /// Loads the player's position from PlayerPrefs and sets it when the scene is not "Fight".
    /// </summary>
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Fight")
        {
            float currentX = PlayerPrefs.GetFloat("positionX", transform.position.x);
            float currentY = PlayerPrefs.GetFloat("positionY", transform.position.y);
            float currentZ = PlayerPrefs.GetFloat("positionZ", transform.position.z);
            transform.position = new Vector3(currentX, currentY, currentZ);
        }
        else
        {
            transform.position = new Vector3(-21f, -16f, 0);
        }

    }

    /// <summary>
    /// Saves the player's position and the current scene name to PlayerPrefs each frame.
    /// </summary>
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Fight")
        {
            PlayerPrefs.SetString("scene", SceneManager.GetActiveScene().name);
            PlayerPrefs.SetFloat("positionX",  transform.position.x);
            PlayerPrefs.SetFloat("positionY",  transform.position.y);
            PlayerPrefs.SetFloat("positionZ",  transform.position.z);
            PlayerPrefs.Save();
        }



    }


}
