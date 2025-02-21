using UnityEngine;

public class animherbe : MonoBehaviour
{
   Vector3 direction = new Vector2(1.5f,1.5f);
   void Update()
   {
   }
    private void OnTriggerEnter2D(Collider2D other)
    {       
        transform.Translate(direction * Time.deltaTime);

        print("herbe touché");
  
            
    }
    

}