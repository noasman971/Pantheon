using System;
using UnityEngine;

public class Spell2 : MonoBehaviour
{
    private Animator anim;
    private PlayerStats playerStats;
    public float cost;
    public float regen;
    Healthbar healthbar;
    PlayerStamina playerStamina;
    
    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        healthbar = FindObjectOfType<Healthbar>();
        playerStamina = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStamina>();
    }

    public void regenHealth()
    {
        
        if (playerStats.currenthealth <= playerStats.maxhealth)
        {
            playerStats.currenthealth += regen;
            playerStamina.Usestamina(cost);

            healthbar.SetHealth(playerStats.currenthealth);
            
        }
        if(playerStats.currenthealth>playerStats.maxhealth)
        {
            playerStats.currenthealth = playerStats.maxhealth;
        }
    }

    public void BlockPlayer()
    {
        playerStats.moveBlock = true;

    }
    void EndAnimation()
    {
        playerStats.isAttacking = false;
        playerStats.moveBlock = false;
        Destroy(gameObject);

    }


}
