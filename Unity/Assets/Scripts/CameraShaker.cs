using UnityEngine;
using System.Collections;

public class CameraShaker : MonoBehaviour {

    private Vector3 intPos;
    private float t;
    public Vector3 amplitude;
    public Vector3 shakeSpeed;
    public float shakeDuration;
    public bool shake;

	// Use this for initialization
	void Start () {
        intPos = transform.position;	
	}
	
	// Update is called once per frame
	void Update () {
        if (shake && (t < shakeDuration || shakeDuration == 0))
        {
            t += Time.deltaTime;
            transform.position = intPos + transform.TransformDirection(new Vector3(amplitude.x * Mathf.Sin(shakeSpeed.x * t), amplitude.y * Mathf.Sin(shakeSpeed.y * t), amplitude.z * Mathf.Sin(shakeSpeed.z * t)));
        }
        else
        {
            t = 0;
        }
	}
}
