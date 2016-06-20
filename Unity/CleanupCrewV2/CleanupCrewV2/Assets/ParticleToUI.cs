using UnityEngine;
using System.Collections;
using System.Linq;

/// <summary>
/// makes particles move to a point in the UI, and destroys itself afterwards
/// </summary>
public class ParticleToUI : MonoBehaviour {

    public UITarget currentTarget = UITarget.Barrel;
    public enum UITarget
    {
        Barrel,
        Energy,
        Points
    }


    /// this is to be called when the play scene is loaded, currently in : GameManager.StartGame
    /// -Canvas with UI must be on : Screen space - Camera   (to be able to set only the MainCamera to render the UI)
    /// -Canvas with UI must be on : Scale with Screen Size     (otherwise the points that represent the UI in world don't line up with different resolutions)
    /// -Main camera must have prefab: "OnTopOfUICamera" as child of the MainCamera
    /// -MainCamera can exclude "OnTopOfUI"-layer in it's culling mask
    public static void SetUIForParticles()
    {
        Canvas[] _canvases = FindObjectsOfType<Canvas>();
        System.Array.ForEach(_canvases, x => 
        {
            x.renderMode = RenderMode.ScreenSpaceCamera;
            x.gameObject.GetComponent<UnityEngine.UI.CanvasScaler>().uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
            x.worldCamera = Camera.main; 
        });
        if (Camera.main.gameObject.transform.childCount == 0)   
        {
            //settings its position identical to the regular camera
            GameObject secondCam = GameObject.Instantiate(Resources.Load("prefabs/OnTopOfUICamera")) as GameObject;
            secondCam.transform.position = Camera.main.transform.position;
            secondCam.transform.rotation = Camera.main.transform.rotation;
            secondCam.transform.parent = Camera.main.transform;
        }
        Camera.main.cullingMask = ~(1 << LayerMask.NameToLayer("OnTopOfUI"));

    }

    public void Init()
    {
        switch(currentTarget)
        {
            case UITarget.Barrel:
                UIWorldPosition = new Vector3(-41, 15, -5);
                break;
            case UITarget.Energy:
                UIWorldPosition = new Vector3(-45, 15, -5);
                break;
            case UITarget.Points:
                UIWorldPosition = new Vector3(-33, 18, -10);
                Debug.LogWarning("Trying to move particle to Points(score) in UI... where is that in the UI???? ");
                break;
        }
    }

    Vector3 UIWorldPosition = new Vector3(-33, 18, 0);
    float speed = 15;
	// Use this for initialization
	void Start () {
        gameObject.layer = LayerMask.NameToLayer("OnTopOfUI");
        Init();
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, UIWorldPosition, Time.deltaTime * speed);
        if (Vector3.Distance(transform.position, UIWorldPosition) < Mathf.Epsilon)
            GameObject.Destroy(gameObject);
	}
}
