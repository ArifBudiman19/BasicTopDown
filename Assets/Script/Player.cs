using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public BoxCollider2D[] collAtks;
    Direction lastDir;
    Rigidbody2D body;

    public int maxHealth, maxCoin;
    public float speed, dashSpeed, maxDashCooldown, maxAttackCooldown;
    public Direction idleDir;

    bool onAttack, onDash;

    private int coin, health;
    private float dashCooldown, attackCooldown;


    private void Awake()
    {
        body = this.GetComponent<Rigidbody2D>();

        foreach(BoxCollider2D col in collAtks)
        {
            col.enabled = false;
        }
    }

    private void Start()
    {
        lastDir = idleDir;
        dashCooldown = maxDashCooldown;
        attackCooldown = maxAttackCooldown;
        health = maxHealth;
    }

    private void Update()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (direction == Vector2.up) { lastDir = Direction.up; }
        else if (direction == Vector2.left) { lastDir = Direction.left; }
        else if (direction == Vector2.right) { lastDir = Direction.right; }
        else if (direction == Vector2.down) { lastDir = Direction.down; }

        if (!onDash)
        {
            body.velocity = direction * speed;
        }
    }

    private void FixedUpdate()
    {
        dashCooldown -= Time.fixedDeltaTime;
        if(Input.GetButtonDown("Dash") && dashCooldown < 0)
        {
            Dash();
        }

        attackCooldown -= Time.fixedDeltaTime;
        if ((Input.GetAxis("Attack") > 0) && !onAttack && attackCooldown < 0)
        {
            Attack();
        }
        
    }

    private void Attack()
    {
        attackCooldown = maxAttackCooldown;
        switch (lastDir)
        {
            case Direction.up:
                StartCoroutine(Attacking(Vector2.up));
                break;
            case Direction.left:
                StartCoroutine(Attacking(Vector2.left));
                break;
            case Direction.right:
                StartCoroutine(Attacking(Vector2.right));
                break;
            case Direction.down:
                StartCoroutine(Attacking(Vector2.down));
                break;
        }
    }

    private IEnumerator Attacking(Vector2 vectorDir)
    {
        BoxCollider2D collider = collAtks[(int)lastDir];
        onAttack = true;
        collider.enabled = true;
        collider.transform.localPosition = new Vector2(vectorDir.x * .17f, vectorDir.y * .2f);
        yield return new WaitForSeconds(.2f);
        collider.transform.localPosition = new Vector2(0,0);
        collider.enabled = false;
        onAttack = false;
    }

    private void Dash()
    {
        dashCooldown = maxDashCooldown;
        switch (lastDir)
        {
            case Direction.up:
                StartCoroutine(Dashing(Vector2.up));
                break;
            case Direction.left:
                StartCoroutine(Dashing(Vector2.left));
                break;
            case Direction.right:
                StartCoroutine(Dashing(Vector2.right));
                break;
            case Direction.down:
                StartCoroutine(Dashing(Vector2.down));
                break;
        }
    }
    
    private IEnumerator Dashing(Vector2 vectorDir)
    {
        onDash = true;
        body.velocity = vectorDir * dashSpeed;
        yield return new WaitForSeconds(.5f);
        body.velocity = vectorDir * speed;
        onDash = false;
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

    public int getCoins()
    {
        return coin;
    }

    public void addCoins(int value)
    {
        coin += value;

        if (coin > maxCoin) coin = maxCoin;
    }

    public bool subtractCoins(int value)
    {
        if (coin - value < 0) return false;

        coin -= value;
        return true;
    }
}
