using UnityEngine;
using System.Collections;

/// <summary>
/// this is used to set the size of the circle arena level thing
/// </summary>
public class ScaleOnStart : MonoBehaviour {

    private Vector3 newScale;
	// Use this for initialization
	void Start () {
        newScale = new Vector3(GameSettings.CircleScaleS, 1, GameSettings.CircleScaleS);
        transform.localScale = newScale;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
