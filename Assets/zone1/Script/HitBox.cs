using UnityEngine;

public class Hitbox : MonoBehaviour
{
    PlayerStamina playerStamina;
    Spell1 spell1;
    public int damage = 30;
    public float speedAfter = 0;
    Stats stats;

    void Start()
    {
        playerStamina = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStamina>();
        spell1 = GameObject.FindGameObjectWithTag("Attack").GetComponent<Spell1>();
        if (spell1 == null)
        {
            Debug.Log("No Spell 1");
        }

        if (playerStamina == null)
        {
            Debug.Log("No Player Stamina ");
        }
        playerStamina.Usestamina(spell1.cost);

    }




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
            stats.health -= damage;


            Debug.Log(stats.health);

        }
    }

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