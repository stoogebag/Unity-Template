using UnityEngine;
using System.Collections;

public class SplashScreenManager : MonoBehaviour {

	public string NextSceneName = "Start";

	// Use this for initialization
	void Start () {

		//there is a bug in windows build. first time we play it seems to open in wrong resolution for some reason
		//so here we set it manually to be fullscreen native.
		Resolution[] ScreenResArray = Screen.resolutions;
		if (Application.platform == RuntimePlatform.WindowsPlayer) {
						Screen.SetResolution (ScreenResArray [ScreenResArray.Length - 1].width,
		                     ScreenResArray [ScreenResArray.Length - 1].height,
			                      true);
				}

		// fade in
		Texture2D pixel = (Texture2D) Resources.Load("pixel");
		iTween.CameraFadeAdd(pixel);
		iTween.CameraFadeFrom(1,0.9f);

		//this just says we wait awhile and then load the start screen
		SendMessage("WaitAndLoadNextScene");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator WaitAndLoadNextScene(){
		yield return new WaitForSeconds (2f);
		iTween.CameraFadeTo(1,.8f);
		yield return new WaitForSeconds (0.9f);
		Application.LoadLevel(NextSceneName);
	}

}
