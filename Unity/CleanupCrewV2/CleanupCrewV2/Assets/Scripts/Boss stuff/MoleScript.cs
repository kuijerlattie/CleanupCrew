using UnityEngine;
using System.Collections;

public class MoleScript : BossBase {

    float visibletimer = 10f;
    float hidetimer = 5f;

    molestate state = molestate.aboveground;

    enum molestate
    {
        underground,
        digging,
        aboveground
    }
    

	// Use this for initialization
	void Start () {
        hitpoints = 5;
	}
	
	// Update is called once per frame
	void Update () {
        if (hitpoints < maxhitpoints - 2)
        {
            switch (state)
            {
                case molestate.underground:
                    break;
                case molestate.digging:
                    break;
                case molestate.aboveground:
                    break;
                default:
                    break;
            }
        }   
	}
    /*
    boss mechanics for the mole

    mole comes out of ground, stays for a bit
    after few hits mole goes underground, moves somewhere else
    mole spawns particles above him showing ground moving or shit
    mole comes back up. stays for x seconds (maybe does an attack)
    mole goes back down moves (repeat this pattern till death)
    */
}
