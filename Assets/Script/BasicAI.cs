using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour {

    Rigidbody2D body;
    SpriteRenderer myRndr;
    

    public GameObject player;
    public Sprite[] spirte = new Sprite[4];
    public float delay = 0.1f;
    public float speed = 5f;
    public LayerMask obstacles;

    float del;
    float angle;
    float distance;
    bool lookedPlayer;
    bool onChangeDirection;

    public Direction direction;
    Vector2 vDir = Vector2.zero;

    // Use this for initialization
    void Start () {
        del = delay;
        myRndr = this.GetComponent<SpriteRenderer>();
        body = this.GetComponent<Rigidbody2D>();
        onChangeDirection = false;


        if(body != null)
        {
            body.freezeRotation = true;
        }
	}

    void Update()
    {
        PatrolAI();
    }

    void FixedUpdate() {
        del -= Time.fixedDeltaTime;

        UpdateVectorDirection();

        // update spirte
        switch (direction)
        {
            case Direction.up:
                myRndr.sprite = spirte[(int)Direction.up];
                break;
            case Direction.left:
                myRndr.sprite = spirte[(int)Direction.left];
                break;
            case Direction.right:
                myRndr.sprite = spirte[(int)Direction.right];
                break;
            case Direction.down:
                myRndr.sprite = spirte[(int)Direction.down];
                break;
        }
        // cause only have left sprite
        if (direction == Direction.right)
        {
            myRndr.flipX = true;
        }
        else
        {
            myRndr.flipX = false;
        }

        VisualChecked();

        //Debug.Log(distance);
        //Debug.Log(angle);
    }

    public void UpdateVectorDirection()
    {
        switch (direction)
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
    }

    public void PatrolAI()
    {
        body.velocity = vDir * speed;
        if(!onChangeDirection)
            StartCoroutine(ChangeRandomDirection(Random.Range(2f, 5f)));
    }

    IEnumerator ChangeRandomDirection(float time)
    {
        onChangeDirection = true;
        yield return new WaitForSeconds(time);
        RandomDirection();
        onChangeDirection = false;
    }

    public void RandomDirection()
    {
        int rnd = Random.Range(0, 4);
        switch (rnd)
        {
            case (int)Direction.up:
                direction = Direction.up;
                break;
            case (int)Direction.left:
                direction = Direction.left;
                break;
            case (int)Direction.right:
                direction = Direction.right;
                break;
            case (int)Direction.down:
                direction = Direction.down;
                break;
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
            lookedPlayer = angle < 60 && distance < 6.0f && hit.collider.gameObject.CompareTag("Player");
        }else
        {
            lookedPlayer = angle < 60 && distance < 6.0f;
        }
    }
}

public enum AIState
{
    idle = 0,
    patrol = 1,
    attack = 2
}
