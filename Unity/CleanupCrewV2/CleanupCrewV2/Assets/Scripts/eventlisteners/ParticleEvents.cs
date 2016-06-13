using UnityEngine;
using System.Collections;

public class ParticleEvents : MonoBehaviour {

	void OnEnable()
    {
        EventManager.StartListening("BlobDestroyed", OnBlobDestroy);
    }

    void OnDisable()
    {
        EventManager.StopListening("BlobDestroyed", OnBlobDestroy);
    }

    void OnBlobDestroy(GameObject g, float f)
    {
        Debug.Log("blob destroyed, starting blob pop");
        StaticFuntions.SpawnParticle("blob pop", g.transform.position);
    }
}
