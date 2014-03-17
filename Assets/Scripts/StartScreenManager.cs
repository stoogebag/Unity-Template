using UnityEngine;
using System.Collections;

public class StartScreenManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//we are just waiting for anything to happen here.
		if (Input.anyKeyDown || Input.touchCount > 0)
						SendMessage("GoToMainMenu");
	}

	IEnumerator GoToMainMenu(){
		iTween.CameraFadeTo(1,0.15f);
		yield return new WaitForSeconds (0.15f);
		Application.LoadLevel (GameManager.instance.MainMenuString);
	}

}
