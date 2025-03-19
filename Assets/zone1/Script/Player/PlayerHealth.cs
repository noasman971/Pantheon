using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{

    public PlayerStats playerStats;
    public Healthbar healthbar;
    
    /// <summary>
    /// Sets the health bar to the correct values.
    /// </summary>
    void Start()
    {
        playerStats.currenthealth = PlayerPrefs.GetFloat("currenthealth", playerStats.maxhealth);
        healthbar.SetMaxHealth(playerStats.maxhealth);
        healthbar.SetHealth(playerStats.currenthealth);
        
  
    }
    
    /// <summary>
    /// Saves the player's current health to PlayerPrefs and loads the menu scene if the player's health is zero or less.
    /// </summary>
    void Update()
    {   
        // a chque frame sauvgarde la vie actuelle du joueur (le currenthealth est la clef de playerpref et que playerStats est la valeur a la quelle on veut mettre cette clef )
        PlayerPrefs.SetFloat("currenthealth", playerStats.currenthealth);
        PlayerPrefs.Save(); 

        if (playerStats.currenthealth <= 0)
        {
            PlayerPrefs.SetFloat("currenthealth", playerStats.maxhealth);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Menu");
        }
    }
    
    
    /// <summary>
    /// Reduces the player's health by the damage amount and updates the health bar.
    /// </summary>
    /// <param name="damage">The amount of damage to subtract from the player's current health.</param>
    public void TakeDamage(float damage)
    
    {
        if(playerStats.currenthealth >= 0)
        {
            playerStats.currenthealth -= damage;
            healthbar.SetHealth(playerStats.currenthealth);
            
        }
  
        
    }


}
