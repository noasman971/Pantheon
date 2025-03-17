using System;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using TMPro.Examples;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject InventoryCanvas;
    
    public static Inventory instance;
    
    public Items[] inventories = new Items[24];
    public Transform inventoryDisplay;

    private Sprite blankItem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryCanvas.SetActive(!InventoryCanvas.activeSelf);
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
       blankItem = inventoryDisplay.transform.GetChild(0).transform.Find("icon").GetComponent<Image>().sprite;
    }

    public void LoadInventory()
    {
        for (int i = 0; i < inventories.Length; i++)
        {
            if (inventories[i] == null)
            {
                inventoryDisplay.transform.GetChild(i).transform.Find("amount").GetComponent<TextMeshProUGUI>().text = "";
                inventoryDisplay.transform.GetChild(i).transform.Find("icon").GetComponent<Image>().sprite = blankItem;
                
                continue;
            }
            
            inventoryDisplay.transform.GetChild(i).transform.Find("amount").GetComponent<TextMeshProUGUI>().text = "" + inventories[i].amount;
            inventoryDisplay.transform.GetChild(i).transform.Find("icon").GetComponent<Image>().sprite = inventories[i].icon;

            
        }
        
    }
}
