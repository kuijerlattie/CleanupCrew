using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class eventTest1 : MonoBehaviour
{
    private UnityAction someListener;

    void Awake()
    {
    }

    void OnEnable()
    {
        EventManager.StartListening("testevent1", SomeFunction);
    }

    void OnDisable()
    {
        EventManager.StopListening("testevent1", SomeFunction);
    }

    void SomeFunction(GameObject g, float f)
    {
        Debug.Log("somefunction was called!");
    }
}
