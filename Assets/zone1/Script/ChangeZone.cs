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
        SceneManager.LoadScene(sceneName);
    }
}
