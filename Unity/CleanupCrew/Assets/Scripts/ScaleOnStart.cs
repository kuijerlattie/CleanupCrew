using UnityEngine;
using System.Collections;

public class ScaleOnStart : MonoBehaviour {

    private Vector3 newScale = new Vector3(GameSettings.CircleScaleS, 1, GameSettings.CircleScaleS);
	// Use this for initialization
	void Start () {
        transform.localScale = newScale;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
