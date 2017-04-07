using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour {

    Rigidbody2D body;
    SpriteRenderer myRndr;
    

    public GameObject player;
    public Sprite[] sprt;
    public float delay = 0.1f;
    public LayerMask obstacles;

    float del;
    float angle;
    float distance;
    bool lookedPlayer;

    public Direction eDir;
    public Vector2 vDir = Vector2.zero;

    // Use this for initialization
    void Start () {
        del = delay;
        myRndr = this.GetComponent<SpriteRenderer>();
	}

    // Update is called once per frame
    void FixedUpdate() {
        del -= Time.fixedDeltaTime;
       
        if (del < 0)
        {
            del = delay;
            //angle = Vector2.Distance(this.transform.position, player.transform.position);

            switch (eDir)
            {
                case Direction.up:
                    vDir = Vector2.up;
                    break;
                case Direction.left:
                    vDir = Vector2.left;
                    break;
                case Direction.right:
                    vDir = Vector2.right;
                    break;
                case Direction.down:
                    vDir = Vector2.down;
                    break;
            }
            VisualChecked();

            Debug.Log(distance);
            //Debug.Log(angle);
        }
	}

    public void VisualChecked()
    {
        Vector2 line = (player.transform.position - this.transform.position).normalized;
        angle = Mathf.Rad2Deg * Mathf.Acos(Vector2.Dot(vDir, line));
        distance = Vector2.Distance(this.transform.position, player.transform.position);

        Ray2D ray = new Ray2D((Vector2)this.transform.position + vDir * 1.0f, vDir * 8);
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + vDir * 1.0f, vDir * 8);
        if (hit.collider != null)
        {
            lookedPlayer = distance < 6.0f && hit.collider.gameObject.CompareTag("Player");
        }
    }
}

public enum AIState
{
    idle = 0,
    patrol = 1,
    attack = 2
}
