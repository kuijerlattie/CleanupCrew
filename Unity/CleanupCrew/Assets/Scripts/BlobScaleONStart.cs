using UnityEngine;
using System.Collections;

public class BlobScaleONStart : MonoBehaviour {

    private Vector3 newScale = new Vector3(GameSettings.BlobScaleS, GameSettings.BlobScaleS, GameSettings.BlobScaleS);
    // Use this for initialization
    void Start () {
        transform.localScale = newScale;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
