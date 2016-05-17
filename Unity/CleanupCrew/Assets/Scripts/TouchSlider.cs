using UnityEngine;
using System.Collections;

/// <summary>
/// used to make the bar visual on the screen where you can click/tap
/// </summary>
public class TouchSlider : MonoBehaviour {

	void Start () {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float percentOfScreen = GameObject.FindObjectOfType<PaddleRotationScript>().InputMaxDistance / 100f;
        rectTransform.position = new Vector3(rectTransform.position.x, (float)Screen.height / rectTransform.sizeDelta.y * (percentOfScreen * 100f), rectTransform.position.z);
        rectTransform.localScale = new Vector3((float)Screen.width / rectTransform.sizeDelta.x, (float)Screen.height / rectTransform.sizeDelta.y * percentOfScreen, rectTransform.localScale.z);
    }
	void Update () {
        
    }
}
