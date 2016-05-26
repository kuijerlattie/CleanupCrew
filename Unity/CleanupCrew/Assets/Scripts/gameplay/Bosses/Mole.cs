using UnityEngine;
using System.Collections;

public class Mole : EnemyScript {

    public int col1Hits = 0;
    public int col2Hits = 0;
    private int hitsPerCol = 3;
	// Use this for initialization
	void Start () {
        BaseStart();
        useBaseCollider = false;
    }

    protected override bool HasDied()
    {
        if (col1Hits >= hitsPerCol && col2Hits >= hitsPerCol) return true;
        return false;
    }

        // Update is called once per frame
    void Update () {
        BaseUpdate();
        //Debug.Log("1: " + col1Hits + "  2: " + col2Hits);
    }
}
