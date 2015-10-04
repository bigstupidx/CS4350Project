using UnityEngine;
using System.Collections;

public class CameraAutonomous : MonoBehaviour {
	GameObject cameraObject;
	private float startTime;
	private float durationTime;
	private Vector3 cam_movement_z;
	private Vector3 cam_movement_y;
	private Vector3 cam_movement_x;
	private Vector3 rotate_y;
	private float _pi;
	// Use this for initialization
	void Start () {
		_pi = 3.142f;
		cameraObject = GameObject.FindGameObjectWithTag ("MainCamera");
		startTime = Time.time;
		cam_movement_z = new Vector3 (0.0f, 0.0f, 1.0f);
		cam_movement_x = new Vector3 (1.0f, 0.0f, 0.0f);
		cam_movement_y = new Vector3 (0.0f, 1.0f, 0.0f);

	}
	
	// Update is called once per frame
	void Update () {
		durationTime = Time.time - startTime ;
		if (durationTime > 8.0f && durationTime < 11.0f) {
			cameraObject.transform.Rotate (cam_movement_y, 30.0f * Time.deltaTime);
		} else if (durationTime > 11.5f && durationTime < 18.5f) {
			cameraObject.transform.Translate (cam_movement_x * 0.05f * Time.deltaTime);
		} else if (durationTime > 24.0f && durationTime < 29.7f) {
			cameraObject.transform.Translate (cam_movement_x * 0.05f * Time.deltaTime);
		} else if (durationTime > 30.0f && durationTime < 46.0f) {
			cameraObject.transform.Translate (cam_movement_z * 0.05f * Time.deltaTime);
		} else if (durationTime > 46.0f && durationTime < 49.0f) {
			cameraObject.transform.Rotate (cam_movement_y, 30.0f * Time.deltaTime);
		} else if (durationTime > 51.0f && durationTime < 57.0f) {
			cameraObject.transform.Rotate (cam_movement_y, 30.0f * Time.deltaTime);
		} else if (durationTime > 57.0f && durationTime < 70.0f) {
			cameraObject.transform.Translate (cam_movement_z * 0.03f * Time.deltaTime);
		}
	}
}
