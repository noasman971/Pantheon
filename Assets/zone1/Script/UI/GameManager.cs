using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject inventory;


    void Start()
    {
        inventory.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            
            inventory.SetActive(!inventory.activeSelf);
        }
    }
}
