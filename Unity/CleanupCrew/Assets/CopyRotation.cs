using UnityEngine;
using System.Collections;

public class CopyRotation : MonoBehaviour {

    public GameObject copyFrom;
    public bool InUpdate = false;
	// Use this for initialization
	void Start () {
        transform.localRotation = copyFrom.transform.localRotation;
        transform.Rotate(90, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	    if(InUpdate) transform.localRotation = copyFrom.transform.localRotation;
    }
}
