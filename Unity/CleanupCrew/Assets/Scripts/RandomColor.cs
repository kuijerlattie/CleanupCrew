using UnityEngine;
using System.Collections;

public class RandomColor : MonoBehaviour {

    static public readonly Color[] Colors = { Color.green, Color.red, Color.blue, Color.yellow};


    static public Color GetRandomColor()
    {
        return Colors[Random.Range(0, Colors.GetLength(0))];
    }

    // Use this for initialization
    void Start () {
        GetComponent<Renderer>().material.color = GetRandomColor();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
