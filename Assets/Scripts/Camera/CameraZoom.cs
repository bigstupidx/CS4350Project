using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

	CameraFollow mainCamFollowScript;
	Transform playerTransform;
	PlayerMovement playerMoveScript;
	bool isLookingHere = false;

	public float lookAtMeTime = 3.0f;

	float lookAtMeTimer = 0.0f;


	// Use this for initialization
	void Start () {
		mainCamFollowScript = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow> ();
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		playerTransform = player.transform;
		playerMoveScript = player.GetComponent <PlayerMovement>();
	}

	void OnTriggerEnter(Collider other){
		mainCamFollowScript.ChangeTarget (transform);
		isLookingHere = true;
		PlayerData.MoveFlag = false;
		playerMoveScript.StopMoving ();

	}
	
	// Update is called once per frame
	void Update () {
		if (isLookingHere) {
			lookAtMeTimer += Time.deltaTime;
			if(lookAtMeTimer >= lookAtMeTime){
				mainCamFollowScript.ChangeTarget (playerTransform);
				GameObject.Destroy(transform.gameObject);
				PlayerData.MoveFlag = true;
			}
		}

	}
}
