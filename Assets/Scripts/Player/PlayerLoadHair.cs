using UnityEngine;
using System.Collections;

public class PlayerLoadHair : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.GetComponent<PlayerSpritePiece> ().pieceName = PlayerData.HairTextureName;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
