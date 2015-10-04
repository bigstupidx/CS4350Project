using UnityEngine;
using System.Collections;

public class CameraAutonomous : MonoBehaviour {
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
		durationTime = Time.time - startTime ;
		if (durationTime > 0.0f && durationTime < 4.0f) {
			startPt = new Vector3 (-0.508f, -0.045f, 0.349f);
			rotateAngle = new Vector3 (0.0f, 0.0f, 0.0f);
			moveCamera (startPt, rotateAngle);
		} else if (durationTime > 4.0f && durationTime < 20.0f) {
			cameraObject.transform.Translate (cam_movement_z * -0.03f * Time.deltaTime);
		} else if (durationTime > 20.0f && durationTime < 21.0f) {
			rotateAngle = new Vector3 (55.0f, 130.0f, 0.0f);
			startPt = new Vector3 (0.353f, 0.22f, 0.139f);
			moveCamera (startPt, rotateAngle);
		} else if (durationTime > 21.0f && durationTime < 40.0f) {
			cameraObject.transform.Translate (cam_movement_x * 0.03f * Time.deltaTime);
		} else if (durationTime > 40.0f && durationTime < 41.0f) {
			rotateAngle = new Vector3 (55.0f, 220.0f, 0.0f);
			startPt = new Vector3 (0.521f, 0.147f, -0.069f);
			moveCamera (startPt, rotateAngle);
		} else if (durationTime > 42.0f && durationTime < 61.0f) {
			cameraObject.transform.Translate (cam_movement_x * 0.03f * Time.deltaTime);
		}

//			else if (durationTime > 11.5f && durationTime < 18.5f) {
//			cameraObject.transform.Translate (cam_movement_x * 0.05f * Time.deltaTime);
//		} else if (durationTime > 24.0f && durationTime < 29.7f) {
//			cameraObject.transform.Translate (cam_movement_x * 0.05f * Time.deltaTime);
//		} else if (durationTime > 30.0f && durationTime < 46.0f) {
//			cameraObject.transform.Translate (cam_movement_z * 0.05f * Time.deltaTime);
//		} else if (durationTime > 46.0f && durationTime < 49.0f) {
//			cameraObject.transform.Rotate (cam_movement_y, 30.0f * Time.deltaTime);
//		} else if (durationTime > 51.0f && durationTime < 57.0f) {
//			cameraObject.transform.Rotate (cam_movement_y, 30.0f * Time.deltaTime);
//		} else if (durationTime > 57.0f && durationTime < 70.0f) {
//			cameraObject.transform.Translate (cam_movement_z * 0.03f * Time.deltaTime);
//		}
	}
	void moveCamera(Vector3 startPt, Vector3 rotationAngle) {
		cameraObject.transform.position = startPt;
		cameraObject.transform.rotation = Quaternion.Euler (rotationAngle);
	}
}
