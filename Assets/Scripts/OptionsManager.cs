using UnityEngine;
using System.Collections;

public class OptionsManager : MonoBehaviour {

	public static OptionsManager instance;



	// Use this for initialization
	void Start () {
		
		DontDestroyOnLoad(gameObject);
	//	PlayerPrefs.DeleteAll ();
		//make sure only one optionsmanager loads ever
		if(instance == null) instance = this;
		if(instance != this) Destroy(gameObject);


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
