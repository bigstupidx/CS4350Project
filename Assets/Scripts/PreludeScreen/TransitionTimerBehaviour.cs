using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransitionTimerBehaviour : MonoBehaviour {

	public GameObject reference;
	private Text timeText;
	public int startTimeHour;
	public int startTimeMinutes;
	public int startTimeSeconds;

	private float startTime;
	private bool isAM = false;

	private int cutOff = 0;
	void Start () {
		timeText = this.GetComponent<Text> ();
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if ((Time.time - startTime) > 1.0f){
			startTimeSeconds++;
			startTime = Time.time;
			cutOff++;
		}
		if (startTimeSeconds == 60) {
			startTimeMinutes++;
			startTimeSeconds = 0;
		}

		if (startTimeMinutes == 60) {
			startTimeHour++;
			startTimeMinutes = 0;
		}

		if (startTimeHour == 24) {
			startTimeHour = 0;
		}


		string content = string.Format ("{0:00}:{1:00}:{2:00}",startTimeHour, startTimeMinutes, startTimeSeconds); 
		timeText.text = content;

		if (cutOff >= 4)
			reference.GetComponent<FadeToClear> ().TransitToNextScene ();

	}
}
