using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{

    public PlayerStats playerStats;
    public Healthbar healthbar;
    void Start()
    {
        playerStats.currenthealth = PlayerPrefs.GetFloat("currenthealth", playerStats.maxhealth);
        healthbar.SetMaxHealth(playerStats.maxhealth);
        healthbar.SetHealth(playerStats.currenthealth);
        
  
    }

    void Update()
    {   
        PlayerPrefs.SetFloat("currenthealth", playerStats.currenthealth);
        PlayerPrefs.Save(); 

        if (playerStats.currenthealth <= 0)
        {
            PlayerPrefs.SetFloat("currenthealth", playerStats.maxhealth);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Badis");
        }
    }

    public void TakeDamage(float damage)
    
    {
        if(playerStats.currenthealth >= 0)
        {
            playerStats.currenthealth -= damage;
            healthbar.SetHealth(playerStats.currenthealth);
            
        }
  
        
    }


}
