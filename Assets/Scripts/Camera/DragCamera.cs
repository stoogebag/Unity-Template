using UnityEngine;
using System.Collections;

public class DragCamera : MonoBehaviour {

	public bool Dragging = false;
	Vector3 LastMousePos;
	Vector3 Anchor;
	public FollowGang gangScript;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Dragging){
			Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Camera.main.transform.position -= (MousePos-Anchor)*Time.deltaTime * 4;
		}

		
		if(Application.platform == RuntimePlatform.Android){
			if(Input.touchCount == 0) Dragging = false;
		}
	}

	
	void OnMouseDown(){
		Dragging = true;
		if(gangScript != null) gangScript.Following = false;
		Anchor = Camera.main.ScreenToWorldPoint(Input.mousePosition);

	}
	
	void OnMouseUp(){
		Dragging = false;
		if(gangScript != null) gangScript.Following = true;
	}

	void OnTouch()
	{
		Dragging = true;
		if(gangScript != null) gangScript.Following = false;
		Anchor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
}
