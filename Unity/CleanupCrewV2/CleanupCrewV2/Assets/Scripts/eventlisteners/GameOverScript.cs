using UnityEngine;
using System.Collections;

public class GameOverScript : BaseGamestate {

    float i = 0;
    public override void StartState()
    {
        //explode all things in the scene?
        GameManager.instance.SetGameplayState(GameManager.gameplaystate.paused);
        //make camera flyover the level with blobs going all over the place while your score is shown onscreen?
    }

    public override void EndState()
    {

    }

    void Update()
    {
        i += Time.deltaTime;
        if (i >= 5f)
        {
            FindObjectOfType<DBconnection>().UploadScore(FindObjectOfType<Arguments>().getUserID(), FindObjectOfType<Arguments>().getGameID(), GameManager.instance.CurrentPoints);
            Application.Quit();
        }
    }
}
