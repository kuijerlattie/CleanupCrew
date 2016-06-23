using UnityEngine;
using System.Collections;

public class UIevents : MonoBehaviour {

	// Use this for initialization
	void Start () {
        EventManager.StartListening("GainedPoints", GainPoints);
	}

    void OnDisable()
    {
        EventManager.StopListening("GainedPoints", GainPoints);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void GainPoints(GameObject g, float f)
    {
        GameObject gg = GameObject.Instantiate(Resources.Load("prefabs/PopupCanvas")) as GameObject;
        gg.GetComponentInChildren<PopupText>().SetValue(f);
    }
}
