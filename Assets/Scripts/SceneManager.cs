using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;


public class SceneManager : MonoBehaviour {

	public static SceneManager Scene;

	public GameObject TheLevel;

	public TextMesh LevelID;

	public bool Demo;
	string LevelSelectString;

	[HideInInspector]
	public AudioSource FXSource;

	public ButtonEvent VolMusicButton;
	public ButtonEvent VolSFXButton;

	[HideInInspector]
	public GameManager GameManager;
	[HideInInspector]
	public GameObject MainCamObj;

	public bool GamePaused;

	public List<GameObject> TheCameraFollowees;

	[HideInInspector]
	public GameObject BeatenMenu;

	Texture2D pixel;

	// Use this for initialization
	void Start () {
		if (SceneManager.Scene == null)
			SceneManager.Scene = this;

		//initialise the main camera object
		if(MainCamObj == null) MainCamObj = Camera.main.gameObject;
		MainCamObj.SetActive(true);

		// initialise the volume controls if necessary
		if (!Application.loadedLevelName.Contains ("LevelSelect") 
		    && Application.loadedLevelName != "Start" 
		    && Application.loadedLevelName != "HowToPlay" 
		    && Application.loadedLevelName != "Credits" 
		    && !Application.loadedLevelName.Contains("Main") ){
						if (VolMusicButton == null) {
								VolMusicButton = MainCamObj.transform.FindChild ("PauseMenu").FindChild ("VolMusic").FindChild ("ButtonGUI").GetComponent<ButtonEvent> ();
								VolMusicButton.receiver = gameObject;
						}
						if (VolSFXButton == null) {
								VolSFXButton = MainCamObj.transform.FindChild ("PauseMenu").FindChild ("VolSFX").FindChild ("ButtonGUI").GetComponent<ButtonEvent> ();
								VolSFXButton.receiver = gameObject;
						}
				}
		if(GameManager.instance != null){
			VolMusic();
			VolMusic();
			VolSFX();
			VolSFX();
		}

		LevelSelectString = "LevelSelect";
		//optionally set the 'levelselectstring'
	/*	if (GameManager.instance != null && !GameManager.instance.IsDemo) {
						LevelSelectString = "LevelSelect";
		} 
		else if(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android){
			LevelSelectString = "LevelSelectDemoMOBILE";
		}
		else
			LevelSelectString = "LevelSelectDemoPC";
*/

		//fade from white.
		pixel = (Texture2D) Resources.Load("pixel");
		iTween.CameraFadeAdd(pixel);
		iTween.CameraFadeFrom(1,0.7f);
	}
	
	// Update is called once per frame
	void Update () {
		// A SECRET CODE TO UNLOCK THE FULL GAME (for debug)
		/*
		if (Input.GetKey (KeyCode.J) && Input.GetKey (KeyCode.K) && Input.GetKey (KeyCode.L)) {
			Demo = false;
			LevelSelectString = "LevelSelect";	
				}
		*/

		//press space to hit `next' on the beaten menu
		if (Input.GetKeyDown (KeyCode.Space) && BeatenMenu != null && BeatenMenu.activeSelf) {
			SendMessage("Next");
				}

	}

	// a function to deal with player death. optionally play a sound. restarts the level
	public void Dead(){
	//	FXSource.clip = Resources.Load<AudioClip>("Audio/Clips/Dead");
	//	FXSource.Play();
	//	yield return new WaitForSeconds(FXSource.clip.length);
		SendMessage("Restart");
	}

	void LevelBeaten(){
		if(BeatenMenu == null)	BeatenMenu = Camera.main.transform.FindChild("BeatenMenu").gameObject;
		if(!BeatenMenu.activeSelf){
			BeatenMenu.SetActive (true);
		//	BeatenMenu.SendMessage("Appear");

			//play the applause sound effect
			FXSource.clip = Resources.Load<AudioClip>("Audio/applause");
			FXSource.Play();

			BeatenMenu.transform.localPosition = new Vector3 ( 0,0,5);

			//the levelmanager deals with things that are particular to levels
			LevelManager MyLM = GetComponent<LevelManager>();

	//optionally make a change if the player beats some kind of condition
	/*	
	 		if(CONDITION){
				BeatenMenu.transform.FindChild("Title").FindChild("Nice Job").GetComponent<TextMesh>().text = "Gold, Baby!";
				BeatenMenu.transform.FindChild("Title").FindChild("GoldBorder").gameObject.SetActive(true);
			}
*/

			//example of changing the text on the popup menu to reflect the player's performance
			/*
			BeatenMenu.transform.FindChild("Title").FindChild("Your Score").GetComponent<TextMesh>().text = "Your #Steps    -      " + NumberOfSteps.ToString();
			BeatenMenu.transform.FindChild("Title").FindChild("Your Best").GetComponent<TextMesh>().text = "Your Best  -      " + MyLM.BestScore.ToString();
			BeatenMenu.transform.FindChild("Title").FindChild("GoldMedal").GetComponent<TextMesh>().text = "Gold Medal  -     " + MyLM.ParScore.ToString();
*/

				//tweenin
			iTween.MoveFrom(BeatenMenu, iTween.Hash("position", BeatenMenu.transform.localPosition + new Vector3(0,10,0), "isLocal", true, "easetype", iTween.EaseType.easeOutBounce, "time", 1));
		}
		GamePaused = true;
	}
	

