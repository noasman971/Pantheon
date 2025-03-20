using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeZone : MonoBehaviour
{
    public string sceneName;
    
    /// <summary>
    /// Load the scene if we enter in colision
    /// </summary>
    /// <param name="other"> the player who enter on colision trigger</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ChangeScene();
        }
    }

    /// <summary>
    /// Load the scene
    /// </summary>
    public void ChangeScene()
    {
        PlayerPrefs.SetString("scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("positionX",  transform.position.x);
        PlayerPrefs.SetFloat("positionY",  transform.position.y);
        PlayerPrefs.SetFloat("positionZ",  transform.position.z);
        PlayerPrefs.SetInt("canLoad",  1);
        PlayerPrefs.Save();
        Debug.Log("Save");
        SceneManager.LoadScene(sceneName);
    }
}
