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

    
    
    
    
    
    protected void Attack()
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
    
    protected bool Randoms()
    {
        System.Random rnd = new System.Random();
        int random = rnd.Next(1, stats.dropRate);
        return random == 1;
    }

    

    protected void EndAttack()
    {
        stats.isAttacking = false;
        stats.atk1 = false;
        stats.atk2 = false;
        stats.special = false;
        
    }
    

    public void EndDeathAnimation()
    {
        anim.speed = 0;
        SceneManager.LoadScene(PlayerPrefs.GetString("scene"));
    }

    public void TimetoCapture()
    {
        anim.speed = 0;
    }

    public void EndGetHitAnimation()
    {
        stats.gethit = false;
    }
}