using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class CollidersMole : MonoBehaviour {

    public bool isHeadCollider;

	void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer != LayerMask.NameToLayer("Balls")) return;
        Mole moleScript = FindObjectOfType<Mole>();
        if (isHeadCollider) moleScript.col1Hits++;
        if (!isHeadCollider) moleScript.col2Hits++;
        GameObject.Destroy(col.gameObject);

    }
}
