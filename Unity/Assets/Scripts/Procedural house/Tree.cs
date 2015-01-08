using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour {
	public GameObject self;
	private Vector3 pos;
	public bool moving;
	public bool colid;

	void Awake(){
		moving = true;
		pos = self.transform.position;
		colid=false;
	}

	void OnCollisionStay(Collision other) {
		colid = true;
		print ("Colliding");
		if (!other.transform.gameObject.tag.Contains ("Walk"))
			Destroy (self);
		if (moving && other.transform.gameObject.tag.Contains ("Walk")) {
			pos.y += 0.1f;
			self.transform.position = pos;
		}
	}

	void OnCollisionExit(){
		moving = false;
		pos.y -= 0.6f;
		self.transform.position = pos;
	}
}
