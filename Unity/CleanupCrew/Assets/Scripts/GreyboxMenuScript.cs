using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GreyboxMenuScript : MonoBehaviour {

    private const string latestScene = "31-5 Josse";  //TODO if other scene change this
    private const string menuScene = "GreyboxMenu";
	// Use this for initialization


    public void StartGame()
    {
        SceneManager.LoadScene(latestScene);
    }

    public void StartMenu()
    {
        SceneManager.LoadScene(menuScene);
    }
}
