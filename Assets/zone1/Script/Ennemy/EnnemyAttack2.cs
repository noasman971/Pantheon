using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class EnemyAttack2 : EnemyBase
{
    //private Animator anim;
    private Transform target;
    public bool gethit = false;
    public ListKatara listKatara;
    public ListAttaque listAttaque;
    public GameObject attackDrop;
    
    
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        listKatara = target.GetComponent<ListKatara>();
        listAttaque = target.GetComponent<ListAttaque>();
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
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
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
                    Debug.Log("true capture");
                    listKatara.AddKatara(gameObject.name);
                    foreach (string attackname in listAttaque.attack)
                    {
                        if (attackDrop.name != attackname)
                        {
                            listAttaque.AddAttack(attackDrop.name);

                        }
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
    
    protected void Attack()
    {
        
        stats.isAttacking = true;
        stats.atk1 = true;

        anim.Play("Attack");
        stats.lastAttackTime = Time.time;
        Invoke("EndAttack", 1f);
    }
    


    
  
    
}