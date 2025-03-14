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
            //EnemyAttack2 enemyAttack2 = collision.gameObject.GetComponent<EnemyAttack2>();
            //enemyAttack2.gethit = true;
            GorgonAttack gorgonAttack = collision.gameObject.GetComponent<GorgonAttack>();
            Stats stats2 = collision.gameObject.GetComponent<Stats>();
            stats2.gethit = true;
            stats.health -= 30;


            Debug.Log(stats.health);

        }
    }



    

}