	//pause the game and activate the pause-menu
	public void Pause(){
		PopupMenu pauseMenu = Camera.main.transform.FindChild("PauseMenu").GetComponent<PopupMenu>();
		if(pauseMenu != null) pauseMenu.Appear();
	}


	public void UnpauseMaybe(){
	
		GameObject pauseMenu = 	Camera.main.transform.FindChild("PauseMenu").gameObject;
		if(pauseMenu != null)
			GamePaused = pauseMenu.activeSelf;
	}

	//restart the level. optionally send a GA event
	public IEnumerator Restart()
	{
		iTween.CameraFadeTo(1,0.15f);	
		//GA.API.Design.NewEvent ("RestartedLevel " + Application.loadedLevelName, Time.timeSinceLevelLoad);

		yield return new WaitForSeconds (0.15f);
		Application.LoadLevel(Application.loadedLevel);
	}

	//go to the next level. Here I assume your levels are named with a 3-digit number and are in order with no gaps. 
	//for example in BROWN our levels are named XXY where XX is the world number (01-10) and Y is the level number (1-5)
	//so you may need some custom code here. 
	public IEnumerator Next(){
		iTween.CameraFadeTo(1,0.15f);
		yield return new WaitForSeconds (0.15f);
		
		int LevelNum = -1;
		int.TryParse(GetComponent<LevelManager>().LevelName,out LevelNum);
		if (LevelNum != -1) {
			string NextLvlString = (LevelNum+1).ToString("D3");
			Application.LoadLevel(NextLvlString);
		}
	}

	//quit to menu
	public IEnumerator QuitToMenu()
	{
		int levelNum;
		if(int.TryParse(Application.loadedLevelName, out levelNum))
			PlayerPrefs.SetInt("LastLevelPlayed", levelNum);
		iTween.CameraFadeTo(1,0.15f);
		yield return new WaitForSeconds (0.15f);
		Application.LoadLevel(GameManager.instance.MainMenuString);
	}


	//quit everything. fades out for dramatic effect.
	IEnumerator QuitGame()
	{
		iTween.CameraFadeTo(1,0.3f);
		yield return new WaitForSeconds (0.25f);
		Application.Quit();
	}

	//go to the appropriate level select scene. used as 'quit level', too
	IEnumerator LevelSelect()
	{
		iTween.CameraFadeTo(1,0.15f);
		yield return new WaitForSeconds (0.15f);
		
		int levelNum;
		if(int.TryParse(Application.loadedLevelName, out levelNum))
			PlayerPrefs.SetInt("LastLevelPlayed", levelNum);

		Application.LoadLevel(LevelSelectString);
	}
	


	// toggle music on/off. modifies the text on the button
	void VolMusic(){
		if(GameManager.instance.VolumeMusic > 0)
		{
			GameManager.instance.VolumeMusic = 0;
			if(VolMusicButton  != null) VolMusicButton.text.text = "Music off";
		}
		else
		{	
			GameManager.instance.VolumeMusic = 1;
			if(VolMusicButton  != null) VolMusicButton.text.text = "Music on";
		}
	}

	//toggle sfx on/off
	void VolSFX(){
		if(GameManager.instance.VolumeFX > 0)
		{
			GameManager.instance.VolumeFX = 0;
			if(VolSFXButton  != null) VolSFXButton.text.text = "SFX off";
		}
		else
		{	
			GameManager.instance.VolumeFX = 1;
			if(VolSFXButton  != null) VolSFXButton.text.text = "SFX on";
		}
	}

}
