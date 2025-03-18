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

    /// <summary>
    /// Destroy the attack object after finish his animation
    /// </summary>
    void EndAnimation()
    {
        playerStats.isAttacking = false;
        Destroy(gameObject);

    }


    /// <summary>
    /// Give the original speed of the ennemy
    /// </summary>
    void BlockEnnemy()
    {
        stats.isAttacking = false;
        stats.speed = speed;
    }
    
    

    /// <summary>
    /// If we have enough stamina throw the attack and change his direction belong to the direction of the player
    /// </summary>
    /// <param name="playerRef">The player GameObject who will attack.</param>
    public void Attack(GameObject playerRef)
    {
        PlayerStats playerStats = playerRef.GetComponent<PlayerStats>();
        if (playerStats.currentstamina > cost)
        {
            playerStats.isAttacking = true;
            GameObject newSpell = Instantiate(this.gameObject, playerRef.transform.position, Quaternion.identity);
            SpriteRenderer spell = newSpell.GetComponent<SpriteRenderer>();

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




}
