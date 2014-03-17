using UnityEngine;
using System.Collections;

//this component handles twening in and out for popup menus
public class PopupMenu : MonoBehaviour {
	Vector3 origPos;

	public bool PausesGame = true;

	//does escape kill this menu? useful for most pause menus
	public bool Escapable = false;

	//does ANY key kill this menu? good for tutorial/dialogue messages.
	public bool AnyKey = false;

	// Use this for initialization

	void Start () {
	//	origPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {	
		if(AnyKey) if(Input.anyKeyDown) Resume();
		if(Escapable) if(Input.GetKeyDown(KeyCode.Escape)) Resume();
	}

	//appear. default is to rise from the bottom of the screen
	public void Appear(){
		if(origPos == Vector3.zero) origPos = transform.localPosition;
		if(SceneManager.Scene != null && PausesGame)	
			SceneManager.Scene.GamePaused = true;
		transform.localPosition = origPos;

		gameObject.SetActive(true);
		iTween.MoveFrom(gameObject, iTween.Hash("position", origPos + new Vector3(0,-10,0), 
		                                        "isLocal", true, 
		                                        "easetype", iTween.EaseType.easeOutCubic, 
		                                        "time", 1));
	}

	//fades out quickly
	public void Resume()
	{
		iTween.FadeTo(gameObject, iTween.Hash( "a", 0, "time" , 0.2f, "onComplete", "Resume2"));
	}

	//the use of a second resume callback is very naughty. i cannot sleep at night over this
	void Resume2()
	{
		iTween.FadeTo(gameObject, iTween.Hash( "a", 1, "time" , 0.1f));	
		gameObject.SetActive(false);
		SceneManager.Scene.UnpauseMaybe();
	}
}
