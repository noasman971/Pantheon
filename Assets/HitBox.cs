using Unity.VisualScripting;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    PlayerStamina playerStamina;
    Spell1 spell1;

    void Start()
    {
        playerStamina = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStamina>();
        spell1 = GameObject.FindGameObjectWithTag("Attack").GetComponent<Spell1>();
        playerStamina.Usestamina(spell1.cost);


    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Ennemy")
        {
            Stats stats = collision.gameObject.GetComponent<Stats>();
            stats.health -= 30;

            Debug.Log(stats.health);
            
        }

    }
  
}