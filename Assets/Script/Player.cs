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
    bool onDash = false;

    enum direction { Atas, Bawah, Kiri, Kanan }
    direction lastDir;

    public int maxHealth;
    public float speed = 100;
    public float dashSpeed = 35;
    public float maxDashCooldown;
    public Rigidbody2D rigid;

    // Private Atrb
    private int coin;
    private int health;
    private float dashCooldown;
    private Vector2 myDirection;

    private void Awake()
    {
        colAtkAtas.enabled = false;
        colAtkBawah.enabled = false;
        colAtkKiri.enabled = false;
        colAtkKanan.enabled = false;
    }

    // Use this for initialization
    void Start () {
        lastDir = direction.Bawah;
        dashCooldown = maxDashCooldown;
        health = maxHealth;
    }
	
	// Update is called once per frame
	void Update () {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        // player direction
        myDirection = new Vector2(horizontalMove, verticalMove).normalized;
        
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
        if(!onDash)
            rigid.velocity = myDirection * speed;

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
                    StartCoroutine(dash(Vector2.up));
                    break;
                case direction.Bawah:
                    StartCoroutine(dash(Vector2.down));
                    break;
                case direction.Kiri:
                    StartCoroutine(dash(Vector2.left));
                    break;
                case direction.Kanan:
                    StartCoroutine(dash(Vector2.right));
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

    private IEnumerator dash(Vector2 dashDirection)
    {
        onDash = true;
        rigid.velocity = dashDirection * dashSpeed;
        yield return new WaitForSeconds(.5f);
        rigid.velocity = dashDirection * speed;
        onDash = false;
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
