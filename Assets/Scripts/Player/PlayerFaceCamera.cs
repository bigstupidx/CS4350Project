using UnityEngine;
using System.Collections;

public class PlayerFaceCamera : MonoBehaviour {

	GameObject cameraObject;

	// Use this for initialization
	void Start () {
		cameraObject = GameObject.FindGameObjectWithTag ("MainCamera");


	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(transform.position + cameraObject.transform.rotation * Vector3.back,
		                 cameraObject.transform.rotation * Vector3.up);

		transform.Rotate (new Vector3(0,180,0));
	}
}
