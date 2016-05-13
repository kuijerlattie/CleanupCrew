using UnityEngine;
using System.Collections.Generic;
using System;

public class TutorialPhase : AbstractPhase {

    public const int amountOfLayers = 3;
    private GameObject[] rods;

    private GameObject playingBall = null;
    private GameManager manager;
    private float powerPerRod = 0;  //based on 'amountOfLayers'

    public override void StartPhase()
    {
        FindPointZones();
        powerPerRod = 100f / (float)amountOfLayers;
        rods = new GameObject[amountOfLayers];
        manager = GameObject.FindObjectOfType<GameManager>();
        isActive = true;
        SetWalls(true);
        SetPointZones(true);
        nextGamestate = GameManager.gamestate.Cleanup;

        for (int i = 0; i < amountOfLayers; i++)
        {
            GameObject rod = GameObject.Instantiate(Resources.Load("Prefabs/UraniumRod") as GameObject);
            rod.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f) * i;
            rod.transform.position = Vector3.zero;
            rods[i] = rod;
        }

        playingBall = SpawnSpheres.SpawnSphere(-Vector3.forward * rods[2].GetComponent<MeshFilter>().mesh.bounds.size.x /4f, -Vector3.forward);
        
    }

    /// <summary>
    /// find all 'WallScript's and toggle them to Tutorial mode.
    /// this means that ball collison with wall only re-enables any missing rods, instead of destroying the ball;
    /// </summary>
    /// <param name="isTutorial"></param>
    void SetWalls(bool isTutorial)
    {
        WallScript[] walls = GameObject.FindObjectsOfType<WallScript>();
        foreach (WallScript wall in walls)
        {
            wall.isTutorial = isTutorial;
        }
    }

   

    private int CheckActiveRods()
    {
        int amountOfActives = 0;
        foreach (GameObject rod in rods)
        {
            if (rod.activeSelf) amountOfActives++;
        }
        return amountOfActives;
    }

    //should only be called from 'WallScript', when the ball hits a wall a rod reappears.
    public void HitWall()
    {
        int activeRods = CheckActiveRods();
        if (activeRods < amountOfLayers)
        {
            rods[activeRods].SetActive(true);
            manager.power -= powerPerRod;
        }
    }

    public void HitRod()
    {
        manager.power += powerPerRod;
    }

    public override void StopPhase()
    {
        GameObject.Destroy(playingBall);
        SetWalls(false);
        SetPointZones(false);
        pointZones.Clear();
        isActive = false;
        for (int i = 0; i < amountOfLayers; i++)
        {
            GameObject.Destroy(rods[i]);
        }
    }

    public override bool HasEnded()
    {
        if (CheckActiveRods() <= 0) return true;
        return false;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
