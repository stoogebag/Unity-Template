using UnityEngine;
using System.Collections;

public class LevelSelectButton : MonoBehaviour {

	public string LevelCode;
	public bool unlocked;
	public bool beaten;
	public bool beatenPar;
	public int bestScore;
	public int parScore;

	// Use this for initialization
	void Start () {
		LevelCode = gameObject.name;

		string LevName = gameObject.transform.parent.parent.name [0].ToString() +gameObject.transform.parent.parent.name [1].ToString() + gameObject.name;

		parScore =	PlayerPrefs.GetInt( LevName  + "ParScore");

		if(PlayerPrefs.HasKey(  LevName  + "unlocked"))
			unlocked = PlayerPrefs.GetInt(  LevName + "unlocked") > 0;
		else unlocked = ( LevName == "11");

	//	if (LevelCode == "11") 
			unlocked = true;
		
		if(PlayerPrefs.HasKey(  LevName  + "beaten"))
			beaten = PlayerPrefs.GetInt(  LevName  + "beaten") > 0;
		else beaten = false;

		if(PlayerPrefs.HasKey(  LevName  + "BestScore"))
			bestScore = PlayerPrefs.GetInt(  LevName  + "BestScore");
		else beaten = false;
		
		if(PlayerPrefs.HasKey(  LevName+ "BestScore"))
		{
			beatenPar = PlayerPrefs.GetInt(  LevName  + "BestScore") <= parScore;
		}
		else beatenPar = false;

		if(!unlocked){
			Color myColor = transform.FindChild("ButtonGUI").GetComponent<SpriteRenderer>().color;
			myColor = new Color(myColor.r, myColor.g,myColor.b, 0.5f);
			transform.FindChild("ButtonGUI").GetComponent<SpriteRenderer>().color = myColor;
			transform.FindChild("ButtonGUI").GetComponent<ButtonEvent>().inactive = true;
		}
		
		if(beaten){
			transform.FindChild("ButtonGUI").FindChild("BeatenSprite").GetComponent<SpriteRenderer>().enabled = true;
		}
		if(beatenPar){
			transform.FindChild("ButtonGUI").FindChild("BeatenSprite").FindChild("BeatenParSprite").GetComponent<SpriteRenderer>().enabled = true;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
