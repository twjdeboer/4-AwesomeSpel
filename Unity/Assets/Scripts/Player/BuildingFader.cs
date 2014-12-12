using UnityEngine;
using System.Collections;

public class BuildingFader : MonoBehaviour {

    //Actions
	
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Building")
           Methods.SetAlpha(other.gameObject, 0.5f);
		if(other.gameObject.tag == "Building2")
			Methods.SetAlpha(other.gameObject, 0.1f);
        if (other.gameObject.tag.Contains("Transparent"))
            Methods.SetAlpha(other.gameObject, 0.5f);
    }

    void OnTriggerExit(Collider other)
	{
        if (other.gameObject.tag == "Building")
            Methods.SetAlpha(other.gameObject, 1);
		if(other.gameObject.tag == "Building")
			Methods.SetAlpha(other.gameObject, 1);
                if (other.gameObject.tag.Contains("Transparent"))
            Methods.SetAlpha(other.gameObject, 1);
    }
}
