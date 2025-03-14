using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePlayerInfo : MonoBehaviour
{
    
    
    
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Fight")
        {
            float currentX = PlayerPrefs.GetFloat("positionX", transform.position.x);
            float currentY = PlayerPrefs.GetFloat("positionY", transform.position.y);
            float currentZ = PlayerPrefs.GetFloat("positionZ", transform.position.z);
            transform.position = new Vector3(currentX, currentY, currentZ);
        }

    }

    // Update is called once per frame
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
