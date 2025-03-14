using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBase : MonoBehaviour
{
    protected Animator anim;
    protected Transform target;
    [SerializeField] protected Stats stats;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        stats = GetComponent<Stats>();
    }

    
    
    
    
    
    
    protected bool Randoms()
    {
        System.Random rnd = new System.Random();
        int random = rnd.Next(1, stats.dropRate);
        return random == 1;
    }

    

    protected void EndAttack()
    {
        stats.atk1 = false;
        stats.atk2 = false;
        stats.special = false;
        stats.isAttacking = false;
    }
    

    public void EndDeathAnimation()
    {
        anim.speed = 0;
        SceneManager.LoadScene(PlayerPrefs.GetString("scene"));
    }

    public void TimetoCapture()
    {
        anim.speed = 0;
    }

    public void EndGetHitAnimation()
    {
        stats.gethit = false;
    }
}