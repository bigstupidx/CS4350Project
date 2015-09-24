using UnityEngine;
using System.Collections;

public class PlayerBlink : MonoBehaviour {
	public float blinkDuration = 0.15f;
	public float maxTimeBeforeBlink = 3.0f;
	public float minTimeBeforeBlink = 0.5f;
	public float chanceToLook = 0.5f;
	public float lookTime = 1.0f;

	float timer = 0; 
	bool isEyeOpen = true;

	Sprite[] eyeOpenSprites = new Sprite[4];
	Sprite[] eyeCloseSprites = new Sprite[4];

	public string eyeOpenFile = "face1";
	public string eyeCloseFile = "face2";

	public PlayerSpritePiece targetSprite;
	public GameObject eyeDot;
	PlayerEyeMovement eyeMoveScript;

	// Use this for initialization
	void Start () {
		eyeOpenSprites[0] =  Resources.Load<Sprite>(PlayerData.FormSpritePath(eyeOpenFile, 0));
		eyeOpenSprites[1] =  Resources.Load<Sprite>(PlayerData.FormSpritePath(eyeOpenFile, 1));
		eyeOpenSprites[2] =  Resources.Load<Sprite>(PlayerData.FormSpritePath(eyeOpenFile, 2));
		eyeOpenSprites[3] =  null;

		eyeCloseSprites[0] =  Resources.Load<Sprite>(PlayerData.FormSpritePath(eyeCloseFile, 0));
		eyeCloseSprites[1] =  Resources.Load<Sprite>(PlayerData.FormSpritePath(eyeCloseFile, 1));
		eyeCloseSprites[2] =  Resources.Load<Sprite>(PlayerData.FormSpritePath(eyeCloseFile, 2));
		eyeCloseSprites[3] =  null;

		RandomizeTimeBeforeBlink ();
		eyeDot.SetActive (true);

		eyeMoveScript = eyeDot.GetComponent<PlayerEyeMovement> ();
	}

	void RandomizeTimeBeforeBlink(){
		timer = Random.Range(minTimeBeforeBlink, maxTimeBeforeBlink);
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		if(timer <= 0){
			if(isEyeOpen){
				targetSprite.SwitchSprites(eyeCloseSprites);
				isEyeOpen = false;
				timer = blinkDuration;
				eyeDot.SetActive (false);


			}
			else{
				targetSprite.SwitchSprites(eyeOpenSprites);
				isEyeOpen = true;
				RandomizeTimeBeforeBlink();
				eyeDot.SetActive (true);

				if(eyeMoveScript.isTargetting){
					eyeMoveScript.isTargetting = false;
				}else{
					float rand = Random.Range (0.0f, 1.0f);
					if(rand < chanceToLook){
						eyeMoveScript.isTargetting = true;
						timer = lookTime;
					}
				}
			}
		}
	}
}
