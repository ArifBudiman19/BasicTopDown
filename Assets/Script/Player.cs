using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Sprite mySpriteAtas;
    public Sprite mySpriteBawah;
    public Sprite mySpriteKiri;
    public Sprite mySpriteKanan;
    public SpriteRenderer sptRndr;

    //attack collider
    public BoxCollider2D colAtkAtas;
    public BoxCollider2D colAtkBawah;
    public BoxCollider2D colAtkKiri;
    public BoxCollider2D colAtkKanan;

    bool onAttack = false;

    enum direction { Atas, Bawah, Kiri, Kanan }
    direction lastDir;

    Transform myTransform;
    Vector3 moveVector;
    public int maxHealth;
    public float speed = 100;
    public float maxDashCooldown;
    public Rigidbody2D rigid;

    // Private Atrb
    private int coin;
    private int health;
    private float dashCooldown;

    private void Awake()
    {
        moveVector = new Vector3(0, 0, 0);
        colAtkAtas.enabled = false;
        colAtkBawah.enabled = false;
        colAtkKiri.enabled = false;
        colAtkKanan.enabled = false;
    }

    // Use this for initialization
    void Start () {
        myTransform = transform;
        lastDir = direction.Bawah;
        dashCooldown = maxDashCooldown;
        health = maxHealth;
    }
	
	// Update is called once per frame
	void Update () {
        float horizontalMove = (speed * Input.GetAxis("Horizontal")) * Time.deltaTime;
        float verticalMove = (speed * Input.GetAxis("Vertical")) * Time.deltaTime;

        // player direction
        Vector2 myDirection = new Vector2(horizontalMove, verticalMove).normalized;

        moveVector = new Vector3(horizontalMove, verticalMove, 0);
        
        // clamping moveVector speed per delta time
        moveVector = Vector3.ClampMagnitude(moveVector, (speed * Time.deltaTime));

        // select last direction
        if(myDirection == Vector2.up)
        {
            lastDir = direction.Atas;
        }
        else if (myDirection == Vector2.down)
        {
            lastDir = direction.Bawah;
        }
        else if (myDirection == Vector2.left)
        {
            lastDir = direction.Kiri;
        }
        else if (myDirection == Vector2.right)
        {
            lastDir = direction.Kanan;
        }

        // Flip sprite
        switch (lastDir)
        {
            case direction.Kiri:
                sptRndr.flipX = false;
                break;
            case direction.Kanan:
                sptRndr.flipX = true;
                break;
        }

        // switch sprite
        switch (lastDir)
        {
            case direction.Atas:
                sptRndr.sprite = mySpriteAtas;
                break;
            case direction.Bawah:
                sptRndr.sprite = mySpriteBawah;
                break;
            case direction.Kiri:
                sptRndr.sprite = mySpriteKiri;
                break;
            case direction.Kanan:
                sptRndr.sprite = mySpriteKanan;
                break;
        }
         
        // move player
        myTransform.Translate(moveVector);

	}


    void FixedUpdate()
    {
        // Dash
        dashCooldown -= Time.deltaTime;
        if (Input.GetButtonDown("Dash") && dashCooldown < 0)
        {
            dashCooldown = maxDashCooldown;
            switch (lastDir)
            {
                case direction.Atas:
                    rigid.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
                    break;
                case direction.Bawah:
                    rigid.AddForce(Vector2.down * 20, ForceMode2D.Impulse);
                    break;
                case direction.Kiri:
                    rigid.AddForce(Vector2.left * 20, ForceMode2D.Impulse);
                    break;
                case direction.Kanan:
                    rigid.AddForce(Vector2.right * 20, ForceMode2D.Impulse);
                    break;
            }
            //Debug.Log("DASH");
        }

        // Attack
        if (Input.GetButtonDown("Fire1") && !onAttack)
        {
            attack();  
        }
    }

    public void attack()
    {
        switch (lastDir)
        {
            case direction.Atas:
                StartCoroutine(attacking(colAtkAtas)); 
                break;
            case direction.Bawah:
                StartCoroutine(attacking(colAtkBawah));
                break;
            case direction.Kiri:
                StartCoroutine(attacking(colAtkKiri));
                break;
            case direction.Kanan:
                StartCoroutine(attacking(colAtkKanan));
                break;
        }
    }

    private IEnumerator attacking(BoxCollider2D collider)
    {
        onAttack = true;
        collider.enabled = true;
        yield return new WaitForSeconds(.2f);
        collider.enabled = false;
        onAttack = false;
    }

    public int getCoins()
    {
        return coin;
    }

    public void addCoins(int value)
    {
        coin += value;

        if (coin > 999) coin = 999;
    }

    public bool subtractCoins(int value)
    {
        if (coin - value < 0) return false;

        coin -= value;
        return true;
    }

    public int getHealth()
    {
        return health;
    }

    public void addHealth(int value)
    {
        health += value;

        if (health > maxHealth) health = maxHealth;
    }

    public bool subtractHealth(int value)
    {
        health -= value;
        return health <= 0;
    }

}
