using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public string LevelName;
	public int beaten = 0;
	public int unlocked = 0;
	public int BestScore = 999;
	public int NumberOfPlays = 0;
	public int ParScore;

	// Use this for initialization
	void Start () {

		//retrieves level data from playerprefs
		LevelName = Application.loadedLevelName;
		if(PlayerPrefs.HasKey(LevelName + "beaten"))
			beaten = PlayerPrefs.GetInt(LevelName + "beaten");
		else beaten = 0;

		if(PlayerPrefs.HasKey(LevelName + "unlocked"))
			unlocked = PlayerPrefs.GetInt(LevelName + "unlocked");
		else unlocked = 0;

		if(PlayerPrefs.HasKey(LevelName + "BestScore"))
			BestScore = PlayerPrefs.GetInt(LevelName + "BestScore");
		else BestScore = 999;

		if(PlayerPrefs.HasKey(LevelName + "NumberOfPlays"))
			NumberOfPlays = PlayerPrefs.GetInt(LevelName + "NumberOfPlays");
		else NumberOfPlays = 0;

		GameManager.instance.LastLevelName = LevelName;


	//	if(!PlayerPrefs.HasKey(LevelName + "ParScore"))
	//		PlayerPrefs.SetInt(LevelName + "ParScore", Mathf.Min(BestScore, ParScore));
	//	else 
	//		ParScore = PlayerPrefs.GetInt(LevelName + "ParScore", ParScore);

		NumberOfPlays++;
	}
	
	// Update is called once per frame
	void Update () {
	}

	//when level is beaten, optionally send data to GA and update some data
	public void LevelBeaten(int Steps){
				if (Steps < BestScore) {
						BestScore = Steps;
				}
				beaten++;
				int LevelNum = int.Parse (LevelName);

//				GA.API.Design.NewEvent ("UserBeatLevel " + LevelName, Time.timeSinceLevelLoad);
//				GA.API.Design.NewEvent ("NumberOfStepsLevel " + LevelName, Steps);

		}

	void OnDestroy(){

		//save all the data
		PlayerPrefs.SetInt(LevelName + "beaten", beaten);
		PlayerPrefs.SetInt(LevelName + "unlocked", unlocked);
		PlayerPrefs.SetInt(LevelName + "BestScore", BestScore);
		PlayerPrefs.SetInt(LevelName + "NumberOfPlays", NumberOfPlays);
		PlayerPrefs.SetInt(LevelName + "ParScore", ParScore);
		PlayerPrefs.Save ();
	}

}
