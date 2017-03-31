using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Sprite mySpriteAtas;
    public Sprite mySpriteBawah;
    public Sprite mySpriteKiri;
    public Sprite mySpriteKanan;
    public SpriteRenderer sptRndr;

    public int coin;

    Transform myTransform;
    public float speed = 100;
    Vector3 moveVector;
    public float maxDashCooldown;
    float dashCooldown;
    enum direction {Atas, Bawah, Kiri, Kanan}
    direction lastDir;

    public Rigidbody2D rigid;

    private void Awake()
    {
        moveVector = new Vector3(0, 0, 0);
    }

    // Use this for initialization
    void Start () {
        myTransform = transform;
        lastDir = direction.Bawah;
        dashCooldown = maxDashCooldown;
	}
	
	// Update is called once per frame
	void Update () {
        float horizontalMove = (speed * Input.GetAxis("Horizontal")) * Time.deltaTime;
        float verticalMove = (speed * Input.GetAxis("Vertical")) * Time.deltaTime;
        
        moveVector = new Vector3(horizontalMove, verticalMove, 0);
        // clamping moveVector speed per delta time
        moveVector = Vector3.ClampMagnitude(moveVector, (speed * Time.deltaTime));
        if(horizontalMove == 0 ^ verticalMove == 0)
        {
            if(horizontalMove > 0)
            {
                lastDir = direction.Kanan;
            }else if(horizontalMove < 0)
            {
                lastDir = direction.Kiri;
            }
            else if(verticalMove > 0)
            {
                lastDir = direction.Atas;
            }
            else if(verticalMove < 0)
            {
                lastDir = direction.Bawah;
            }

            // left rigth handling
            if(lastDir == direction.Kanan)
            {
                myTransform.localScale = new Vector3(-5, 5, 5);
            }else
            {
                myTransform.localScale = new Vector3(5, 5, 5);
            }

            // sprite handling
            switch(lastDir)
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
            Debug.Log("DASH");
        }
    }
}
