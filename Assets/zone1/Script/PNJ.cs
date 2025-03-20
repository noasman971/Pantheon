using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class PNJ : MonoBehaviour
{
    [SerializeField]
    string[] sentences;
    [SerializeField]
    string characterName;
    int index;
    private bool isOndial, canDial;
    
    
    // Utilisation correcte du Manager
    HUDManager manager => HUDManager.instance;



    private void Update()
    {
        if (!canDial)
        {
            // Fin du dialogue
            isOndial = false;
            index = 0;
            manager.textDisplay.text = ""; 
            manager.nameDisplay.text = ""; 
            manager.DialogueHolder.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.T) && canDial)
        {
            Cursor.visible = true;

            StartDialogue();

        }
    }

    public void StartDialogue()
    {
        // Afficher le dialogue

        manager.DialogueHolder.SetActive(true);
        isOndial = true;
        TypingText(sentences);
    }

    void TypingText(string[] sentences) 
    {
        // Effacer le texte précédent
        manager.nameDisplay.text = "";
        manager.textDisplay.text = "";

        // Affichage du texte du personnage et de la phrase actuelle
        manager.nameDisplay.text = characterName; // Utilise l'index pour choisir le nom
        manager.textDisplay.text = sentences[index];

        // Vérifier si la phrase est complètement affichée
        if (manager.textDisplay.text == sentences[index]) 
        {
            // Afficher le bouton de continuation
            manager.continueButton.SetActive(true); 
        }
    }

    public void NextLine()
    {
        // Masquer le bouton de continuation
        //manager.continueButton.SetActive(false);

        if (isOndial && index < sentences.Length - 1)
        {
            // Passer à la phrase suivante
            index++;
            manager.textDisplay.text = "";
            TypingText(sentences);
        }  
        else if (isOndial && index == sentences.Length - 1)
        {
            // Fin du dialogue
            isOndial = false;
            index = 0;
            manager.textDisplay.text = ""; 
            manager.nameDisplay.text = ""; 
            manager.DialogueHolder.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canDial = true;
            Debug.Log("Le joueur est dans la zone de dialogue.");
        }
    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Cursor.visible = false;

            canDial = false;
        }
    }
}
