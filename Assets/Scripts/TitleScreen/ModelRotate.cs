using UnityEngine;
using System.Collections;

public class ModelRotate : MonoBehaviour {
	public GameObject blackScreen;

	public float[] angles = {0,45,90,135, 180};
	int currAngle = 0;
	public float displayTime = 5.0f;
	public float blackOutTime = 0.05f;
	float timeLeft = 0;
	float blackScreenTimeLeft = 0;


	// Use this for initialization
	void Start () {
		timeLeft = displayTime;
		blackScreen.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		if (blackScreenTimeLeft >= 0) {
			blackScreenTimeLeft -= Time.deltaTime;

			if(blackScreenTimeLeft <= 0)
				blackScreen.SetActive(false);
			else
				return;
		}


		timeLeft -= Time.deltaTime;
		if (timeLeft <= 0)
		{
			int oldAngle = currAngle;

			while(oldAngle == currAngle)
				currAngle = Random.Range(0, angles.Length-1);

			transform.Rotate (Vector3.up *  -angles[oldAngle], Space.World);
			transform.Rotate (Vector3.up * angles[currAngle], Space.World);

			timeLeft = displayTime;
			blackScreenTimeLeft = blackOutTime;
			blackScreen.SetActive(true);

		}
	}
}
