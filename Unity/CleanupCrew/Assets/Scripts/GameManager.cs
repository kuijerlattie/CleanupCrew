using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    const float GENERATIONTIME = 5; //3 minutes
    const float CLEANINGTIME = 60;
    float gameTimer = 15;
    bool timerPaused = false;


    public int points;
    public int power;

    [SerializeField]
    Text pointText;
    [SerializeField]
    Text powerText;

    public Rect spawnLocation;

    public enum gamestate
    {
        Generating,
        Cleaning
    }

    gamestate state;

    GameObject g;

    [SerializeField] 
    Text timertext;
    [SerializeField]
    GameObject breakoutObjects;


    public void ResetTimer(bool generationStage = true)
    {
        if (generationStage)
        {
            timerPaused = false;
            gameTimer = GENERATIONTIME;
        }
        else
        {
            timerPaused = false;
            state = gamestate.Cleaning;
            gameTimer = CLEANINGTIME;
        }
    }

    public void PauseTimer()
    {
        timerPaused = true;
    }
	// Use this for initialization
	void Start () {
        state = gamestate.Generating;
        ResetTimer();
        PauseTimer();

        g = new GameObject("ShapeContainer");
	}

    //ball specific shit
    public float ballSpawnInterval = 10;
    public float spawnIntervalIncrease = 0.9f;
    public float spawnIntervalPowerIncrease = 0.1f;
    float spawntimer = 0;
    float spawncounter = 0;
	
	// Update is called once per frame
	void Update () {

        spawntimer -= Time.deltaTime;
        if (spawntimer <= 0)
        {
            spawncounter += spawnIntervalPowerIncrease;
            spawntimer = ballSpawnInterval * Mathf.Pow(0.9f, (float)spawncounter);
            Debug.Log((float)Mathf.Pow(0.9f, (float)spawncounter));
            Vector3 spawnloc = new Vector3(spawnLocation.x, 0, spawnLocation.y);
            SpawnSpheres.SpawnSphere(spawnloc, false);
        }
    }
}
