using UnityEngine;
using System.Collections;

public class transparentPwrups : MonoBehaviour {

    PaddleControls paddle;
    MeshRenderer pwrUp;

	// Use this for initialization
	void Start () {
        paddle = FindObjectOfType<PaddleControls>();
        pwrUp = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (paddle.currentState == PaddleControls.PaddleState.Launching)
        {
            pwrUp.material.color = new Color(pwrUp.material.color.r, pwrUp.material.color.g, pwrUp.material.color.b, 0.25f);
        }
        else
            pwrUp.material.color = new Color(pwrUp.material.color.r, pwrUp.material.color.g, pwrUp.material.color.b, 1);

    }
}
