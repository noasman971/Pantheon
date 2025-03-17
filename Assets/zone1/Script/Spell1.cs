using UnityEngine;

public class Spell1 : MonoBehaviour, Attackable
{
    public float cost;
    private Vector2 dir;
    public GameObject target;
    public PlayerStats playerStats;
    Stats stats;
    public float speed;
    public bool flip = true;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        playerStats = target.GetComponent<PlayerStats>();
        stats = GameObject.FindGameObjectWithTag("Ennemy").GetComponent<Stats>();
        speed = stats.maxSpeed;
    }


    void EndAnimation()
    {
        playerStats.isAttacking = false;
        Destroy(gameObject);

    }

    void UseStamina(float cost)
    {
        if (playerStats != null)
        {
            playerStats.currentstamina -= cost;

        }
    }


    void BlockEnnemy()
    {
        stats.isAttacking = false;
        stats.speed = speed;
    }

    public void Attack(GameObject playerRef)
    {
        GameObject newSpell = Instantiate(this.gameObject, playerRef.transform.position, Quaternion.identity);
        SpriteRenderer spell = newSpell.GetComponent<SpriteRenderer>();
        UseStamina(cost);

        if (flip)
        {
            Vector2 playerDirection;
            playerDirection.x = Input.GetAxisRaw("Horizontal");
            playerDirection.y = Input.GetAxisRaw("Vertical");

            if (playerDirection != Vector2.zero)
            {
                playerDirection.Normalize();
            }

            if (Mathf.Abs(playerDirection.y) > Mathf.Abs(playerDirection.x))
            {
                if (playerDirection.y > 0)
                {
                    spell.transform.Rotate(0, 0, 90);

                }
                else
                {
                    spell.transform.Rotate(0, 0, -90);

                }
            }
            else
            {
                if (playerDirection.x > 0)
                {
                    spell.transform.Rotate(0, 0, 0);
                }
                else
                {
                    spell.transform.Rotate(0, 0, 180);
                }
            }
        }
    }




}
