using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class FillCanvas : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RectTransform rt = GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.sizeDelta = Vector2.zero;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
