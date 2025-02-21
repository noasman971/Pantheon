using UnityEngine;

public class Stats : MonoBehaviour
{
    
    public float health = 100f;
    
    public  float speed = 3f;
    public  float attackRange = 1.5f;
    public  float damageAttackRange = 2f;
    public  float verticalThreshold = 1f;
    public  float attackCooldown = 2f;
    
    public bool isDead = false;
    public bool isAttacking = false;
    public float lastAttackTime = 0f;


}
