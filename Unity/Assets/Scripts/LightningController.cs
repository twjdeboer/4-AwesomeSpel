using UnityEngine;
using System.Collections;

public class LightningController : MonoBehaviour
{
    private GameObject lightning_spot;
    private GameObject lightning_point;
    private GameObject camera;
    private int lightninglength;
    private double timer;
    private double elapsedtime;
    private AudioClip lightning;
    public AudioClip lightning1;
    public AudioClip lightning2;
    public AudioClip lightning3;
    public Vector3 shakeSpeed;
    public Vector3 amplitude;


    // Use this for initialization
    void Start()
    {
        camera = GameObject.Find("MenuCamera");
        camera.GetComponent<CameraShaker>().shakeSpeed = shakeSpeed;
        camera.GetComponent<CameraShaker>().amplitude = amplitude;
        lightning_spot = GameObject.Find("Lightning_spot");
        lightning_point = GameObject.Find("Lightning_point");

        lightning_spot.GetComponent<Light>().intensity = 0;
        lightning_point.GetComponent<Light>().intensity = 0;

        timer = Random.Range(5f, 10f);
        Debug.Log(timer);
        lightninglength = 0;
    }

    // Update is called once per frame
    void Update()
		{
				elapsedtime += Time.deltaTime;
		
				if (elapsedtime > timer) {
						lightninglength++;
						//Light effects
						lightning_point.GetComponent<Light> ().intensity = 3.1f;
						if (lightninglength == 4)
								lightning_spot.GetComponent<Light> ().intensity = 4.75f;
						if (lightninglength > 7)
								lightning_point.GetComponent<Light> ().intensity = 0;
						if (lightninglength > 10)
								lightning_spot.GetComponent<Light> ().intensity = 0;
                      
						//sound effects 

						//refresh
						if (lightninglength > 20) {
                         
								timer = Random.Range (10F, 15F);
								elapsedtime = 0;
								lightninglength = 0;
								Debug.Log (timer);
								int clipselect = Random.Range (0, 3);
								switch (clipselect) {
								case 0:
										lightning = lightning1;
										break;
								case 1:
										lightning = lightning2;
										break;
								case 2:
										lightning = lightning3;
										break;
								}
                                camera.GetComponent<CameraShaker>().shakeDuration = lightning.length;
                                camera.GetComponent<CameraShaker>().shake = true;
								audio.PlayOneShot (lightning);
						}
				}
		}
}
