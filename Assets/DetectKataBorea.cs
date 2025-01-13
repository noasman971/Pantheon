using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectKataBorea : MonoBehaviour
{
    public int jauge = 0;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        jauge+=1;
        print(jauge);
        if (jauge >= 100)
        {
            print("changez de scène");
            jauge = 0;
            SceneManager.LoadScene("fight");
        }
            
    }


}
