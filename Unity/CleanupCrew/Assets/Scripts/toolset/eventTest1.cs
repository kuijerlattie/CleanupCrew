using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class eventTest1 : MonoBehaviour
{
    private UnityAction someListener;

    void Awake()
    {
        someListener = new UnityAction(SomeFunction);
    }

    void OnEnable()
    {
        EventManager.StartListening("testevent1", someListener);
    }

    void OnDisable()
    {
        EventManager.StopListening("testevent1", someListener);
    }

    void SomeFunction()
    {
        Debug.Log("somefunction was called!");
    }
}
