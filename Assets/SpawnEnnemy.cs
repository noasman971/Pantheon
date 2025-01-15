using UnityEngine;

public class SpawnEnnemy : MonoBehaviour
{
    public Sprite ennemy;
    void Start()
    {

    }

    void Update()
    {   
        ennemy = Resources.Load<Sprite>("Assets/zone1/katara/Canines/Canine_Gray_Full_FX.png");

        GetComponent<SpriteRenderer>().sprite = ennemy;

        
    }
}
