using UnityEngine;
using System.Collections;

public class PlayerSpritePiece : MonoBehaviour {
	SpriteRenderer spriteRenderer;
	
	public string pieceName = "face";

	const int downCnst = 0;
	const int leftConst = 1;
	const int rightConst = 2;
	const int upConst = 3;


	int currDir = 0;

	PlayerMovement playerMoveScript;

	Sprite[] sprites = new Sprite[4];

	// Use this for initialization
	void Start () {
		sprites[0] =  Resources.Load<Sprite> ("Sprites/" +  PlayerData.TextureName + "/" + PlayerData.TextureName + "_"+ pieceName +"_south");
		sprites[1] =  Resources.Load<Sprite> ("Sprites/" +  PlayerData.TextureName + "/" + PlayerData.TextureName + "_"+ pieceName +"_west");
		sprites[2] =  Resources.Load<Sprite> ("Sprites/" +  PlayerData.TextureName + "/" + PlayerData.TextureName + "_"+ pieceName +"_east");
		sprites[3] =  Resources.Load<Sprite> ("Sprites/" +  PlayerData.TextureName + "/" + PlayerData.TextureName + "_"+ pieceName +"_north");

		// Set up references.
		spriteRenderer = GetComponent<SpriteRenderer> ();
		spriteRenderer.sprite = sprites [0];


		playerMoveScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerMoveScript.GetCurrDir() != currDir) {
			currDir = playerMoveScript.GetCurrDir();

			spriteRenderer.sprite = sprites [currDir];
		}
	}
}
