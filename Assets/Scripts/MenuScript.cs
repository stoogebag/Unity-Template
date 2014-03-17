using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayerPrefs.Save();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	IEnumerator HowToPlay(){
		iTween.CameraFadeTo(1,0.15f);
		yield return new WaitForSeconds (0.15f);
		Application.LoadLevel("HowToPlay");
	}
	IEnumerator Credits(){
		iTween.CameraFadeTo(1,0.15f);
		yield return new WaitForSeconds (0.15f);
		Application.LoadLevel("Credits");
	}



}
