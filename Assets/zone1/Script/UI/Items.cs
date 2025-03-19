using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite icon;
    public string nameItem;

    public int minamount;
    public int maxamount = 0;
    public int amount;

    void Start()
    {
        if (maxamount==0)
        {
            maxamount = minamount;
        }


        if (minamount != 0)
        {
            amount = Random.Range(minamount, maxamount);
        }
    }
    
    public bool isUsed;

    public virtual void AddItemToInventory()
    {
        var haveFoundItem = false;

        for (int i = 0; i < Inventory.instance.inventories.Length; i++)
        {
            if(Inventory.instance.inventories[i] != null)
            {
                if(Inventory.instance.inventories[i].nameItem == this.nameItem)
                {
                    if(Inventory.instance.inventories[i].amount < 99)
                    {
                        haveFoundItem = true;
                        Inventory.instance.inventories[i].amount += amount;
                        break;
                    }
                }
            }
        }

        if(haveFoundItem == false)
        {
            for (int i = 0; i < Inventory.instance.inventories.Length; i++)
            {
                if(Inventory.instance.inventories[i] == null)
                {
                    Inventory.instance.inventories[i] = this;
                    break;
                }
            }
        }

        if (haveFoundItem) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (isUsed)
            {
                return;
            }

            GetComponent<SpriteRenderer>().enabled = false;

            foreach (var item in GetComponents<BoxCollider2D>())
            {
                item.isTrigger = true;
            }

            isUsed = true;

            AddItemToInventory();

            Inventory.instance.LoadInventory();
        }
    }

}