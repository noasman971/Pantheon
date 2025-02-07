using UnityEngine;

public class Spell1 : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void EndAnimation()
    {
        
        
        /*
         * Attaque qui suit le joueur ou bloquer pendant 0.1 seconde au lieu de 1 seconde
         *  ne pas spammer les attaques
         * 
         */
        Hero hero = GameObject.FindWithTag("Player").GetComponent<Hero>();

        if (hero != null)
        {
            hero.isAttacking = false;
        }
        else
        {
            Debug.LogError("hero est null dans Spell1.EndAnimation()");
        }

        Destroy(gameObject);

    }
}
