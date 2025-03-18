using UnityEngine;

public class Hitbox : MonoBehaviour
{
    PlayerStamina playerStamina;
    Spell1 spell1;
    public int damage = 30;
    public float speedAfter = 0;
    Stats stats;
    public GameObject parent;
    
    /// <summary>
    /// Use stamina when the attack is used
    /// </summary>
    void Start()
    {
        playerStamina = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStamina>();
        spell1 = parent.GetComponent<Spell1>();
        
        playerStamina.Usestamina(spell1.cost);

    }



    /// <summary>
    /// Detect collision between attack and ennemy
    /// Ignores collisions between the player and attacks layers,
    /// and applies damage to the enemy when colliding.
    /// </summary>
    void OnCollisionEnter2D(Collision2D collision)
    {
        int playerLayer = LayerMask.NameToLayer("Player");
        int attackLayer = LayerMask.NameToLayer("Attack");

        Physics2D.IgnoreLayerCollision(playerLayer, attackLayer, true);
        if (collision.gameObject.tag == "Ennemy")
        {
            Stats stats = collision.gameObject.GetComponent<Stats>();
            stats.isAttacking = false;

            stats.gethit = true;
            if (stats.health > 0)
            {
                stats.health -= damage;

            }
            else
            {
                stats.health = 0;
            }


            Debug.Log(stats.health);

        }
    }

    
    /// <summary>
    /// Detect trigger collision with an enemy. It applies damage to the enemy and changes its speed,
    /// </summary>
    void OnTriggerEnter2D(Collider2D collision)
    {

            if (collision.gameObject.tag == "Ennemy")
            {
                stats = collision.gameObject.GetComponent<Stats>();
                stats.speed = speedAfter;
                stats.isAttacking = false;

                stats.health -= damage;
                
                Debug.Log(stats.health);
            }
        
    }



 



    

}