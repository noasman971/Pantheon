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

    /// <summary>
    /// Checks which attacks the player has and adds them to the 'myAttacks' list.
    /// </summary>
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

    /// <summary>
    /// It checks the player's available attacks and allows the player to use them
    /// by pressing the corresponding keys (Y, U, I, J, K, L).
    /// </summary>
    void Spell()
    {
        if (myAttacks.Count > 0 && myAttacks[0] != null)
        {
            Attackable attackScript = myAttacks[0].GetComponent<Attackable>();

            if (Input.GetKeyDown(KeyCode.Y))
            {
                attackScript.Attack(this.gameObject);
            }
        }

        if (myAttacks.Count > 1 && myAttacks[1] != null)
        {
            Attackable attackScript = myAttacks[1].GetComponent<Attackable>();

            if (Input.GetKeyDown(KeyCode.U))
            {
                attackScript.Attack(this.gameObject);
            }
        }
        
        if (myAttacks.Count > 2 && myAttacks[2] != null)
        {
            Attackable attackScript = myAttacks[2].GetComponent<Attackable>();

            if (Input.GetKeyDown(KeyCode.I))
            {
                attackScript.Attack(this.gameObject);
            }
        }
        
        if (myAttacks.Count > 3 && myAttacks[3] != null)
        {
            Attackable attackScript = myAttacks[3].GetComponent<Attackable>();

            if (Input.GetKeyDown(KeyCode.J))
            {
                attackScript.Attack(this.gameObject);
            }
        }
        
        if (myAttacks.Count > 4 && myAttacks[4] != null)
        {
            Attackable attackScript = myAttacks[4].GetComponent<Attackable>();

            if (Input.GetKeyDown(KeyCode.K))
            {
                attackScript.Attack(this.gameObject);
            }
        }
        
        if (myAttacks.Count > 5 && myAttacks[5] != null)
        {
            Attackable attackScript = myAttacks[5].GetComponent<Attackable>();

            if (Input.GetKeyDown(KeyCode.L))
            {
                attackScript.Attack(this.gameObject);
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


 
}
