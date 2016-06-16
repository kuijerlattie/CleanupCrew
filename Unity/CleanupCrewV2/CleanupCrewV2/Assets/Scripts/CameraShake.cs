using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    Camera _camera;
    float shake = 0;
    float shakeAmount = 0.3f;
    float decreaseFactor = 1.0f;
    Vector3 defaultPos;

    void Start()
    {
        _camera = GetComponent<Camera>();
        defaultPos = transform.position;
          
    }
    public void SetShake(float seconds, float intensity)
    {
        shake = seconds;
        shakeAmount = intensity;
    }

    public static void ScreenShake(float seconds, float intensity = 0.3f)
    {
        CameraShake CS = FindObjectOfType<CameraShake>();
        if (CS == null) CS = Camera.main.gameObject.AddComponent<CameraShake>();
        CS.SetShake(seconds, intensity);
    }
 
    void Update()
    {
        if (shake > 0)
        {
            _camera.transform.localPosition = defaultPos + Random.insideUnitSphere * shakeAmount;
            shake -= Time.deltaTime * decreaseFactor;

        }
        else {
            shake = 0.0f;
        }
    }
}
