using UnityEngine;
using System.Collections;

public class CreditTextClear : MonoBehaviour {

    public float speed;
    private bool send = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        //        Debug.Log(GetComponent<RectTransform>().anchoredPosition.y);
        if (GetComponent<RectTransform>().anchoredPosition.y > 650)
            Destroy(gameObject);
        if (GetComponent<RectTransform>().anchoredPosition.y > -400 && send)
        {
            GameObject.Find("AllText").GetComponent<ShowText>().next = true;
            send = false;
        }
    }

}
