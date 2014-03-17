using UnityEngine;

using System.Collections;



public class ButtonEvent : MonoBehaviour {
	public Color colorOver = new Color(1f,0.88f,0.55f); 

	public bool SceneLoader;
	public GameObject receiver;
	public string EventArg;
	public bool ActivateReceiver = true;

	public bool inactive = false;

	public TextMesh text;

	public Color colorPushed  = new Color(0.66f,0.66f,0.48f);   
	
	public float ScaleFactorHover = 1.1f;
	public float ScaleFactorPress = .9f;

	public string level; //If empty quit the application

	private Color originalColor;
	private Vector3 originalLocalScale;

	public float Cooldown;
	float TimeLeftOnCooldown = 0;


	void Start(){       
		originalColor = gameObject.renderer.material.color;     
		originalLocalScale = transform.localScale;
	}

	void Update(){
		if(TimeLeftOnCooldown > 0)	TimeLeftOnCooldown -= Time.deltaTime;
	}
		
	void OnMouseEnter()     
	{	
		if(inactive) return;	
		
		if (Application.platform != RuntimePlatform.Android &&
						Application.platform != RuntimePlatform.IPhonePlayer) {
						gameObject.renderer.material.color = colorOver;	
						transform.localScale = originalLocalScale * ScaleFactorHover;
				}
	}   

	
	void OnMouseExit()
		
	{
		if(inactive) return;	

		
		if (Application.platform != RuntimePlatform.Android &&
						Application.platform != RuntimePlatform.IPhonePlayer) {
						gameObject.renderer.material.color = originalColor;
		
						transform.localScale = originalLocalScale;
				}
	}
	
	

	void OnMouseDown()
		
	{
		if(inactive) return;	
		
		if (Application.platform != RuntimePlatform.Android &&
						Application.platform != RuntimePlatform.IPhonePlayer) {
						transform.localScale = originalLocalScale * ScaleFactorPress;
						gameObject.renderer.material.color = colorPushed;
				}
	}
	 
	//this is the 'do things' function, mostly just ugly branches.
	void Clicked()        
	{   
		if(!inactive){
			if(TimeLeftOnCooldown <= 0){
				transform.localScale = originalLocalScale;

				if(SceneLoader){
					if(transform.parent.name.Length == 1){
						GameManager.instance.SendMessage("StartLvl", transform.parent.parent.parent.name[0].ToString() + transform.parent.parent.parent.name[1].ToString() + transform.parent.name);
					}
					else GameManager.instance.SendMessage("StartLvl", transform.parent.name);
				}
				else{
					if(receiver == null) //if no receiver we just kind of spam things :/
					{
						if(EventArg != null && EventArg != "") {
							GameObject.Find("SceneManager").SendMessage(EventArg, SendMessageOptions.DontRequireReceiver);
							GameObject.Find("GameManager").SendMessage(EventArg, SendMessageOptions.DontRequireReceiver);
							transform.parent.parent.gameObject.SendMessage(EventArg, SendMessageOptions.DontRequireReceiver);
					}
						else{
							
							GameObject.Find("SceneManager").SendMessage(transform.parent.name,SendMessageOptions.DontRequireReceiver);
							GameObject.Find("SceneManager").SendMessage(transform.name,SendMessageOptions.DontRequireReceiver);
							
							GameObject.Find("GameManager").SendMessage(transform.parent.name,SendMessageOptions.DontRequireReceiver);
							GameObject.Find("GameManager").SendMessage(transform.name,SendMessageOptions.DontRequireReceiver);
							
							transform.parent.parent.gameObject.SendMessage(transform.parent.name,SendMessageOptions.DontRequireReceiver);
							transform.parent.parent.gameObject.SendMessage(transform.name,SendMessageOptions.DontRequireReceiver);
						}
					}
					else{
							if(EventArg != null && EventArg != "") 
						{
							if(ActivateReceiver)
								receiver.SetActive(true);
							receiver.gameObject.SendMessage(EventArg);
						}
						else{
							if(ActivateReceiver)
								receiver.SetActive(true);

							receiver.SendMessage(transform.parent.name,SendMessageOptions.DontRequireReceiver);
							receiver.SendMessage(transform.name,SendMessageOptions.DontRequireReceiver);
						}
					}
				}
				TimeLeftOnCooldown = Cooldown;
			}
		}
	}



	//if we are on a touch screen then a touch activates 
	void OnTouch()    
	{       
		if(Application.platform == RuntimePlatform.Android ||
		   Application.platform == RuntimePlatform.IPhonePlayer) 
		Clicked();
	}

	//if we are on pc then we don't activate until mouse release
	void OnMouseUpAsButton()    
	{       
		if(Application.platform != RuntimePlatform.Android &&
		   Application.platform != RuntimePlatform.IPhonePlayer) 
		   		Clicked();
	}



}