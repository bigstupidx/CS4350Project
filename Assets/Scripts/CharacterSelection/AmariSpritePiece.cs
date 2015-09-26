using UnityEngine;
using System.Collections;

public class AmariSpritePiece : MonoBehaviour {
	SpriteRenderer spriteRenderer;
	
	public string pieceName = "face";
	
	const int downCnst = 0;
	const int leftConst = 1;
	const int rightConst = 2;
	const int upConst = 3;
	
	
	int currDir = 0;
	
	AmariMovement moveScript;
	
	Sprite[] sprites = new Sprite[4];
	
	// Use this for initialization
	void Start () {
		sprites[0] =  Resources.Load<Sprite>(PlayerData.FormSpritePath(pieceName, 0));
		sprites[1] =  Resources.Load<Sprite>(PlayerData.FormSpritePath(pieceName, 1));
		sprites[2] =  Resources.Load<Sprite>(PlayerData.FormSpritePath(pieceName, 2));
		sprites[3] =  Resources.Load<Sprite>(PlayerData.FormSpritePath(pieceName, 3));
		
		// Set up references.
		spriteRenderer = transform.GetComponent<SpriteRenderer> ();
		spriteRenderer.sprite = sprites [0];
		
		
		moveScript = transform.parent.GetComponent<AmariMovement> ();


	}
	
	public void SwitchSprites(Sprite[] newSet){
		sprites = newSet;
		spriteRenderer.sprite = sprites [currDir];
	}
	
	// Update is called once per frame
	void Update () {
		if (moveScript.GetCurrDir() != currDir) {
			currDir = moveScript.GetCurrDir();
			
			spriteRenderer.sprite = sprites [currDir];
		}
	}

}
