using UnityEngine;
using System.Collections;

public class paddleShooting : MonoBehaviour {

    public bool canShoot = false;
    
    void Start()
    {
        EventManager.StartListening("DoubleClick", Shoot);
    }

    void OnDisable()
    {
        EventManager.StopListening("DoubleClick", Shoot);
    }

    void Shoot(GameObject g, float f)
    {
        PaddleControls paddle = FindObjectOfType<PaddleControls>();
        if (paddle.currentState == PaddleControls.PaddleState.Playing && canShoot)
        {
           GameObject bullet = (GameObject)Instantiate(Resources.Load("prefabs/Bullet"), (paddle.gameObject.transform.position + new Vector3(0, 0.75f, 1.5f)), Quaternion.identity);
        }
    }
}
