﻿using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	public Transform target;            // The position that that camera will be following.
	public float followSpeed = 5f;        // The speed with which the camera will be following.
	public float changeTargetSpeed = 2.5f;
	
	Vector3 offset;                     // The initial offset from the target.

	Vector3 targetCamPos;

	float smoothing;

	Transform origTarget;

	void Start ()
	{
		// Calculate the initial offset.
		offset = transform.position - target.position;
		//Debug.Log (offset);
		smoothing = followSpeed;
		origTarget = target;
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