using UnityEngine;
using UnityEngine.SceneManagement;

public class changementzone : MonoBehaviour
{
  
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        SceneManager.LoadScene("background");

    }

    void Update()
    {
        
    }
}
