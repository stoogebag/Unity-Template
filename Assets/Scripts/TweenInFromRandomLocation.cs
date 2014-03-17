using UnityEngine;
using System.Collections;

public class TweenInFromRandomLocation : MonoBehaviour {

	public float MinDistance = 100f;
	public float MaxDistance = 200f;
	public float MaxTime = 1;
	public float MinTime = 0.2f;


	// Use this for initialization
	void Start () {

		if (Application.loadedLevelName.Contains ("LevelSelect"))
						return;
		Vector3 MyVec = Random.onUnitSphere;
		float MyDistance = Random.Range (MinDistance, MaxDistance);
		MyVec *= MyDistance;
		float MyTime = Random.Range (MinTime, MaxTime);

		iTween.MoveFrom (gameObject, iTween.Hash( "position", MyVec + gameObject.transform.position, "time", MyTime, "easetype", iTween.EaseType.easeInOutQuad) );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
