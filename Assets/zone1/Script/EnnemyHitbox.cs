using UnityEngine;

public class EnnemyHitbox : MonoBehaviour
{

    public GameObject player;
    public GameObject parent;
    public PlayerHealth playerHealth;
    public Stats stats;
    public float damage;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        stats = parent.GetComponent<Stats>();

    }
    
    
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            Debug.Log(gameObject.name);
            if (gameObject.name == "Capsule")
            {
                
                
                if (stats.atk1)
                {
                    damage = stats.damage_atk1;
                }

                if (stats.atk2)
                {
                    damage = stats.damage_atk2;
                }
         
                playerHealth.TakeDamage(damage);
                Debug.Log("Damage : " + damage);

            }
                
        }
       
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (gameObject.name == "SpecialZone")
        {
            stats.canAttack = true;
            if (stats.special)
            { 
                damage = stats.damage_special;
                Debug.Log("Damage : " + damage);
            }
            else
            {
                damage = 0;
            }
            playerHealth.TakeDamage(damage);

                
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.name == "SpecialZone")
        {
            stats.canAttack = false;

        }
    }
    
    
}
