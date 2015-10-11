using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisualHint_2D : MonoBehaviour {


	public string fileName;
	public int spriteLength = 4;
	public double duration = 1.5;
	
	public List<Texture2D> defaultSpriteSheet;
	public List<Texture2D> highlightSpriteSheet;

	public int currFrame = 0;
	private int delay = 5;
	private int curr = 0;
	public bool isPlayerEntered = false;

	private float startTime = 0.0f;
	private bool startAnimation = true;

	public bool myStatus = false;
	private GameObject player;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");

		defaultSpriteSheet = new List<Texture2D> (spriteLength);
		highlightSpriteSheet = new List<Texture2D> (spriteLength); 
		
		for (int i = 0; i < spriteLength; i++) {
			defaultSpriteSheet.Add(Resources.Load<Texture2D> (fileName + i ) );
			highlightSpriteSheet.Add(Resources.Load<Texture2D> (fileName + i +"_highlight" ) );

		}
		transform.GetComponentInChildren<Renderer> ().material.mainTexture = defaultSpriteSheet [0];
	}

	void OnTriggerEnter(Collider other){
		Item curr = GameController.instance.GetItem (this.gameObject.name);
		myStatus = PlayerController.instance.AbleToTrigger (curr);
		isPlayerEntered = true;
	}
	
	void OnTriggerExit(Collider other){
		myStatus = false;
		isPlayerEntered = false;
	}

	void Update()
	{
		if (spriteLength > 1) {
			if (!startAnimation && (Time.time - startTime) > (float)(duration) ) {
				startAnimation = true;
			}

			if (startAnimation) {
				if (curr > delay) {
					currFrame++;
					curr = 0;
				} else
					curr++;
			}

			if (currFrame < spriteLength - 1) {
				if (myStatus) {
					transform.GetComponentInChildren<Renderer> ().material.mainTexture = highlightSpriteSheet [currFrame];
				} else {
					transform.GetComponentInChildren<Renderer> ().material.mainTexture = defaultSpriteSheet [currFrame];
				}
			} else {
				currFrame = 0;
				startAnimation = false;
				startTime = Time.time;
			}
		} else {
			if (myStatus) {
				transform.GetComponentInChildren<Renderer> ().material.mainTexture = highlightSpriteSheet [currFrame];
			} else {
				transform.GetComponentInChildren<Renderer> ().material.mainTexture = defaultSpriteSheet [currFrame];
			}
		}
	}

	void LateUpdate()
	{
		if(isPlayerEntered)
			myStatus = PlayerController.instance.AbleToTrigger ( GameController.instance.GetItem (this.gameObject.name) );
	}
}
