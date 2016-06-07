﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public enum gamestate
    {
        Start,
        Tutorial,
        BreakoutIntermission,
        Breakout,
        BossIntermission,
        Boss
    }

    public static GameManager instance = null;
    private gamestate gameState = gamestate.Start;

    void Awake()
    {
        if (instance == null)
            instance = this;
        if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        InitGame();
        
    }

    void InitGame()
    {
        SetState(gamestate.Start);
    }

    public void SetState(gamestate state)
    {
        switch (state)
        {
            case gamestate.Start:
                StartIntro();
                break;
            case gamestate.Tutorial:
                StartTutorial();
                break;
            case gamestate.BreakoutIntermission:
                StartBreakoutIntermission();
                break;
            case gamestate.Breakout:
                StartBreakout();
                break;
            case gamestate.BossIntermission:
                StartBossIntermission();
                break;
            case gamestate.Boss:
                StartBoss();
                break;
            default:
                break;
        }
    }

    void StartIntro()
    {
        //game start animation and shit
    }

    void StartTutorial()
    {
        //play tutorial
    }

    void StartBreakoutIntermission()
    { 
        //breakout start animation and shit
    }

    void StartBreakout()
    {
        //start the main gameplay stuff
    }

    void StartBossIntermission()
    {
        //boss start animation and shit
    }

    void StartBoss()
    {
        //actually start the bossfight logics
    }

    public gamestate CurrentGamestate
    { get { return gameState; } }

}
