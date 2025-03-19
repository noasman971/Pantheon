using UnityEngine;
using UnityEngine.SceneManagement;

public class TwoAttack: EnemyBase
{

    /// <summary>
    /// If the enemy health is superior to 80% do the attack1
    /// Else do attack2
    /// </summary>
    protected override void Attack()
    {
        
        stats.isAttacking = true;
        if (stats.health >= stats.maxHealth * (0.8))
        {
            stats.atk1 = true;
            stats.atk2 = false;
            anim.Play("Attack");
        }
        else
        {

            stats.atk1 = false;
            stats.atk2 = true;
            anim.Play("Attack2");
            


        }
        stats.lastAttackTime = Time.time;
    }

   
    
    
       

}
