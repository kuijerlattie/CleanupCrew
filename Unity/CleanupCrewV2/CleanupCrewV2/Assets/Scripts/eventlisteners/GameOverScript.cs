using UnityEngine;
using System.Collections;

public class GameOverScript : BaseGamestate {

    float i = 0;
    GameObject gameOverObject = null;   //canvas prefab with scaled gameover screen

    public override void StartState()
    {
        //explode all things in the scene?
        GameManager.instance.SetGameplayState(GameManager.gameplaystate.paused);
        //make camera flyover the level with blobs going all over the place while your score is shown onscreen?
        ToggleGameOverScreen(true);

    }

    public override void EndState()
    {
        
    }

    void ToggleGameOverScreen(bool toggle)
    {
        if(toggle && gameOverObject == null)
        {
            gameOverObject = GameObject.Instantiate(Resources.Load("prefabs/GameOverCanvas")) as GameObject;
            gameOverObject.GetComponentInChildren<UnityEngine.UI.Text>().text = GameManager.instance.CurrentPoints.ToString();
        }
        else if(gameOverObject != null) //in case you want to restart the game without quitting
        {
            GameObject.Destroy(gameOverObject);
            gameOverObject = null;
        }
    }

    
    void Update()
    {
        i += Time.deltaTime;
        if (i >= 5f)
        {
            try
            {
                FindObjectOfType<DBconnection>().UploadScore(FindObjectOfType<Arguments>().getUserID(), FindObjectOfType<Arguments>().getGameID(), GameManager.instance.CurrentPoints);
            }
            catch
            {
                Debug.Log("tried uploading score");
            }

            Application.Quit();
        }
    }
}
