
using UnityEngine;
using UnityEngine.SceneManagement;

public class OneAttack : EnemyBase
{


    
    /// <summary>
    /// Attack the Player
    /// </summary>
    protected override void Attack()
    {
        
        stats.isAttacking = true;
        stats.atk1 = true;

        anim.Play("Attack");
        stats.lastAttackTime = Time.time;
    }
    


    
  
    
}