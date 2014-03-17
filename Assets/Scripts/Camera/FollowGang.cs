using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowGang : MonoBehaviour {

	//this class makes the camera follow a group of gameobjects. LevelCentre is an anchor in case you 
	// want the movement to be more subtle.

	//the guys to follow
	public List<GameObject> Followees;
	
	public float FollowRatio = 0.9f;
	public bool Following = true;

	//the anchor
	public GameObject LevelCentre;

	//float for how much the centre pulls on things.
	public float TheLevelWeight = 10;

	// Use this for initialization
	void Start () {
		if (LevelCentre == null){  TheLevelWeight = 0; LevelCentre = gameObject;}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//set a group to follow from the SceneManager if none is set
		if(Followees == null || Followees.Count == 0){
			if(SceneManager.Scene.TheCameraFollowees != null)
				Followees = SceneManager.Scene.TheCameraFollowees;
		}

		if(Following && Followees.Count > 0){
			Vector3 Target = new Vector3();
			if(Followees != null){
				foreach(GameObject GE in Followees){
					Target += GE.transform.position;
				}
				Target += TheLevelWeight * LevelCentre.transform.position;
				Target /= (Followees.Count + TheLevelWeight);
			}
			else
			{
				Target += TheLevelWeight * LevelCentre.transform.position;
				Target /= TheLevelWeight;
			}

			Vector3 MovementVec = FollowRatio* Time.fixedDeltaTime * (Target - transform.position);
				
			transform.position += new Vector3(MovementVec.x, MovementVec.y,0);
		}
	}
}
