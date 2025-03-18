using UnityEngine;
using UnityEngine.SceneManagement;

public class ThreeAttack : EnemyBase
{

    public GameObject attackDrop;
    private Transform target;
    //private Animator anim;
    public ListKatara listKatara;
    public ListAttaque listAttaque;
    private Rigidbody2D rb;

    void Start()
    {
        //anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        listKatara = target.GetComponent<ListKatara>();
        rb = GetComponent<Rigidbody2D>();
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
            GetComponent<SpriteRenderer>().flipX = direction.x < 0;
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
                    PlayerPrefs.SetInt("Loaded", 1);                }
                else
                {
                    //anim.speed = 1;

                    Debug.Log("false capture");
                }
            }
        }
    }
       

    
}

