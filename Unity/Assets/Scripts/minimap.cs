using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class minimap : MonoBehaviour {
	public GameObject pl;
	public RawImage minimapsize;
	public Vector3 pointingpos;
	public RawImage pointer;
	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("m")) {
			minimapsize.transform.localPosition=new Vector3(0,0,0);
			minimapsize.rectTransform.localScale=new Vector3(2.5f,2.5f,1);
			//c2.enabled = false;
		} else if(Input.GetKeyUp("m")) {
			//c1.enabled = false;
			minimapsize.transform.localPosition=new Vector3(730,360,0);
			minimapsize.rectTransform.localScale=new Vector3(0.7f,0.7f,1);

		}

		pointingpos =  pl.transform.position;
		pointingpos.x -= 30f;
		pointingpos.y = pointingpos.z+22.5f;
		pointingpos.z = 0;

		pointer.transform.localPosition = pointingpos;


	}
}
