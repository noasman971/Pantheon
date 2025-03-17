using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public ListAttaque listAttaque;
    public PlayerStats playerStats;
    private Vector2 dir;
    public List<GameObject> myAttacks;

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        listAttaque = GetComponent<ListAttaque>();
        HaveAttack();
    }

    void HaveAttack()
    {
        if (listAttaque.attack != null){
            foreach (GameObject attack in listAttaque.AllAttack)
            {
                foreach (string haveattack in listAttaque.attack)
                {
                    if (haveattack.Trim().ToLower() == attack.name.Trim().ToLower())
                    {
                        myAttacks.Add(attack);
                        
                    }
                }
            }
        }
    }

    void Spell()
    {
        if (myAttacks.Count > 0 && myAttacks[0] != null)
        {
            Attackable attackScript = myAttacks[0].GetComponent<Attackable>();

            if (Input.GetKeyDown(KeyCode.Y))
            {
                attackScript.Attack(this.gameObject);
                playerStats.isAttacking = true;
            }
        }

        if (myAttacks.Count > 1 && myAttacks[1] != null)
        {
            Attackable attackScript = myAttacks[1].GetComponent<Attackable>();

            if (Input.GetKeyDown(KeyCode.U))
            {
                attackScript.Attack(this.gameObject);
                playerStats.isAttacking = true;
            }
        }
        
        if (myAttacks.Count > 2 && myAttacks[2] != null)
        {
            Attackable attackScript = myAttacks[2].GetComponent<Attackable>();

            if (Input.GetKeyDown(KeyCode.I))
            {
                attackScript.Attack(this.gameObject);
                playerStats.isAttacking = true;
            }
        }
        
        if (myAttacks.Count > 3 && myAttacks[3] != null)
        {
            Attackable attackScript = myAttacks[3].GetComponent<Attackable>();

            if (Input.GetKeyDown(KeyCode.J))
            {
                attackScript.Attack(this.gameObject);
                playerStats.isAttacking = true;
            }
        }
        
        if (myAttacks.Count > 4 && myAttacks[4] != null)
        {
            Attackable attackScript = myAttacks[4].GetComponent<Attackable>();

            if (Input.GetKeyDown(KeyCode.K))
            {
                attackScript.Attack(this.gameObject);
                playerStats.isAttacking = true;
            }
        }
        
        if (myAttacks.Count > 5 && myAttacks[5] != null)
        {
            Attackable attackScript = myAttacks[5].GetComponent<Attackable>();

            if (Input.GetKeyDown(KeyCode.L))
            {
                attackScript.Attack(this.gameObject);
                playerStats.isAttacking = true;
            }
        }
    }

    private void Update()
    {
        if (myAttacks != null && myAttacks.Count > 0 && !playerStats.isAttacking)
        {
            Spell();
        }
    }


    /*
    private void Spell1()
    {
        if(Input.GetKeyDown(KeyCode.Y)){
            playerStats.isAttacking = true;
            GameObject newSpell = Instantiate(firstspell, transform.position, transform.rotation);
            SpriteRenderer spell = newSpell.GetComponent<SpriteRenderer>();
            
            spell.flipX = dir.x < 0;
            if (dir.y < 0)
            {
                spell.transform.Rotate(0, 0, -90);
            }

            if (dir.y>0)
            {
                spell.transform.Rotate(0, 0, 90);

            }
        }
    }

    
    private void Spell2()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            playerStats.isAttacking = true;
            GameObject newSpell = Instantiate(secondspell, transform.position, transform.rotation);
            Spell2 spell = newSpell.GetComponent<Spell2>();
            spell.regenHealth();
            
        }
    }
    */
}
