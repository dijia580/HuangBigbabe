using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiderControl : MonoBehaviour
{
   
    private Enemy enemy;
    private string PLAYER = "Player";
    
    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag(PLAYER))
        {
            enemy.PlayerInRange = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(PLAYER))
        {
            enemy.PlayerInRange = false;
        }
    }
    
}
