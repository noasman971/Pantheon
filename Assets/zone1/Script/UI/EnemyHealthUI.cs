using TMPro;
using UnityEngine;

public class EnemyHealthUI : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    Stats stats;


    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Ennemy").GetComponent<Stats>();

    }

    /// <summary>
    /// Every frame update the ennemy health UI
    /// </summary>
    void Update() {
  
        
        if (displayText != null)
        {
            displayText.text = "Health: " + stats.health.ToString() +" / " + stats.maxHealth.ToString(); ;

        }

    }
    

}
