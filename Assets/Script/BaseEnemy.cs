using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour, IDamage {
    public int health;
    public int contactDmg;
    public int baseDmg;

    public void onDamage(int damage)
    {
        health -= damage;
        
        //Die Implement prototype
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("ASDSAD");
        }
    }

}
