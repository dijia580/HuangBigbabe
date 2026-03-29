using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_colliderControl : MonoBehaviour
{
    private string ENEMY = "Enemy";
    public bool hasEnemy=false;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        if (collision.CompareTag(ENEMY))
        {
            hasEnemy = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag(ENEMY))
        {
            hasEnemy = false;
        }
    }
}
