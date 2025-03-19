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
    
   
    /// <summary>
    /// If we enter in collision trigger of the player do damage
    /// </summary>
    /// <param name="other"> The colision of the player</param>
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

    /// <summary>
    /// If we stay in collision trigger of the player do damage
    /// </summary>
    /// <param name="other"></param>
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

    /// <summary>
    /// Stop Attack animation and do run animation
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.name == "SpecialZone")
        {
            stats.isAttacking = false;
            stats.canAttack = false;

        }
    }
    
    
    
}
