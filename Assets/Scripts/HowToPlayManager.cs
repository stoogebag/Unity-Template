using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HowToPlayManager : MonoBehaviour {

	public List<GameObject> FolloweesList;
	public int CurrentRoom = 0;
	
	
	public PopupMenu NextButton;
	public PopupMenu PrevButton;
	public PopupMenu FinishedButton;

	// Use this for initialization
	void Start () {

		if(Application.loadedLevelName.Contains("LevelSelect") && PlayerPrefs.HasKey("LastLevelPlayed"))
		{
			CurrentRoom = PlayerPrefs.GetInt("LastLevelPlayed") /10;
			CurrentRoom -= 2;
		}
		else CurrentRoom = -1;
		InitRoom();

	}
	
	// Update is called once per frame
	void Update () {
	}

	void InitRoom(){
		CurrentRoom++;
		if (CurrentRoom > 0)
						PrevButton.Appear ();
				else
						PrevButton.Resume ();
		
		if (CurrentRoom < FolloweesList.Count - 1) {
						NextButton.Appear (); 
				} else
						NextButton.Resume ();
		EnterRoom(CurrentRoom);

		if(FinishedButton != null)
			FinishedButton.Resume ();
	}

	void NextRoom(){
		CurrentRoom++;

		if(CurrentRoom == 1) PrevButton.Appear();
		
		if(CurrentRoom == FolloweesList.Count-1) {
			NextButton.Resume(); 
			if(FinishedButton != null)
				FinishedButton.Appear(); 
		}
		EnterRoom(CurrentRoom);
	}
	
	void PrevRoom(){
		CurrentRoom--;
		if(CurrentRoom == 0) PrevButton.Resume();

		if(CurrentRoom == FolloweesList.Count-2) {
			NextButton.Appear(); 
			if(FinishedButton != null)
				FinishedButton.Resume(); 
		}
		EnterRoom(CurrentRoom);
	}
	IEnumerator Finished(){
		iTween.CameraFadeTo(1,0.15f);
		yield return new WaitForSeconds (0.15f);
		Application.LoadLevel(GameManager.instance.MainMenuString);
	}

	void EnterRoom(int No){
		//switch 'rooms'
		Camera.main.GetComponent<FollowGang>().Followees = new List<GameObject>();
		Camera.main.GetComponent<FollowGang>().Followees.Add(FolloweesList[No]);
	}

}
