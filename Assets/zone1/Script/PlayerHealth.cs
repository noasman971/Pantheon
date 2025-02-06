using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public int maxhealth = 100;
    public float currenthealth;
    
    public Healthbar healthbar;
    void Start()
    {
        currenthealth = maxhealth;
        healthbar.SetMaxHealth(maxhealth);
        
    }

    void Update()
    {

        if (currenthealth <= 0)
        {
            SceneManager.LoadScene("Badis");
        }
    }

    public void TakeDamage(float damage)
    
    {
        if(currenthealth >= 0)
        {
            currenthealth -= damage;
            healthbar.SetHealth(currenthealth);
            
        }
  
        
    }
}
