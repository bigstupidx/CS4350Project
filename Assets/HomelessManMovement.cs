using UnityEngine;
using System.Collections;

public class HomelessManMovement : MonoBehaviour {

	SpriteRenderer spriteRenderer;		// Reference to my spriteRenderer

	Sprite[] sprites = new Sprite[4];
	int idleFrames = 4;
	float idleTime = 0.0f;
	float frameTime = 0.0f;
	int currFrame = 0;
	int repeatedTimes = 0;
	public float timeBeforeIdle = 15.0f;
	public float timePerFrame = 0.025f;
	public int timesToRepeat = 3;

//	
	
	// Use this for initialization
	void Awake () {

		sprites[0] = Resources.Load<Sprite> ("Sprites/HomelessManSprites/spr_homeless0");
		sprites[1] = Resources.Load<Sprite> ("Sprites/HomelessManSprites/spr_homeless1");
		sprites[2] = Resources.Load<Sprite> ("Sprites/HomelessManSprites/spr_homeless2");
		sprites[3] = Resources.Load<Sprite> ("Sprites/HomelessManSprites/spr_homeless3");

		// Set up references.
		spriteRenderer = transform.GetComponent<SpriteRenderer> ();
		spriteRenderer.sprite = sprites [0];
	}

	
	// Update is called once per frame
	void FixedUpdate () {
		idleTime += Time.deltaTime;
		//timeFrame++;
		//Debug.Log ("Current frame" + currFrame);
		if (idleTime >= timeBeforeIdle) {

			frameTime += Time.deltaTime;
			if (frameTime >= timePerFrame){

				currFrame = (currFrame +1)%idleFrames;
				spriteRenderer.sprite = sprites[currFrame];
				frameTime = 0;

				if(currFrame == 0){
					repeatedTimes++;
					if(repeatedTimes == timesToRepeat){
						idleTime = 0.0f;
						repeatedTimes = 0;
					}
				}
			
			}
		}
	}
}
