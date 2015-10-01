using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BlinkingButton : MonoBehaviour {

	private string fileName = "Sprites/DialogueBoxButton/ui_convo_triangle";
	private int spriteLength = 23;

	public List<Sprite> spriteSheet;
	private int currFrame = 0;
	private bool toggleNext = true;

	//private Image spriteRenderer;	

	/*
	private float speed = 0.4f;
	private bool fadeOut = true;
	private Color defaultColor;
	*/
	// Use this for initialization
	void Start () {
		//defaultColor = new Color (1.0f, 1.0f, 1.0f, 0.5f);

		spriteSheet = new List<Sprite> (spriteLength);

		for (int i = 0; i < spriteLength; i++) {
			spriteSheet.Add(Resources.Load<Sprite> (fileName + i ) );
		}
		transform.GetComponent<Image> ().sprite = spriteSheet [0];
	}

	void OnEnable()
	{
		currFrame = 0;
	}

	void FixedUpdate()
	{
		if (currFrame < spriteLength) {
			if(toggleNext)
				currFrame++;

				transform.GetComponent<Image> ().sprite = spriteSheet [currFrame];
				toggleNext = !toggleNext;
		} else {
			currFrame = 0;
		}
	
	}
	
	// Update is called once per frame
	/*void Update () {
		float alpha = gameObject.GetComponent<Image> ().color.a;
		if (fadeOut) 
			alpha -= (speed * Time.deltaTime);
		else
			alpha += (speed * Time.deltaTime);

		gameObject.GetComponent<Image> ().color = new Color (defaultColor.r, defaultColor.g, defaultColor.b, alpha);

		if (gameObject.GetComponent<Image> ().color.a <= 0.0f || gameObject.GetComponent<Image> ().color.a >= 0.5f)
			fadeOut = !fadeOut;
	}*/
}
