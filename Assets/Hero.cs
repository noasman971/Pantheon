using UnityEngine;

public class hero : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed  = 5f;
    Vector2 dir;

    Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
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
}

