using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider slider;
    
    
    /// <summary>
    /// Set the maximum of the stamina bar
    /// </summary>
    /// <param name="stamina">Amount of the maximum stamina</param>
    public void SetMaxStamina(float stamina)
    {
        slider.maxValue = stamina;
        slider.value = stamina;
        
    }

    /// <summary>
    /// Set the staminabar to the amount we want
    /// </summary>
    /// <param name="stamina">Amount of the stamina</param>
    public void SetStamina(float stamina)
    {
        slider.value = stamina;
        
    }


}
