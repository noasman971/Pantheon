using System;
using Cainos.PixelArtTopDown_Basic;
using UnityEngine;
using UnityEngine.UI;


public class PauseGame:MonoBehaviour {
    private bool pause = false;
    private Button btn;
    

    
    void Start()
    { 
        GameObject.Find("pauseMenu").GetComponent<Canvas>().enabled = false;

    }

    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            pause = !pause;

            if (pause)
            {  GameObject.Find("PF Player").GetComponent<TopDownCharacterController>().enabled = false;
                GameObject.Find("pauseMenu").GetComponent<Canvas>().enabled = true;
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                btn = GameObject.Find("returnGame").GetComponent<Button>();
                btn.onClick.AddListener(ReturnToGame);
            }
            else
            {
                GameObject.Find("PF Player").GetComponent<TopDownCharacterController>().enabled = true;
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                GameObject.Find("pauseMenu").GetComponent<Canvas>().enabled = false;
            }
        }
    }

    void ReturnToGame()
    {
        GameObject.Find("PF Player").GetComponent<TopDownCharacterController>().enabled = true;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.Find("pauseMenu").GetComponent<Canvas>().enabled = false;
    }

    private void OnDisable()
    {
        btn.onClick.RemoveAllListeners();
    }
}
