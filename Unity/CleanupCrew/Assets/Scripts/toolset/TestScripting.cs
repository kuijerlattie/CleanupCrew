using UnityEngine;

public class TestScripting : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (SI.idleTime >= 10) SI.DisplayMessage("Prefabs/Text/MessageAFK", StopIdleMessage);
        Tutorial();
      
	}

    void OnEnable()
    {
        EventManager.StartListening("BlobDestroyed", OnBlobDestroy);
        EventManager.StartListening("BallDestroyed", OnBlobDestroy);
        EventManager.StartListening("EnemyDestroyed", OnBlobDestroy);
    }

    void OnBlobDestroy(GameObject g, float f)
    {
        SI.SpawnParticle("Fire", g.transform);
    }

    void OnBallDestroy(GameObject g, float f)
    {
        SI.SpawnParticle("STEAM", g.transform);
    }

    void OnEnemyDestroy(GameObject g, float f)
    {
        SI.SpawnParticle("Smoke 2", g.transform);
    }

    void Tutorial()
    {
        if (SI.gameState != GameManager.gamestate.Tutorial) return;
        
        if (SI.elapsedTimethisPhase >= 60) SI.DisplayMessage("Prefabs/Text/MessageBAD", StopYouAreBadMessage);


    }


    bool StopIdleMessage()
    {
        if (SI.idleTimeInt <= 0) return true;
        else return false;
    }

    bool StopYouAreBadMessage()
    {
        if (SI.elapsedTimethisPhase <= 0) return true;
        else return false;
    }


}
