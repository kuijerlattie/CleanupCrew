﻿using UnityEngine;
using System.Collections;
using System;

public class IntroScript : BaseGamestate {

    // Use this for initialization

    public override void StartState()
    {

    }

    public override void EndState()
    {
        
    }
    
    void Update()
    {
        GameManager.instance.SetState(GameManager.gamestate.Tutorial);
    }
}