using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour {
    public int health;
    public int contactDmg;
    public int baseDmg;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.GetComponent<BoxCollider2D>().CompareTag("PlayerAttack"))
        {
            Destroy(this.gameObject);
        }
        /*
        else if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().subtractHealth(contactDmg);
        }*/
    }

}
