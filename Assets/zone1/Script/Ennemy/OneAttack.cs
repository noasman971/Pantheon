using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class OneAttack : EnemyBase
{
    //private Animator anim;
    public Transform target;
    public ListKatara listKatara;
    public ListAttaque listAttaque;
    public GameObject attackDrop;
    private Rigidbody2D rb;
    
    void Start()
    {
        //anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        listKatara = target.GetComponent<ListKatara>();
        listAttaque = target.GetComponent<ListAttaque>();
        rb = GetComponent<Rigidbody2D>();

    }
    
    
    void Update()
    {
        if (!stats.isDead)
        {

            if (stats.health <= 0)
            {
                stats.isDead = true;
            }
            
            if (stats.isAttacking)
            {
                return;
            }

            if (stats.canAttack && Time.time >= stats.lastAttackTime + stats.attackCooldown)
            {
                Attack();
            }
            Vector2 direction = (target.position - transform.position).normalized;
            if (stats.gethit)
            {
                anim.Play("Hit");
            }
            else
            {   
                anim.Play("Run");
                

                rb.linearVelocity = direction * stats.speed;
                
            }

            if (stats.spriteReverse)
            {
                GetComponent<SpriteRenderer>().flipX = direction.x < 0;

            }
            else
            {
                
                GetComponent<SpriteRenderer>().flipX = direction.x > 0;

            }

        }
        
        
        if (stats.isDead)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            anim.Play("Dead");
            if (anim.speed == 0 && Input.GetKeyDown(KeyCode.M))
            {
                anim.speed = 1;
                

            }

            if (anim.speed == 0 && Input.GetKeyDown(KeyCode.C))
            {
                if (Randoms())
                {
                    Debug.Log("true capture");
                    listKatara.AddKatara(gameObject.name);
                    if (!listAttaque.attack.Contains(attackDrop.name))
                    {
                        listAttaque.AddAttack(attackDrop.name);
                    }
                    Destroy(gameObject);
                    SceneManager.LoadScene(PlayerPrefs.GetString("scene"));
                }
                else
                {
                    //anim.speed = 1;

                    Debug.Log("false capture");
                }
            }
        }
    }
    
    protected new void Attack()
    {
        
        stats.isAttacking = true;
        stats.atk1 = true;

        anim.Play("Attack");
        stats.lastAttackTime = Time.time;
    }
    


    
  
    
}