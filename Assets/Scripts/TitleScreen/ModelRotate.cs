using UnityEngine;
using System.Collections;

public class ModelRotate : MonoBehaviour {
	public GameObject blackScreen;

	public float[] angles = {0,20,40,60,80,100,120,140};
	int currAngle = 0;
	public float displayTime = 5.0f;
	public float blackOutTime = 0.05f;
	float timeLeft = 0;
	float blackScreenTimeLeft = 0;
	GameObject[] objArr;
	int currIndex = 0;
	// Use this for initialization
	void Start () {
		timeLeft = displayTime;
		blackScreen.SetActive (false);

		Transform[] tArr = transform.GetComponentsInChildren<Transform> ();
		GameObject[] objTempArr = new GameObject[tArr.Length];
		int i = 0;
		foreach(Transform t in tArr){
			if(!t.gameObject.Equals(transform.gameObject)&& t.name.Contains("obj")){
				Debug.Log (t.name);
				objTempArr[i] = t.gameObject;
				objTempArr[i].SetActive(false);
				i++;
			}
		}
		objArr = new GameObject[i];
		for (int j=0; j<i; j++) {
			objArr[j] = objTempArr[j];
		}
		Debug.Log (objArr.Length);
		SelectRandomObject ();
	}

	void SelectRandomObject(){
		objArr[currIndex].SetActive (false);
		currIndex = (int)Random.Range (0.0f, objArr.Length - 0.1f);
		objArr [currIndex].SetActive (true);
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
			SelectRandomObject();
			timeLeft = displayTime;
			blackScreenTimeLeft = blackOutTime;
			blackScreen.SetActive(true);

		}
	}
}
