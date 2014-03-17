using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour {

	public bool Touching;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer){
			if(Input.touchCount > 0) {
				if(Input.GetTouch(0).phase == TouchPhase.Began){
					checkTouch(Input.GetTouch(0).position);
				}
			}
		}
	}


	void checkTouch(Vector3 position){
		
		Vector3 wp = Camera.main.ScreenToWorldPoint(position);
		Vector2 touchPos = new Vector2(wp.x, wp.y);
		Collider2D hit = Physics2D.OverlapPoint(touchPos);

		if(hit){
			Touching = true;
			hit.transform.gameObject.SendMessage("OnTouch",0,SendMessageOptions.DontRequireReceiver);
		}
	}

}
