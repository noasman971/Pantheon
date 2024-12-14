using UnityEngine;

// classe de la stamina du joueur
public class PlayerStamina : MonoBehaviour
{
    public float regenStamina = 0.5f;
    public int maxstamina = 200;
    public float currentstamina;
    
    public StaminaBar staminaBar;
    
    
    void Start()
    {
        currentstamina = maxstamina;
        staminaBar.SetMaxStamina(maxstamina);
        
    }
    
    // régénère la stamina  toutes les frames
    void Update()
    {
        
        RegenStamina(regenStamina);

        
    }
    
    // Utilise de la stamina et met à jour la barre de stamina
    public void Usestamina(float stamina)
    
    {
        if(currentstamina >= 0)
        {
            currentstamina -= stamina;
            staminaBar.SetStamina(currentstamina);
            
        }
  
        
    }
    
    // régénère la stamina jusqu'au maximum selon la variable en paramètre
    void RegenStamina(float regenStamina)
    {
        if(currentstamina <= maxstamina)
        {
            currentstamina += regenStamina;
            staminaBar.SetStamina(currentstamina);
            
        }

        if (currentstamina > maxstamina)
        {
            currentstamina = maxstamina;
        }
    }
    

}
