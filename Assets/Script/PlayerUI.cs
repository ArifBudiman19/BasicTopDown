using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    public Player myPlayer;
    Text coinTxt;
    Text healthTxt;

	// Use this for initialization
	void Start () {
        coinTxt = transform.FindChild("CoinUI").GetComponent<Text>();
        healthTxt = transform.FindChild("HealthUI").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {

        // Coin UI manipulation
        //String tempCoins = myPlayer.getCoins().ToString();
        String tempCoins = "A";
        if (tempCoins.Length == 1)
        {
            tempCoins = "00" + tempCoins;
        }else if(tempCoins.Length == 2)
        {
            tempCoins = "0" + tempCoins;
        }
        coinTxt.text = tempCoins;


        // Health UI manipulation
        //healthTxt.text = "Health = " + myPlayer.getHealth().ToString();
	}
}
