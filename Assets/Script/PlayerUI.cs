using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    public Player myPlayer;
    Text coinTxt;

	// Use this for initialization
	void Start () {
        coinTxt = transform.FindChild("CoinUI").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        coinTxt.text = myPlayer.coin.ToString();
	}
}
