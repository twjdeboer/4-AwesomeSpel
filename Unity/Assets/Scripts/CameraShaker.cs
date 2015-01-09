using UnityEngine;
using System.Collections;

public class CameraShaker : MonoBehaviour {

    private float intZoom;
    private float t;
    public float amplitude;
    public float shakeSpeed;
    public float shakeDuration;
    public float dampFactor;
    public bool shake;

	// Use this for initialization
	void Start () {
        intZoom = camera.fieldOfView;
	}
	
	// Update is called once per frame
	void Update () {
        if (shake && (t < shakeDuration || shakeDuration == 0))
        {
            t += Time.deltaTime;
            camera.fieldOfView = intZoom + amplitude*Mathf.Exp(-dampFactor * t)* Mathf.Sin(shakeSpeed * t);
            
        }
        else
        {
            t = 0;
            camera.fieldOfView = intZoom;
            shake = false;
        }
	}
}
