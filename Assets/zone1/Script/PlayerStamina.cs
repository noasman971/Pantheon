using System.Net;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{

    public StaminaBar staminaBar;
    public PlayerStats playerStats;
    public bool paused;
    
    void Start()
    {
        playerStats.currentstamina =playerStats. maxstamina;
        staminaBar.SetMaxStamina(playerStats.maxstamina);
        
    }
    
    void Update()
    {
        RegenStamina(playerStats.regenStamina);
        
    }
    
    // Utilise de la stamina et met à jour la barre de stamina
    public void Usestamina(float stamina)
    
    {
        if(playerStats.currentstamina >= 0)
        {
            playerStats.currentstamina -= stamina;
            staminaBar.SetStamina(playerStats.currentstamina);
            
        }
  
        
    }
    
    // régénère la stamina jusqu'au maximum selon la variable en paramètre
    void RegenStamina(float regenStamina)
    {
        if (paused)
        {
            return;
        }
        else
        {
            if(playerStats.currentstamina <= playerStats.maxstamina)
            {
                playerStats.currentstamina += regenStamina;
                staminaBar.SetStamina(playerStats.currentstamina);
            
            }

            if (playerStats.currentstamina > playerStats.maxstamina)
            {
                playerStats.currentstamina = playerStats.maxstamina;
            }
            
        }

    }
    

}
