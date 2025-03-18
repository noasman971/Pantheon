using UnityEngine;

public class animherbe : MonoBehaviour
{
   Vector2 direction = new Vector2(1.5f,1.5f);

   /// <summary>
   /// The grass move in the direction of the player
   /// </summary>
   /// <param name="other">the gameobject who enter in collision trigger</param>
    private void OnTriggerEnter2D(Collider2D other)
    {       
        transform.Translate(direction * Time.deltaTime);
            
    }
    

}