using UnityEngine;
using System.Collections;

public class ParticleToUI : MonoBehaviour {

    public UITarget currentTarget = UITarget.Barrel;
    public enum UITarget
    {
        Barrel,
        Energy,
        Points
    }

    public void Init()
    {
        switch(currentTarget)
        {
            case UITarget.Barrel:
                UIWorldPosition = new Vector3(-33, 18, 0);
                break;
            case UITarget.Energy:
                UIWorldPosition = new Vector3(-37, 18, 0);
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
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, UIWorldPosition, Time.deltaTime * speed);
        if (Vector3.Distance(transform.position, UIWorldPosition) < Mathf.Epsilon)
            GameObject.Destroy(gameObject);
	}
}
