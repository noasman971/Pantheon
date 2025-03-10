using UnityEngine;
using UnityEngine.SceneManagement;

public class GorgonAttack : EnemyBase
{

    public GameObject attackDrop;
    private Transform target;
    private Animator anim;
    public ListKatara listKatara;
    public ListAttaque listAttaque;

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

            if (stats.gethit)
            {
                anim.Play("hit");
            }
            else
            {   
                anim.Play("Run");
                transform.position = Vector2.MoveTowards(transform.position, target.position, stats.speed * Time.deltaTime);
            }
            Vector2 direction = (target.position - transform.position).normalized;
            GetComponent<SpriteRenderer>().flipX = direction.x < 0;
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
        anim.Play("Attack");
        stats.lastAttackTime = Time.time;
        Invoke("EndAttack", 1f);
    }

    
}

