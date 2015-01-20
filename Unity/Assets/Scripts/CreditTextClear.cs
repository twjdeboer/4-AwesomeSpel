using UnityEngine;
using System.Collections;

public class CreditTextClear : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
//        Debug.Log(GetComponent<RectTransform>().anchoredPosition.y);
        if (GetComponent<RectTransform>().anchoredPosition.y > 650)
            Destroy(gameObject);
	}

}
