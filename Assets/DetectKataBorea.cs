using System;
using UnityEngine;

public class DetectKataBorea : MonoBehaviour
{
    public int jauge = 0;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("caca");
    }

    void Update()
    {
    }
}
