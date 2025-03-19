using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBase : MonoBehaviour
{
    protected Animator anim;
    [SerializeField] protected Stats stats;
    Rigidbody2D rb;
    public Transform target;
    public ListKatara listKatara;
    public ListAttaque listAttaque;
    public GameObject attackDrop;

    
    protected void Awake()
    {
        anim = GetComponent<Animator>();
        stats = GetComponent<Stats>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        listKatara = target.GetComponent<ListKatara>();
        listAttaque = target.GetComponent<ListAttaque>();
        
        
    }

    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
        /// <summary>
    /// if the ennemy not dead:
    /// - attack the player
    /// - or get hit animation if he receive damage
    /// - else run
    /// if the ennemy is dead:
    /// - press M to kill him
    /// - press C to Capture him if we have the chance to
    /// </summary>
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
                stats.isAttacking = false;
                stats.gethit = false;
                Attack();
            }
            Vector2 direction = (target.position - transform.position).normalized;
            if (stats.gethit)
            {
                stats.isAttacking = false;

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
            if (SceneManager.GetActiveScene().name == "Mael")
            {
                anim.speed = 1;
            }
            else
            {
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
                        PlayerPrefs.SetInt("Loaded", 1);


                    }
                    else
                    {
                        anim.speed = 1;

                        Debug.Log("false capture");
                    }
                }
            }

            
        }
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