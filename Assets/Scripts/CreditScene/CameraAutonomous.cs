using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraAutonomous : MonoBehaviour {

	public GameObject cameraObject;
	public GameObject background;
	public float alphaValue;

	private int currentState;
	private Color bg_color;
	private bool pos;

	private float stateTimer;
	private float threshold;
	private float _pi;

	private Vector3[] cam_movement = new [] { 
		new Vector3 (1.0f, 0.0f, 0.0f),
		new Vector3 (0.0f, 1.0f, 0.0f),
		new Vector3 (0.0f, 0.0f, 1.0f)
	};
	private Vector3[] startArr = new [] { 
		new Vector3 (-14.0f, 20.0f, -11.0f), 
		new Vector3 (-10.0f, 7.0f, -19.0f), 
		new Vector3 (-12.4f, 1.9f, -12.5f),
		new Vector3 (0.0f, 0.0f, 0.0f)
	};
	private Vector3[] rotateArr = new [] { 
		new Vector3 (55.0f, 30.0f, 0.0f),
		new Vector3 (25.0f, 130.0f, 0.0f),
		new Vector3 (15.0f, 0.0f, 0.0f),
		new Vector3 (0.0f, 0.0f, 0.0f)
	};
	// Use this for initialization
	void Start () {
		_pi = 3.142f;
		threshold = 1.0f;
		currentState = -1;
		stateTimer = 10.0f;
		bg_color = background.GetComponent<Image> ().color;
		pos = true;
	}
//	cameraObject.transform.position = startPt;
//	cameraObject.transform.rotation = Quaternion.Euler(rotate_y);
	// Update is called once per frame
	void Update () {
		switch (currentState) {
		case 0: moveCamera(startArr[0], rotateArr[0]); break;
		case 1: dragCamera(cam_movement[0], 1.0f); fadeIn (); break;
		case 2: dragCamera(cam_movement[0], 1.0f);break;
		case 3: dragCamera(cam_movement[0], 1.0f); fadeOut(); pos = true; break;
		case 4: moveCamera(startArr[1], rotateArr[1]); dragCamera(cam_movement[0], -1.5f); fadeIn ();  break;
		case 5: dragCamera(cam_movement[0], -1.5f);break;
		case 6: dragCamera(cam_movement[0], -1.5f); fadeOut(); pos = true; break;
		case 7: moveCamera (startArr[2], rotateArr[2]);dragCamera(cam_movement[0], 1.0f); fadeIn ();  break;
		case 8: dragCamera(cam_movement[0], 1.0f); break;
		case 9: dragCamera(cam_movement[0], 1.0f); fadeOut(); break;
		}

		if (stateTimer > 0.0f) {
			stateTimer -= 10.0f * Time.deltaTime;
		} else {
			currentState++;
			switch (currentState) {
			case 0: stateTimer = 40.0f;  break;
			case 1: stateTimer = 10.0f; break;
			case 2: stateTimer = 155.0f; break;
			case 3: stateTimer = 10.0f; break;
			case 4: stateTimer = 10.0f; break;
			case 5: stateTimer = 155.0f; break;
			case 6: stateTimer = 10.0f; break;
			case 7: stateTimer = 10.0f; break;
			case 8: stateTimer = 155.0f; break;
			case 9: stateTimer = 10.0f; break;
			default: stateTimer = 10000.0f; break;
			}
		}
	}

	void moveCamera(Vector3 startPt, Vector3 rotationAngle) {
		if (pos) {
			cameraObject.transform.position = startPt;
			cameraObject.transform.rotation = Quaternion.Euler (rotationAngle);
		}
	}

	void dragCamera (Vector3 direction, float speed) {
		pos = false;
		cameraObject.transform.Translate (direction * speed * Time.deltaTime);
	}

	void fadeIn() {
		if (bg_color.a < alphaValue) {
			threshold = 0.0f;
		}
		bg_color.a -= threshold * Time.deltaTime;
		threshold = 1.0f;
		background.GetComponent<Image> ().color = bg_color;
	}

	void fadeOut() {
		bg_color.a += 1.0f * Time.deltaTime;
		background.GetComponent<Image> ().color = bg_color;
	}
}
