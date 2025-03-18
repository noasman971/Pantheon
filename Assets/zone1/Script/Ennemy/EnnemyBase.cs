using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBase : MonoBehaviour
{
    protected Animator anim;
    [SerializeField] protected Stats stats;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        stats = GetComponent<Stats>();
    }

    
    
    
    
    /// <summary>
    /// If the enemy health is superior to 80% do the attack1
    /// If the ennemy health is superior to 40% and can do an attack special do a special attack
    /// Else do attack2
    /// </summary>
    protected virtual void Attack()
    {
        
        stats.isAttacking = true;
        if (stats.health >= stats.maxHealth * (0.8))
        {
            stats.atk1 = true;
            stats.atk2 = false;
            stats.special = false;
            anim.Play("Attack");
        }
        else if (stats.health >= stats.maxHealth * (0.4) && stats.canSpecial)
        {
            stats.atk1 = false;
            stats.atk2 = false;
            stats.special = true;
            anim.Play("Special");
            stats.canSpecial = false;


        }
        else
        {

            stats.atk1 = false;
            stats.special = false;
            stats.atk2 = true;
            anim.Play("Attack2");


        }
        stats.lastAttackTime = Time.time;
    }
    
    /// <summary>
    /// Random function to do one chance of a drop rate
    /// </summary>
    /// <returns> true if random = 1</returns>
    protected bool Randoms()
    {
        System.Random rnd = new System.Random();
        int random = rnd.Next(1, stats.dropRate);
        return random == 1;
    }

    
    /// <summary>
    /// EndAttack animation
    /// </summary>
    protected void EndAttack()
    {
        stats.isAttacking = false;
        stats.atk1 = false;
        stats.atk2 = false;
        stats.special = false;
        
    }
    

    /// <summary>
    /// EndDeath Animation
    /// </summary>
    public void EndDeathAnimation()
    {
        anim.speed = 0;       
        SceneManager.LoadScene(PlayerPrefs.GetString("scene"));
        PlayerPrefs.SetInt("Loaded", 1);

    }

    /// <summary>
    /// Stop the animation to have the time to capture
    /// </summary>
    public void TimetoCapture()
    {
        anim.speed = 0;
    }

    
    /// <summary>
    /// End get hit animation
    /// </summary>
    public void EndGetHitAnimation()
    {
        stats.gethit = false;
    }
}