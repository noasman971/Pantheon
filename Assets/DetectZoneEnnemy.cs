using UnityEngine;

public class DetectZoneEnnemy : MonoBehaviour
{

    public GameObject parent;
    public Stats stats;

    void Start()
    {
        stats = parent.GetComponent<Stats>();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(gameObject.name + " entered");
        if (other.tag == "Player")
        {
            stats.playerDetected = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            stats.canAttack = false;
            stats.playerDetected = false;
        }
    }
}
