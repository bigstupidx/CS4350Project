using UnityEngine;
using System.Collections;

public class HomelessManMovement : MonoBehaviour {

	SpriteRenderer spriteRenderer;		// Reference to player's spriteRenderer

	Sprite[] sprites = new Sprite[4];
	int idleFrames = 4;
	public float timeBeforeIdle = 30.0f;
	public float timePauseInIdle = 3.0f;
	public float timePerFrame = 0.025f;
	
	// Use this for initialization
	void Awake () {

		sprites[0] = Resources.Load<Sprite> ("Sprites/HomelessManSprites/spr_homeless0.png");
		sprites[1] = Resources.Load<Sprite> ("Sprites/HomelessManSprites/spr_homeless1.png");
		sprites[2] = Resources.Load<Sprite> ("Sprites/HomelessManSprites/spr_homeless2.png");
		sprites[3] = Resources.Load<Sprite> ("Sprites/HomelessManSprites/spr_homeless3.png");

		// Set up references.
		spriteRenderer = GetComponent<SpriteRenderer> ();
		spriteRenderer.sprite = sprites [0];
	}

	
	// Update is called once per frame
	void FixedUpdate () {
		spriteRenderer.sprite = sprites [0];
	}
}
