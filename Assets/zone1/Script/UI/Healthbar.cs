using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider slider;
    
    public Gradient gradient;
    public Image fill;
    
    
    /// <summary>
    /// Set the maximum of the health bar
    /// </summary>
    /// <param name="health">Amount of the maximum health</param>
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        
        fill.color = gradient.Evaluate(1f);
    }

    /// <summary>
    /// Set the healthbar to the amount we want
    /// </summary>
    /// <param name="health">Amount of the health</param>
    public void SetHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        
    }
    
    
}
