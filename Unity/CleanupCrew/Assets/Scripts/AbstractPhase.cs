using UnityEngine;
using System.Collections;

public abstract class AbstractPhase : MonoBehaviour {

    protected bool isActive = false;
    public abstract void StartPhase();
    public abstract void StopPhase();

}
