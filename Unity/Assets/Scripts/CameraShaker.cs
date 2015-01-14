using UnityEngine;
using System.Collections;

/**
 * Let camera shake using fieldOfView.
 * */
public class CameraShaker : MonoBehaviour {

    private float intZoom;
    private float t;
    public float amplitude;
    public float shakeSpeed;
    public float shakeDuration;
    public float dampFactor;
    public bool shake;

	// Use this for initialization: determine initial zoom for resetting camera if needed.
	void Start () {
        intZoom = camera.fieldOfView;
	}

    /**
     * Let camera shake by adjusting field of view using sinus. To damp vibration, exp is used
     * */
    void Shaker()
    {
        if (shake && (t < shakeDuration || shakeDuration == 0))
        {
            t += Time.deltaTime;
            camera.fieldOfView = intZoom + amplitude * Mathf.Exp(-dampFactor * t) * Mathf.Sin(shakeSpeed * t);

        }
        else
        {
            t = 0;
            camera.fieldOfView = intZoom;
            shake = false;
        }
    }
	
	// Update is called once per frame
	void Update () {

	}
}
