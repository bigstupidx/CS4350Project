using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMute: MonoBehaviour {
	public GameObject muteButton;
	public GameObject unmuteButton;

	void Awake() {
		if (AudioListener.pause) {
			muteButton.SetActive (false);
			unmuteButton.SetActive (true);
		} else {
			muteButton.SetActive(true);
			unmuteButton.SetActive(false);
		}
	}
	
	public void MutePressed() {
		AudioListener.pause = true;
		muteButton.SetActive(false);
		unmuteButton.SetActive(true);
	}

	public void UnmutePressed() {	
		AudioListener.pause = false;
		muteButton.SetActive(true);
		unmuteButton.SetActive(false);
	}
}
