using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 2f;
    public float speed;
    public int health = 100;

    public int moneyGain = 50;

    void Start(){
            speed = startSpeed;
    }
    public void TakeDamage(int amount)
    {
        health -=amount;

        if(health <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        PlayerStats.Money += moneyGain;
        Destroy(gameObject);    
    }

    

}
