using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class Hero : MonoBehaviour
{
    
    Rigidbody2D rb;
    public float speed_run = 5f;
    public float speed  = 3f;
    public float speed_esquive = 15f;
    public float runCost = 2f;
    private float currentSpeed;
    public GameObject firstspell;
    public bool isAttacking = false;
    
    
    Vector2 dir;
    Animator anim;
    
    public bool isEsquive = false;

    public bool canDash = true;
    public float dashDuration = 0.2f;
    public float dashCoolDown = 1f;
    public float dashCost = 110f;
    
    private PlayerStamina playerStamina;
    void Start()
    {
        canDash = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerStamina = GetComponent<PlayerStamina>();
    }

    
    


    public float SpeedPlayer()
    {
        if((Input.GetKey(KeyCode.Space) || (Input.GetKey(KeyCode.Joystick1Button4))) && (playerStamina.currentstamina >= runCost))
        {
            currentSpeed = speed_run;
            playerStamina.Usestamina(runCost);
        }
        else if (playerStamina.currentstamina < runCost && (Input.GetKey(KeyCode.Space)))
        {
            currentSpeed = 0f;
        }
        
        else
        {
            currentSpeed = speed;
        }
        return currentSpeed;
    }
    void FixedUpdate()
    {
        if (!isEsquive)
        {

            float currentSpeed = SpeedPlayer();
            rb.MovePosition(rb.position + dir * currentSpeed * Time.fixedDeltaTime);        }
        
        else
        {
            canDash = false;
            StartCoroutine(Dash());
        }
    }

    void Update()
    {
        if (isEsquive)
        {
            return;
        }

        if (!isAttacking)
        {
            dir.x = Input.GetAxis("Horizontal");
            dir.y = Input.GetAxis("Vertical");
        }
        else
        {
            dir.x = 0;
            dir.y = 0;
        }
        Spell1();
        

        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKey(KeyCode.JoystickButton1)) && canDash && playerStamina.currentstamina >= dashCost)
        {
            isEsquive = true;
            playerStamina.Usestamina(dashCost);
            
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
        rb.MovePosition(rb.position + dir * speed_esquive * Time.fixedDeltaTime);
        yield return new WaitForSeconds(dashDuration);
        isEsquive = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }

    private void Spell1()
    {
        if(Input.GetKeyDown(KeyCode.Y)){
            isAttacking = true;
            Debug.Log("touche appuyé");
            Instantiate( firstspell, transform.position, transform.rotation);
        }
    }



}

