using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class options : MonoBehaviour
{
    public GameObject panel;
    private bool visible = false;
    
    public TMP_Dropdown DResolution;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            visible = !visible;
            panel.SetActive(visible);
        }
    } 
    public void SetResolution()
    {
        switch (DResolution.value)
        {
            case 0:
                Screen.SetResolution(1920, 1080, true );
                break; 
            case 1:
                Screen.SetResolution(1366, 768, true );
                break;
            case 2:
                Screen.SetResolution(640, 360, true );
                break;
            case 3:
                Screen.SetResolution(2560, 1440, true );
                break;
            case 4:
                Screen.SetResolution(3840, 2160, true );
                break;
        }
    }
}