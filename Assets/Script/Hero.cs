using System.Collections;
using UnityEngine;

public class hero : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed  = 5f;
    public float speed_esquive = 20f;
    Vector2 dir;
    Animator anim;
    public bool isEsquive = false;

    public bool canDash = true;
    public float dashDuration = 1f;
    public float dashCoolDown = 1f;
    void Start()
    {
        canDash = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!isEsquive) 
        {
            rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
        }
        else 
        {
            canDash = false;
            StartCoroutine(Dash());
        }
    }

    void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        // rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
        if (Input.GetKeyDown(KeyCode.E) && canDash==true)
        {
            isEsquive = true;
        }
        
        SetParam();
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
}

