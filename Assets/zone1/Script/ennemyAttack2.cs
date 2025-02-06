using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAttack2 : MonoBehaviour
{
    private Animator anim;
    private Transform target;
    public static float speed = 2.5f;
    public static float attackRange = 1.5f;
    public static float verticalThreshold = 1f;
    public static float attackCooldown = 2f;
    public float health = 100f;
    
    //private bool reallyDead = false;
    private bool isDead = false;
    private bool isAttacking = false;
    private float lastAttackTime = 0f;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update()
    {
        if (!isDead)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                health-=20;
            }

            if (health <= 0)
            {
                isDead = true;
            }
            float horizontalDistanceToPlayer = Mathf.Abs(transform.position.x - target.position.x);
            float verticalDistanceToPlayer = Mathf.Abs(transform.position.y - target.position.y);
            Debug.Log(verticalDistanceToPlayer);

            // Si on est en train d'attaquer, on attend la fin de l'animation
            if (isAttacking)
            {
                return;
            }
        
            // Vérifie si on peut attaquer
            if (horizontalDistanceToPlayer <= attackRange && 
                verticalDistanceToPlayer <= verticalThreshold && 
                Time.time >= lastAttackTime + attackCooldown && !isDead)
            {
                Attack();
            }
            else
            {   
                anim.Play("Run");
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            Vector2 direction = (target.position - transform.position).normalized;
            GetComponent<SpriteRenderer>().flipX = direction.x > 0;

        }
        
        if (isDead)
        {
            anim.Play("Dead");
            if (anim.speed == 0 && Input.GetKeyDown(KeyCode.M))
            {
                anim.speed = 1;
                

            }

        }

        
       
        

    }
    
    private void Attack()
    {
        isAttacking = true;
        anim.Play("Attack");
        lastAttackTime = Time.time;
        
        // Inflige les dégâts
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(15f);
        
        // On peut utiliser un AnimationEvent dans l'animation d'attaque pour appeler cette méthode
        Invoke("EndAttack", 1f); // Ajustez le délai selon la durée de votre animation
    }
    
    private void EndAttack()
    {
        isAttacking = false;
    }

    public void EndDeathAnimation()
    {
        //reallyDead = true;
        anim.speed = 0;
        SceneManager.LoadScene("FirstZone");

    }

    public void TimetoCapture()
    {
        anim.speed = 0;
    }

    
}