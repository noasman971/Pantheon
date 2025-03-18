using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KataraInventory : MonoBehaviour
{

    public ListAttaque listAttaque;
    public ListKatara listKatara;
    public List<GameObject> Katara;
    public List<GameObject> Attack;
    public GameObject target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        listAttaque = target.GetComponent<ListAttaque>();
        listKatara = target.GetComponent<ListKatara>();

    }
    
    /// <summary>
    /// Search if we have the katara or attack name
    /// if its true change its color to white
    /// </summary>
    private void Update()
    {
        foreach (GameObject katara in Katara)
        {
            if (katara == null) continue;

            Image image = katara.GetComponent<Image>();
    

            foreach (string kataraname in listKatara.katara)
            {
                if (kataraname == katara.name)
                {
                    image.color = Color.white; 
                }
            }
        }

        foreach (GameObject attack in Attack)
        {
            if (attack == null) continue;
            
            Image image = attack.GetComponent<Image>();

            foreach (string attackname in listAttaque.attack)
            {
                if (attackname == attack.name)
                {
                    image.color = Color.white;
                }
            }
        }
    }
}
