using System.Net;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{

    public StaminaBar staminaBar;
    public PlayerStats playerStats;
    public bool paused;
    
    /// <summary>
    /// Sets the stamina bar to the correct values.
    /// </summary>
    void Start()
    {
        playerStats.currentstamina =playerStats. maxstamina;
        staminaBar.SetMaxStamina(playerStats.maxstamina);
        
    }
    
    
    /// <summary>
    /// Updates the player's stamina by calling the RegenStamina method every frame.
    /// </summary>
    void Update()
    {
        RegenStamina(playerStats.regenStamina);
        
    }
    
    /// <summary>
    /// Use stamina and update the staminabar
    /// </summary>
    /// <param name="stamina">The amount of stamina to subtract from the player's current stamina.</param>
    public void Usestamina(float stamina)
    
    {
        if(playerStats.currentstamina >= 0)
        {
            playerStats.currentstamina -= stamina;
            staminaBar.SetStamina(playerStats.currentstamina);
            
        }
  
        
    }
    
    /// <summary>
    /// Regenerate the stamina to his maximum
    /// </summary>
    /// <param name="regenStamina">The amount of stamina to add from the player's current stamina.</param>
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
