using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BlinkingButton : MonoBehaviour {

	public string fileName = "Sprites/DialogueBoxButton/ui_convo_triangle";
	public int spriteLength = 23;

	public List<Sprite> spriteSheet;
	private int currFrame = 0;
	private int counter = 0;
	public int delay = 2;

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

	public void SetTargetPosition(Vector3 _targetPos)
	{
		transform.position = _targetPos;
	}

	void FixedUpdate()
	{
		if (!GameController.instance.isPaused) {
			if (currFrame < spriteLength - 1) {
				if (counter == delay){
					currFrame++;
					counter = 0;
				}
				else
					counter++;

				transform.GetComponent<Image> ().sprite = spriteSheet [currFrame];
			} else {
				currFrame = 0;
				counter = 0;
			}
		}
	}
}
