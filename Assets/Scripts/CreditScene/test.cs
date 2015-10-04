using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {
	GameObject cameraObject;
	private float startTime;
	private float durationTime;
	private Vector3 cam_movement_z;
	private Vector3 cam_movement_y;
	private Vector3 cam_movement_x;
	private float _pi;
	private Vector3 rotateAngle;
	private Vector3 startPt;
	// Use this for initialization
	void Start () {
		_pi = 3.142f;
		cameraObject = GameObject.FindGameObjectWithTag ("MainCamera");
		startTime = Time.time;
		cam_movement_z = new Vector3 (0.0f, 0.0f, 1.0f);
		cam_movement_x = new Vector3 (1.0f, 0.0f, 0.0f);
		cam_movement_y = new Vector3 (0.0f, 1.0f, 0.0f);
		
		
	}
	//	cameraObject.transform.position = startPt;
	//	cameraObject.transform.rotation = Quaternion.Euler(rotate_y);
	// Update is called once per frame
	void Update () {
		durationTime = Time.time - startTime;
		if (durationTime > 0.0f && durationTime < 20.0f) {
			cameraObject.transform.Translate(cam_movement_x * 0.03f * Time.deltaTime);
		}
	
	}
}
