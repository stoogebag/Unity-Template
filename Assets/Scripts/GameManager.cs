using UnityEngine;
using System.Collections;


//this class is for all `game level' functions, like volume controls, options, etc.
public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public AudioSource MusicSource;
	public string MainMenuString;

	public string LastLevelName;
	float _VolumeMusic;

	public bool IsDemo = true;

	public float VolumeMusic {get {return _VolumeMusic;} 
		set{
			iTween.AudioTo(gameObject, value, MusicSource.pitch, 0.8f); 
			_VolumeMusic = value;
			PlayerPrefs.SetFloat("VolumeMusic", value);
			PlayerPrefs.Save();
		}
	}
	
	float _VolumeFX;
	public float VolumeFX {get {return _VolumeFX;} 
		set{
			_VolumeFX = value;
			PlayerPrefs.SetFloat("VolumeFX", value);
		}
	}

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);


		//make sure only one gamemanager loads ever
		if(GameManager.instance == null) instance = this;
		if(instance != this) Destroy(gameObject);

		MusicSource = GetComponent<AudioSource>();
		
		//initiate values and options
		if(!PlayerPrefs.HasKey("VolumeMusic")) 
		{
			PlayerPrefs.SetFloat("VolumeMusic", 1f);
		}
		_VolumeMusic = PlayerPrefs.GetFloat("VolumeMusic");
		MusicSource.volume = _VolumeMusic; 

		if(!PlayerPrefs.HasKey("VolumeFX")) 
			PlayerPrefs.SetFloat("VolumeFX", 1);
		VolumeFX = PlayerPrefs.GetFloat("VolumeFX");

		if (PlayerPrefs.HasKey ("Unlocked"))
						IsDemo = PlayerPrefs.GetInt ("Unlocked") == 0;

		MainMenuString = "MainMenu";

		// optionally set a different main menu string, for example for different aspect ratio or for web player
		// in web player i like to not have an exit button, since it doesn't really work
		/*
		if (Camera.main.aspect >= 1.32 && Camera.main.aspect <= 1.34)
			MainMenuString = "MainMenu43";
				else if
			(Application.isWebPlayer)
				MainMenuString = "MainMenuWEB";
		 */
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// start a given level. mostly used by the level select scenes
	public IEnumerator StartLvl(string LevelName)
	{
		iTween.CameraFadeTo(1,0.15f);
		yield return new WaitForSeconds (0.15f);
		Application.LoadLevel(LevelName);
	}
	
	//open the credits scene
	IEnumerator Credits(){
		iTween.CameraFadeTo(1,0.15f);
		yield return new WaitForSeconds (0.15f);
		Application.LoadLevel("Credits");
	}


}
