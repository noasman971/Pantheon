using UnityEngine;

public class Stats : MonoBehaviour
{
    
    public float health = 100f;
    public float maxHealth;
    
    public  float speed = 3f;
    public  float attackCooldown = 2f;
    
    public bool isDead = false;
    public bool isAttacking = false;
    public float lastAttackTime = 0f;

    public float damage_atk1;
    public float damage_atk2;
    public float damage_special;
    
    public bool atk1 = false;
    public bool atk2 = false;
    public bool special = false;

    public bool canSpecial = true;

    public bool canAttack;
    public bool gethit = false;
    public int dropRate = 5;

    public bool spriteReverse = false;

    void Start()
    {
        maxHealth = health;
    }
}
