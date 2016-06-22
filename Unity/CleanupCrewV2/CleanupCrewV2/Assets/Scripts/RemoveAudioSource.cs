using UnityEngine;
using System.Collections;

public class RemoveAudioSource : MonoBehaviour {

	// Use this for initialization
	void Update () {
        if (!GetComponent<AudioSource>().isPlaying)
            Destroy(gameObject);
	}
}
