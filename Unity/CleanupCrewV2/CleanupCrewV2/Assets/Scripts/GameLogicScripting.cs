using UnityEngine;
using System.Collections;


/// <summary>
/// script that resolves (most of) the important game events
/// </summary>
public class GameLogicScripting : MonoBehaviour
{
    void OnAwake()
    {
        if (instance == null)
            instance = this;
        if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public static GameLogicScripting instance = null;


	void OnEnable()
    {
        EventManager.StartListening("BallHitRod", OnBallHitRod);
        EventManager.StartListening("BallHitBlob", OnBallHitBlob);
        EventManager.StartListening("BlobKill", OnBlobKill);
    }

    void OnDisable()
    {
        EventManager.StopListening("BallHitRod", OnBallHitRod);
        EventManager.StopListening("BallHitBlob", OnBallHitBlob);
        EventManager.StopListening("BlobKill", OnBlobKill);
    }

    void OnBallHitRod(GameObject g, float f)
    {
        BlobScript.Spawn(RodScript.GetRodSpawnPoint(g));
    }

    void OnBallHitBlob(GameObject g, float f)
    {
        g.GetComponent<BlobScript>().TakeDamage(1);
    }

    void OnBlobKill(GameObject g, float f)
    {
        GameManager.instance.AddGooToBarrel(1);
    }

}
