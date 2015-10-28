using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransitionTimerBehaviour : MonoBehaviour {

	private Text timeText;
	public int startTimeHour;
	public int startTimeMinutes;

	private float startTime;
	void Start () {
		timeText = this.GetComponent<Text> ();
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if ((Time.time - startTime) > 1.0f){
			startTimeMinutes++;
			startTime = Time.time;
		}

		if (startTimeMinutes == 60) {
			startTimeHour++;
			startTimeMinutes = 0;
		}

		string content = startTimeHour + " : " + string.Format("{0:00}", startTimeMinutes) + " pm";
		timeText.text = content;
	}
}
