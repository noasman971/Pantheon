using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public int maxhealth = 100;
    public float currenthealth;
    
    public Healthbar healthbar;
    void Start()
    {
        currenthealth = PlayerPrefs.GetFloat("currenthealth", maxhealth);
        healthbar.SetHealth(currenthealth);
  
    }

    void Update()
    {   
        PlayerPrefs.SetFloat("currenthealth", currenthealth);
        PlayerPrefs.Save(); 

        if (currenthealth <= 0)
        {
            PlayerPrefs.SetFloat("currenthealth", maxhealth);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Badis");
        }
        regenHealth();
    }

    public void TakeDamage(float damage)
    
    {
        if(currenthealth >= 0)
        {
            currenthealth -= damage;
            healthbar.SetHealth(currenthealth);
            
        }
  
        
    }

    public void regenHealth()
    {
        if (currenthealth <= maxhealth)
        {
            
            if (Input.GetKeyDown(KeyCode.O))
            {
                currenthealth += 10;
                healthbar.SetHealth(currenthealth);


            }
        }
        else if(currenthealth>maxhealth)
        {
            currenthealth = maxhealth;
        }
    }
}
