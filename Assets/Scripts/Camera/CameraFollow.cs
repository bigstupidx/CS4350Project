using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{	
	public readonly static Vector3 fixedOffsetLevel6 = new Vector3(-1.6f, 17.5f, -20.2f);
	public readonly static Vector3 fixedOffsetLevel5 = new Vector3(0.4f, 22.37f, -28.1f);
	public readonly static Vector3 fixedOffsetLevel4 = new Vector3(0.2f, 20.56f, -25.44f);
	public readonly static Vector3 fixedOffsetLevel3 = new Vector3(5.6f, 13.7f, 14.92f);
	public readonly static Vector3 fixedOffsetLevel2 = new Vector3(0.08f, 5.08f, -3.98f);
	public readonly static Vector3 fixedOffsetLevel1 = new Vector3(1.31f, 15.18f, -19.64f);
	public readonly static Vector3 fixedOffsetLevel0 = new Vector3 (5.46f, 4.02f, 0.01f);

	// camera mode
	public static int cameraMode;

	// camera setting for ground outside game scene
	public static float fixedXPosition = -2.00f;
	public static float fixedYLowPosition = 3.45f;
	public static float fixedYHighPosition = 5.80f;
	public static float fixedZPosition = -55.2f;
	public static float maxDiffPosition = 25.0f;
	public static float maxFieldOfView = 60.0f;

	// camera setting for basement game scene
	public static float ZPosition = 3.19f;

	public Transform target;            // The position that that camera will be following.
	public float followSpeed = 150f;        // The speed with which the camera will be following.
	public float changeTargetSpeed = 2.5f;
	
	public Vector3 offset;                     // The initial offset from the target.

	Vector3 targetCamPos;

	float smoothing;

	Transform origTarget;

	void Start ()
	{
		smoothing = followSpeed;
		origTarget = target;
	}

	public void switchOffset(int level) {
		if (level == 6) {
			offset = fixedOffsetLevel6;
			cameraMode = 1;
		} else if (level == 5) {
			offset = fixedOffsetLevel5;
			cameraMode = 1;
		} else if (level == 4) {
			offset = fixedOffsetLevel4;
			cameraMode = 1;
		} else if (level == 3){
			offset = fixedOffsetLevel3;
			cameraMode = 2;
		} else if (level == 2) {
			offset = fixedOffsetLevel2;
			cameraMode = 1;
		} else if (level == 1) {
			offset = fixedOffsetLevel1;
			cameraMode = 1;
		} else if (level == 0) {
			offset = fixedOffsetLevel0;
			cameraMode = 3;
		} else {
			offset = fixedOffsetLevel2;
			cameraMode = 1;
		}
	}


	public void ChangeTarget(Transform newTarget){
		target = newTarget;
		smoothing = changeTargetSpeed;
	}
		
	public void ResetTarget(){
		target = origTarget;
		smoothing = followSpeed;
	}

	void FixedUpdate ()
	{
		// Ground outside the camera
		if (cameraMode == 2) {
			//Distance towards player
			float xDiff = target.position.x - transform.position.x;
			float yDiff = 0f;
			float zDiff = target.position.z - transform.position.z;
			
			Vector3 diffVector = new Vector3 (xDiff, yDiff, zDiff);
			float currentLength = diffVector.magnitude;

			float distDiff = maxDiffPosition - currentLength;
			Camera.main.fieldOfView = (distDiff/maxDiffPosition) * maxFieldOfView;

			targetCamPos.x = fixedXPosition;

			if(transform.rotation.eulerAngles.y > 135.0f && transform.rotation.eulerAngles.y <255.0f){
				targetCamPos.y = fixedYLowPosition;
			}else{
				targetCamPos.y = fixedYHighPosition;
			}
			targetCamPos.z = fixedZPosition;

			transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
			transform.LookAt (target);

		} else if (cameraMode == 1) {
			// Create a position the camera is aiming for based on the offset from the target.
			targetCamPos = target.position + offset;
			
			// Smoothly interpolate between the camera's current position and it's target position.
			transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);

		} else if (cameraMode == 3) {
			targetCamPos = target.position + offset;

			targetCamPos.z = ZPosition;

			transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
			transform.LookAt(target);
		}

	}
}