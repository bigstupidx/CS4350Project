using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BubbleBehaviour : MonoBehaviour {

	public GameObject target;

	private string fileName = "Sprites/Textbubble/textbubble_";
	private int spriteLength = 4;
	public bool canTrigger = false;
	public bool itemStatus = false;

	public bool alreadySelected = false;
	
	//public List<Texture2D> spriteSheet;
	public List<Sprite> spriteSheetDefault;
	public List<Sprite> spriteSheetActive;
	public int currFrame = 0;
	private int delay = 25;
	private int counter = 0;

	// Use this for initialization

	void Start () {

		spriteSheetDefault = new List<Sprite> (spriteLength);
		spriteSheetActive = new List<Sprite> (spriteLength);
		
		for (int i = 0; i < spriteLength; i++) {
			spriteSheetDefault.Add(Resources.Load<Sprite> (fileName + i ) );
			spriteSheetActive.Add(Resources.Load<Sprite> (fileName + i + "_highlight") );
		}

		transform.GetComponent<Image> ().sprite = spriteSheetDefault [0];
		if (target == null) {
			target = GameObject.FindGameObjectWithTag("Player");
		}

		if (!GameController.instance.isAndroidVersion)
			gameObject.SetActive (false);

		if (EndingController.instance.isChapter2Activated) {
			gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.7f);
		}
	}

	public void TouchDetected()
	{
		alreadySelected = true;
		if(!GameObject.Find ("TextBox_Android").GetComponent<FadeInFadeOut> ().isFadingOn)
			target.GetComponent<Displaytextbox> ().TriggerTextbox ();
	}

	public void TurnOffButton()
	{
		transform.GetComponent<Image> ().enabled = false;
	}
	
	void Reset()
	{
		transform.GetComponent<Image> ().enabled = true;
		currFrame = 0;
		if (EndingController.instance.isChapter2Activated) {
			gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.7f);
		}else
			gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	}
	
	void FixedUpdate()
	{
		if (canTrigger) {
			if(!transform.GetComponent<Image> ().enabled)
				Reset();
			Vector3 playerPos = target.transform.position;
			playerPos.x += 0.2f; 
			playerPos.y += 1.0f; 
			transform.position = Camera.main.WorldToScreenPoint (playerPos);

			if (counter > delay) {
				currFrame++;
				counter = 0;
			} else
				counter++;
			
			if (currFrame < spriteLength) {
				if(itemStatus)
					transform.GetComponent<Image> ().sprite = spriteSheetActive[currFrame];
				else
					transform.GetComponent<Image> ().sprite = spriteSheetDefault [currFrame];

			} else {
				currFrame = 0;
			}
		} else {
			TurnOffButton();
		}
	}
}
