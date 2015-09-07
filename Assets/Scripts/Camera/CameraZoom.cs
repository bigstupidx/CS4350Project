using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {
	GameObject mainCamObj;
	Camera mainCam;
	Vector3 origPos;
	Quaternion origRot;
	float origSize;
	Vector3 targetPos;
	Vector3 targetRot = new Vector3(0,0,0);

	int stage = 0; // 1 - zooming , 2 - showing text, 3 - zooming out

	public float viewSize = 1.0f;
	public float speed = 3.0f;       

	float timer = 0;

	// Use this for initialization
	void Start () {
		mainCamObj = GameObject.FindWithTag ("MainCamera");
		mainCam = mainCamObj.GetComponent<Camera> ();

		origRot = mainCamObj.transform.rotation;
		origSize = mainCam.orthographicSize;

		targetPos = transform.position;
		targetPos.z -= transform.GetComponent<SphereCollider>().radius;

		Debug.Log (targetPos);
	}

	void OnTriggerEnter(Collider other){
		mainCamObj.GetComponent<CameraFollow> ().DisableFollow ();
		origPos = mainCamObj.GetComponent<CameraFollow> ().GetTargetCamPos();
		PlayerData.MoveFlag = false;

		stage = 1;
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (stage == 1) {
			mainCamObj.transform.position = Vector3.Lerp (mainCamObj.transform.position, targetPos, Time.deltaTime * speed);
			RotateCamera (true);
			mainCam.orthographicSize += (viewSize - origSize) / speed * Time.deltaTime;

			if (timer >= speed) {
				timer = 0;
				stage = 2;
			}
		} else if (stage == 2) {
			if (timer >= speed) {
				timer = 0;
				stage = 3;
			}
		}
		else if (stage == 3) {
			mainCamObj.transform.position = Vector3.Lerp (targetPos, origPos, Time.deltaTime * speed);
			RotateCamera (false);
			mainCam.orthographicSize += (origSize - viewSize) / speed * Time.deltaTime;

			if (timer >= speed) {
				mainCam.transform.position.Set(origPos.x, origPos.y, origPos.z);
				mainCam.transform.rotation.eulerAngles.Set (origRot.eulerAngles.x, origRot.eulerAngles.y, origRot.eulerAngles.x);
				mainCam.orthographicSize = origSize;
				mainCamObj.GetComponent<CameraFollow> ().EnableFollow ();
				PlayerData.MoveFlag = true;
				timer = 0;
				stage = 0;
				Destroy(transform.gameObject);
			}
		}

	}

	void RotateCamera(bool isZoomingIn){
		float rotAmountX = (targetRot.x - origRot.eulerAngles.x) / speed;
		float rotAmountY = (targetRot.y - origRot.eulerAngles.y) / speed;
		float rotAmountZ = (targetRot.z - origRot.eulerAngles.z) / speed;

		if (!isZoomingIn) {
			rotAmountX = -rotAmountX;
			rotAmountY = -rotAmountY;
			rotAmountZ = -rotAmountZ;

		}

		Vector3 rot = new Vector3 ();

		rot.x = rotAmountX * Time.deltaTime;
		if(rot.x > 360)
			rot.x -= 360;
		else if(rot.y < 360)
			rot.x += 360;


		rot.y = rotAmountY * Time.deltaTime;
		if(rot.y > 360)
			rot.y -= 360;
		else if(rot.y < 360)
			rot.y += 360;

		rot.z = rotAmountZ * Time.deltaTime;
		if(rot.z > 360)
			rot.z -= 360;
		else if(rot.z < 360)
			rot.z += 360;



		mainCamObj.transform.Rotate(rot);
	}
}
