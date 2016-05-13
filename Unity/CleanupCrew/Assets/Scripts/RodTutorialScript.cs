using UnityEngine;
using System.Collections;

public class RodTutorialScript : MonoBehaviour {

	void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer != LayerMask.NameToLayer("Balls")) return;
        Debug.Log("GONE " + col.gameObject.name);
        GameObject.FindObjectOfType<TutorialPhase>().HitRod();
        gameObject.SetActive(false);

    }
}
