using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PNJ : MonoBehaviour
{
    [SerializeField]
    string[] sentences;
    [SerializeField]
    string[] characterName;
    int index;
    private bool isOndial, canDial;
    
    // Utilisation correcte du Manager
    HUDManager Manager => HUDManager.instance;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canDial)
        {
            StartDialogue();
            // Utilisation correcte du bouton continue
            Manager.ContinueButton.GetComponent<Button>();
        }
    }

    public void StartDialogue()
    {
        // Afficher le dialogue
        Manager.DialogueHolder.SetActive(true);
        isOndial = true;
        TypingText(sentences);
    }

    void TypingText(string[] sentences) 
    {
        // Effacer le texte précédent
        Manager.nameDisplay.text = "";
        Manager.textDisplay.text = "";

        // Affichage du texte du personnage et de la phrase actuelle
        Manager.nameDisplay.text = characterName[index]; // Utilise l'index pour choisir le nom
        Manager.textDisplay.text = sentences[index];

        // Vérifier si la phrase est complètement affichée
        if (Manager.textDisplay.text == sentences[index]) 
        {
            // Afficher le bouton de continuation
            Manager.ContinueButton.SetActive(true); 
        }
    }

    public void NextLine()
    {
        // Masquer le bouton de continuation
        Manager.ContinueButton.SetActive(false);

        if (isOndial && index < sentences.Length - 1)
        {
            // Passer à la phrase suivante
            index++;
            Manager.textDisplay.text = "";
            TypingText(sentences);
        }  
        else if (!isOndial && index == sentences.Length - 1)
        {
            // Fin du dialogue
            isOndial = false;
            index = 0;
            Manager.textDisplay.text = ""; 
            Manager.nameDisplay.text = ""; 
            Manager.DialogueHolder.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canDial = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canDial = false;
        }
    }
}
