using UnityEngine;
using UnityEngine.SceneManagement;
// classe de la vie du joueur
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            TakeDamage(10f);
        }

        if (currenthealth <= 0)
        {
            SceneManager.LoadScene("Badis");
        }
    }

    void TakeDamage(float damage)
    
    {
        if(currenthealth >= 0)
        {
            currenthealth -= damage;
            healthbar.SetHealth(currenthealth);
            
        }
  
        
    }
}
