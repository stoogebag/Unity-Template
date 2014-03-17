using UnityEngine;
using System.Collections;


public class AspectRatio : MonoBehaviour {

	bool ChangeMade = false;

	// Use this for initialization
	void Awake () {       

		//this script will move uielements to fit an aspect ratio wider than 16:10.
		if(!ChangeMade){
			foreach(Transform Ch in transform)
			{
				//			if(Camera.main.aspect >= 1.6f)
				Ch.localPosition = Vector3.Scale(Ch.localPosition, new Vector3(Camera.main.aspect * 10/16,1,1));

				if(Application.loadedLevelName == "menu") Ch.localScale = Vector3.Scale(Ch.localScale, new Vector3(Camera.main.aspect * 10/16, Camera.main.aspect * 10/16, 1));
				
				//				Ch.localPosition = Vector3.Scale(Ch.localPosition, new Vector3(1,Camera.main.aspect * 16/10,1));
			}
			ChangeMade = true;
		}
	}
	// Update is called once per frame
	void Update () {
	}
}
