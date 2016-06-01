using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Collider))]
public class SplitUpScript : MonoBehaviour {

    private int splits = 0;
    private int MAXSPLITS = 3;
    private float splitupPushDistance = 5;
    private float splitUpScale = 0.75f;

    void OnCollisionEnter(Collision col)
    {
        if (splits >= MAXSPLITS)
        {
            GameObject.Destroy(gameObject);
            return;    
        }
        if (col.collider.gameObject.layer == LayerMask.NameToLayer("Balls"))
        {
            if (!col.collider.gameObject.GetComponent<HitPaddle>().HittedPaddle) return;
            GameObject clone = GameObject.Instantiate(gameObject) as GameObject;
            Vector3 randomVec = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0, UnityEngine.Random.Range(-1.0f, 1.0f)).normalized;
            gameObject.transform.position += randomVec * splitupPushDistance;
            gameObject.transform.localScale *= splitUpScale;
            clone.transform.position -= randomVec * splitupPushDistance;
            clone.transform.localScale *= splitUpScale;
            splits++;
            clone.GetComponent<SplitUpScript>().splits = splits;
            FindObjectOfType<BattlePhase>().AddEnemyToList(clone);

            Meteo m = clone.GetComponent<Meteo>();
            m.overrideStoppedAtCenter = true;
            m.AbleToShoot = false;
            clone.AddComponent<ForceNotTrigger>();
            GameObject.Destroy(col.collider.gameObject);    //destroys the ball
            Meteo[] meteos = GameObject.FindObjectsOfType<Meteo>();
            if (Array.Find(meteos, a => a.AbleToShoot) == null) m.AbleToShoot = true;
        }
    }
}
