using UnityEngine;
using UnityEngine.UI;

public class HudScript : MonoBehaviour {
    
    Text tPoints;
    Text tEnergy;
    Text tWaterpoints;
    Text tEarthpoints;
    Text tSpacepoints;

    // Use this for initialization
    void Start () {
        tPoints = GameObject.Find("Points").GetComponent<Text>();
        tEnergy = GameObject.Find("Energy").GetComponent<Text>();
        tWaterpoints = GameObject.Find("Waterpoints").GetComponent<Text>();
        tEarthpoints = GameObject.Find("Sandpoints").GetComponent<Text>();
        tSpacepoints = GameObject.Find("Spacepoints").GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {
        tPoints.text = "points: " + GameManager.instance.CurrentPoints;
        tEnergy.text = "energy: " + GameManager.instance.CurrentEnergy;
        tWaterpoints.text = "Waste in water: " + GameManager.instance.CurrentWaterPoints;
        tEarthpoints.text = "Waste underground: " + GameManager.instance.CurrentEarthPoints;
        tSpacepoints.text = "waste in space: " + GameManager.instance.CurrentSpacePoints;
	}
}
