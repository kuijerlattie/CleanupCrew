using UnityEngine;
using System.Collections;

public class Meteo : EnemyScript {

    //TODO DOES NOTHING YET
    void Start()
    {
        BaseStart();
        useBaseCollider = false;
    }

    protected override bool HasDied()
    {

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        BaseUpdate();
    }
}
