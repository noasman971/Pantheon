using UnityEngine;
using UnityEngine.UI;  // Nécessaire pour accéder aux éléments UI comme Button

public class HideObjectOnClick : MonoBehaviour
{
    // Référence à l'objet que l'on veut rendre invisible
    public GameObject objectToHide;

    // Référence au bouton
    public Button hideButton;

    // Start is called before the first frame update
    void Start()
    {
        // S'assurer que le bouton a bien un écouteur d'événement
        hideButton.onClick.AddListener(HideObject);
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
