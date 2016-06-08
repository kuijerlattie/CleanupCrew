using UnityEngine;
using System.Collections;

public abstract class BaseGamestate : MonoBehaviour {

    public abstract void StartState();

    public abstract void EndState();
	
}
