using UnityEngine;
using System.Collections;

public abstract class AbstractPhase : MonoBehaviour {

    protected bool isActive = false;
    public abstract void Start();
    public abstract void Stop();

}
