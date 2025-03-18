using UnityEngine;
using UnityEngine.SceneManagement;

public class Spell1 : MonoBehaviour, Attackable
{
    public float cost;
    private Vector2 dir;
    public GameObject target;
    public PlayerStats playerStats;
    Stats stats;
    public bool flip = true;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        playerStats = target.GetComponent<PlayerStats>();
        if (SceneManager.GetActiveScene().name == "fight")
        {
            stats = GameObject.FindGameObjectWithTag("Ennemy").GetComponent<Stats>();
        }

        
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
        stats = GameObject.FindGameObjectWithTag("Ennemy").GetComponent<Stats>();

        stats.isAttacking = false;
        stats.speed = stats.maxSpeed;
    }
    
    

    /// <summary>
    /// If we have enough stamina throw the attack and change his direction belong to the direction of the player
    /// </summary>
    /// <param name="playerRef">The player GameObject who will attack.</param>
    public void Attack(GameObject playerRef)
    {
        PlayerStats playerStats = playerRef.GetComponent<PlayerStats>();
        if (playerStats.currentstamina > cost && SceneManager.GetActiveScene().name == "fight")
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
