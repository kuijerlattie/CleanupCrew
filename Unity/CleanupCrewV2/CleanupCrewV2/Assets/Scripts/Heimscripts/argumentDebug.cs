using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class argumentDebug : MonoBehaviour {
    Text userid;
    Text gameid;
    Text username;
    Text gametime;
    Text conurl;

    Arguments args;
	// Use this for initialization
	void Start () {
        args = GetComponent<Arguments>();

        userid = GameObject.Find("Text1").GetComponent<Text>();
        gameid = GameObject.Find("Text2").GetComponent<Text>();
        username = GameObject.Find("Text3").GetComponent<Text>();
        gametime = GameObject.Find("Text4").GetComponent<Text>();
        conurl = GameObject.Find("Text5").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        userid.text = args.getUserID().ToString();
        gameid.text = args.getGameID().ToString();
        username.text = args.getUsername().ToString();
        gametime.text = args.getGameTime().ToString();
        conurl.text = args.getConURL().ToString();

	}
}
