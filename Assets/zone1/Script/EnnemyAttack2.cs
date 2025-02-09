using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class EnemyAttack2 : MonoBehaviour
{
    private Animator anim;
    private Transform target;
    public Stats stats;
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    
    
    
    void Update()
    {
        if (!stats.isDead)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                stats.health-=20;
            }

            if (stats.health <= 0)
            {
                stats.isDead = true;
            }
            float horizontalDistanceToPlayer = Mathf.Abs(transform.position.x - target.position.x);
            float verticalDistanceToPlayer = Mathf.Abs(transform.position.y - target.position.y);

            // Si on est en train d'attaquer, on attend la fin de l'animation
            if (stats.isAttacking)
            {
                return;
            }
        
            // Vérifie si on peut attaquer
            if (horizontalDistanceToPlayer <= stats.attackRange && 
                verticalDistanceToPlayer <= stats.verticalThreshold && 
                Time.time >= stats.lastAttackTime + stats.attackCooldown && !stats.isDead)
            {
                Attack();
            }
            else
            {   
                anim.Play("Run");
                transform.position = Vector2.MoveTowards(transform.position, target.position, stats.speed * Time.deltaTime);
            }
            Vector2 direction = (target.position - transform.position).normalized;
            GetComponent<SpriteRenderer>().flipX = direction.x > 0;

        }
        
        if (stats.isDead)
        {
            anim.Play("Dead");
            if (anim.speed == 0 && Input.GetKeyDown(KeyCode.M))
            {
                anim.speed = 1;
                

            }

            if (anim.speed == 0 && Input.GetKeyDown(KeyCode.C))
            {
                if (Randoms())
                {
                    Debug.Log("ouiiiiiiiiiiiii");
                }
                else
                {
                    Debug.Log("aaaaaaa");
                }
            }

        }

        
       
        

    }

    private bool Randoms()
    {
        Random rnd = new Random();
        int random = rnd.Next(1, 3);
        return random == 1;
    }
    
    private void Attack()
    {
        stats.isAttacking = true;
        anim.Play("Attack");
        stats.lastAttackTime = Time.time;
        Invoke("EndAttack", 1f); // Ajustez le délai selon la durée de votre animation
    }

    public void HitPlayer()
    {
        float horizontalDistanceToPlayer = Mathf.Abs(transform.position.x - target.position.x);
        float verticalDistanceToPlayer = Mathf.Abs(transform.position.y - target.position.y);
        
        if (horizontalDistanceToPlayer <= stats.damageAttackRange &&
            verticalDistanceToPlayer <= stats.verticalThreshold)
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(15f);

        }
        else
        {
            return;
        }

    }
    
    private void EndAttack()
    {
        stats.isAttacking = false;
    }

    public void EndDeathAnimation()
    {
        anim.speed = 0;
        SceneManager.LoadScene("FirstZone");

    }

    public void TimetoCapture()
    {
        anim.speed = 0;
    }

    
}