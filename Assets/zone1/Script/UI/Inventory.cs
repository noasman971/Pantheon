using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public GameObject inventoryCanvas;
    public Item[] inventories = new Item[24];
    public Transform inventoryDisplay;

    private Sprite blankItem;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        inventoryCanvas.SetActive(false);
        blankItem = inventoryDisplay.transform.GetChild(0).transform.Find("Icon").GetComponent<Image>().sprite;
    }

    public void LoadInventory()
    {
        for (int i = 0; i < inventories.Length; i++)
        {
            if(inventories[i] == null)
            {
                inventoryDisplay.transform.GetChild(i).transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = "";
                inventoryDisplay.transform.GetChild(i).transform.Find("Icon").GetComponent<Image>().sprite = blankItem;

                continue;
            }

            inventoryDisplay.transform.GetChild(i).transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = "" + inventories[i].amount;
            inventoryDisplay.transform.GetChild(i).transform.Find("Icon").GetComponent<Image>().sprite = inventories[i].icon;

        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
        }
    }
}