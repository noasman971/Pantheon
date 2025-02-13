using Unity.VisualScripting;
using UnityEngine;

public class Spell1 : MonoBehaviour
{
    private Animator anim;
    private PlayerStats playerStats;
    public float cost;

    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        // si l'animation est flip en x alors play l'animation reverse
    }

    void EndAnimation()
    {
        playerStats.isAttacking = false;
        Destroy(gameObject);

    }



  
}
