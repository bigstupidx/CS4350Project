using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	public readonly static Vector3 fixedOffsetLevel2 = new Vector3(0.08f, 5.08f, -3.98f);
	public readonly static Vector3 fixedOffsetLevel1 = new Vector3(1.31f, 15.18f, -19.64f);
	public readonly static Vector3 fixedOffsetLevel0 = new Vector3 (0.0f, 6.0f, 6.4f);

	public Transform target;            // The position that that camera will be following.
	public float followSpeed = 5f;        // The speed with which the camera will be following.
	public float changeTargetSpeed = 2.5f;
	
	public Vector3 offset;                     // The initial offset from the target.

	Vector3 targetCamPos;

	float smoothing;

	Transform origTarget;

	void Start ()
	{
		offset = fixedOffsetLevel0;
		smoothing = followSpeed;
		origTarget = target;
	}

	public void switchOffset(int level) {
		if (level == 2) {
			offset = fixedOffsetLevel2;
		} else if (level == 1) {
			offset = fixedOffsetLevel1;
		} else if (level == 0) {
			offset = fixedOffsetLevel2;
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
		// Create a postion the camera is aiming for based on the offset from the target.
		targetCamPos = target.position + offset;
		
		// Smoothly interpolate between the camera's current position and it's target position.
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);

	}
}