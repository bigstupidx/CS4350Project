using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class TimeCounter : MonoBehaviour {

	int hour;
	int minutes;
	int seconds;

	Text time;
	// Use this for initialization
	void Awake () {
		time = gameObject.GetComponent<Text> ();
		time.text = "00:00:00";
	}

	void Start()
	{
		if (EndingController.instance.isChapter2Activated) {
			DateTime currTimeStamp;
			bool hasItem = GameController.instance.timeStamp.TryGetValue("StartGame", out currTimeStamp);
			if (hasItem) {
				this.setTime(currTimeStamp.Hour, currTimeStamp.Minute, currTimeStamp.Second);
			}
			else
			{
				this.setTime(GameController.instance.GetCurrentObjectTime().Hour,
				             GameController.instance.GetCurrentObjectTime().Minute, 
				             GameController.instance.GetCurrentObjectTime().Second);
			}
		}
	}

	public void setTime(int _hour, int _min, int _sec)
	{
		hour = _hour;
		minutes = _min;
		seconds = _sec;
	}

	// Update is called once per frame
	void Update () {

		if (!EndingController.instance.isChapter2Activated) {
			hour = GameController.instance.GetActualTime (2);
			minutes = GameController.instance.GetActualTime (1);
			seconds = GameController.instance.GetActualTime (0);
		}

		time.text = string.Format ("{0:00}:{1:00}:{2:00}",hour, minutes, seconds); 
	}
}
