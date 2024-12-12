using UnityEngine;

public class HideObjectOnClick : MonoBehaviour
{
    // Référence à l'objet que l'on veut rendre invisible
    public GameObject objectToHide;

    // Référence au bouton
    public Button hideButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Fonction qui rend l'objet invisible
    void HideObject()
    {
        if (objectToHide != null)
        {
            objectToHide.SetActive(false); // Rend l'objet inactif (invisible)
        }
    }
}
