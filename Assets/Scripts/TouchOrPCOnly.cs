using UnityEngine;
using System.Collections;

public class TouchOrPCOnly : MonoBehaviour {

	public bool InvisibleOnTouch;
	public bool InvisibleOnPC;

	// Use this for initialization
	void Start () {
		if(Application.platform == RuntimePlatform.Android || 
		   Application.platform == RuntimePlatform.IPhonePlayer || 
		   Application.platform == RuntimePlatform.BB10Player)
		{
			if(InvisibleOnTouch) gameObject.SetActive( false);
		}
		else if(InvisibleOnPC) gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
