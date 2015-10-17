using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BubbleBehaviour : MonoBehaviour {

	public GameObject target;

	private string fileName = "Sprites/Textbubble/textbubble_";
	private int spriteLength = 4;
	public bool canTrigger = false;

	public bool alreadySelected = false;
	
	//public List<Texture2D> spriteSheet;
	public List<Sprite> spriteSheet;
	public int currFrame = 0;
	private int delay = 25;
	private int counter = 0;

	// Use this for initialization

	void Start () {

		spriteSheet = new List<Sprite> (spriteLength);
		
		for (int i = 0; i < spriteLength; i++) {
			spriteSheet.Add(Resources.Load<Sprite> (fileName + i ) );
		}

		transform.GetComponent<Image> ().sprite = spriteSheet [0];
		if (target == null) {
			target = GameObject.FindGameObjectWithTag("Player");
		}

		if (!GameController.instance.isAndroidVersion)
			gameObject.SetActive (false);
	}

	public void TouchDetected()
	{
		alreadySelected = true;
		if(!GameObject.Find ("TextBox_Android").GetComponent<FadeInFadeOut> ().isFadingOn)
			target.GetComponent<Displaytextbox> ().TriggerTextbox ();
	}
	
	void OnEnable()
	{
		currFrame = 0;
	}
	
	void FixedUpdate()
	{
		if (canTrigger) {
			transform.GetComponent<Image> ().enabled = true;
			Vector3 playerPos = target.transform.position;
			playerPos.x += 0.2f; 
			playerPos.y += 1.0f; 
			transform.position = Camera.main.WorldToScreenPoint (playerPos);

			if (counter > delay) {
				currFrame++;
				counter = 0;
			} else
				counter++;
			
			if (currFrame < spriteLength - 1) {
				transform.GetComponent<Image> ().sprite = spriteSheet [currFrame];
			} else {
				currFrame = 0;
			}
		} else {
			transform.GetComponent<Image> ().enabled = false;
		}
	}
}
