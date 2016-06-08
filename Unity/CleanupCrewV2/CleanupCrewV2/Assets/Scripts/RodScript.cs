using UnityEngine;
using System.Collections;

public class RodScript : MonoBehaviour {

    public enum rodtype
    {
        water,
        space,
        underground
    }

    public rodtype rodType;

	void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ball")
        {
            EventManager.TriggerEvent("BallHitRod", this.gameObject, (float)rodType);
        }

        if (col.gameObject.tag == "Blob")
        {
            EventManager.TriggerEvent("BlobHitRod", col.gameObject, (float)rodType);
        }
    }
}
