using UnityEngine;
using System.Collections;

public class ScalePowerUp : MonoBehaviour {

    static Vector3 basescale;
    static GameManager manager;

	// Use this for initialization
	void Start () {
        
        manager = FindObjectOfType<GameManager>();
        basescale = manager.paddles[0].transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
	}

    static public void ScaleDown()
    {
        
        foreach (GameObject p in manager.paddles)
        {
            p.transform.localScale = basescale - new Vector3(GameSettings.PowerupPaddleDownscaleByS, 0, 0);
        }

    }

    static public void ScaleUp()
    {
        GameManager manager;
        manager = FindObjectOfType<GameManager>();

        foreach (GameObject p in manager.paddles)
        {
            p.transform.localScale = basescale + new Vector3(GameSettings.PowerupPaddleUpscaleByS, 0, 0);
        }
    }

    static public void ResetScale()
    {
        foreach (GameObject p in manager.paddles)
        {
            p.transform.localScale = basescale;
        }
    }
}
