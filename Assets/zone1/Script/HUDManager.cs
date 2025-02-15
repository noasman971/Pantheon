using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;
 
    public GameObject DialogueHolder;
    public TextMeshProUGUI nameDisplay, textDisplay;
    public GameObject continueButton;
    private void Awake()
    {
        instance = this;
    }
    
    
    
}
