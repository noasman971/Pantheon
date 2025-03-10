using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class Hero : MonoBehaviour
{
    
    Rigidbody2D rb;
    public PlayerStats playerStats;
    Vector2 dir;
    Animator anim;
    

    private PlayerStamina playerStamina;
    
    
    void Start()
    {
        playerStats.canDash = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerStamina = GetComponent<PlayerStamina>();
        
    }

    
    


    public float SpeedPlayer()
    {
        if((Input.GetKey(KeyCode.Space) || (Input.GetKey(KeyCode.Joystick1Button4))) && (playerStats.currentstamina >= playerStats.runCost))
        {
            playerStats.currentSpeed = playerStats.speed_run;
            playerStamina.Usestamina(playerStats.runCost);
        }
        else if (playerStats.currentstamina < playerStats.runCost && (Input.GetKey(KeyCode.Space)))
        {
            playerStats.currentSpeed = 0f;
        }
        
        else
        {
            playerStats.currentSpeed = playerStats.speed;
        }
        return playerStats.currentSpeed;
    }
    void FixedUpdate()
    {
        if (!playerStats.isEsquive)
        {

            float currentSpeed = SpeedPlayer();
            rb.MovePosition(rb.position + dir * currentSpeed * Time.fixedDeltaTime);        }
        
        else
        {
            playerStats.canDash = false;
            StartCoroutine(Dash());
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            playerStats.currenthealth = 200;
        }
        if (playerStats.isEsquive)
        {
            return;
        }
        
        if (!playerStats.moveBlock)
        {
            dir.x = Input.GetAxis("Horizontal");
            dir.y = Input.GetAxis("Vertical");
        }
        else
        {
            dir.x = 0;
            dir.y = 0;
        }
        

        if (playerStats.isAttacking)
        {
            return;
        }

    
        

        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKey(KeyCode.JoystickButton1)) && playerStats.canDash && playerStats.currentstamina >= playerStats.dashCost)
        {
            playerStats.isEsquive = true;
            playerStamina.Usestamina(playerStats.dashCost);
            
        }
        SetParam();
        SpeedPlayer();
    }

    void SetParam ()
    {
        if (dir.x == 0 && dir.y==0)
        {
            anim.SetInteger("dir", 0);
        }
        else if (dir.y>0)
        {
            anim.SetInteger("dir", 3);
        }
        else if (dir.x>0)
        {
            anim.SetInteger("dir", 1);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (dir.x<0)
        {
            anim.SetInteger("dir", 1);
            GetComponent<SpriteRenderer>().flipX = false; 
        }
        else if (dir.y<0)
        {
            anim.SetInteger("dir", 2);
        }
    }

    private IEnumerator Dash(){
        rb.MovePosition(rb.position + dir * playerStats.speed_esquive * Time.fixedDeltaTime);
        yield return new WaitForSeconds(playerStats.dashDuration);
        playerStats.isEsquive = false;
        yield return new WaitForSeconds(playerStats.dashCoolDown);
        playerStats.canDash = true;
    }





}

