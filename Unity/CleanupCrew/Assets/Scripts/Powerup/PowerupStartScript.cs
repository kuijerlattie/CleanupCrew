using UnityEngine;
using System.Collections;

public class PowerupStartScript : MonoBehaviour {

    PowerupManager pManager;
    public PowerupManager.PowerupType powerupType = PowerupManager.PowerupType.MultiPaddle;
	// Use this for initialization
	void Start () {
        pManager = FindObjectOfType<PowerupManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "paddle")
        {
            pManager.ActivatePowerup(powerupType);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            GameObject.Destroy(gameObject);
        }
    }
}
