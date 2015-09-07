using UnityEngine;
using System.Collections;

public class RunningTrain : MonoBehaviour {

	public Vector3 directionVector = new Vector3(-1,0,0);
	public float timeBetweenRuns = 5.0f;
	public float runningTime = 15.0f;
	public float runningSpeedPerSec = 15.0f;

	MeshRenderer meshRenderer;
	Vector3 normalizedDirVector;
	Vector3 origPosition; 
	float timer;


	bool isRunning;

	// Use this for initialization
	void Start () {
		origPosition = transform.position;
	
		meshRenderer = transform.GetComponent<MeshRenderer> ();
		meshRenderer.enabled = false;

		normalizedDirVector = directionVector.normalized;

		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (isRunning) {
			transform.position = transform.position + (normalizedDirVector * runningSpeedPerSec * Time.deltaTime);

			if(timer >= runningTime){
				timer = 0;
				meshRenderer.enabled = false;
				transform.position = origPosition;
				isRunning = false;
			}
		} else {
			if(timer >= timeBetweenRuns){
				timer = 0;
				isRunning = true;
				meshRenderer.enabled=true;
			}
		}
	}
}
