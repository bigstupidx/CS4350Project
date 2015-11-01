using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BubbleBehaviour : MonoBehaviour {

	public GameObject target;
	public GUITexture overlay;
	
	public bool canTrigger = false;
	public bool itemStatus = false;

	public bool alreadySelected = false;



	void Start () {

		if (target == null) {
			target = GameObject.FindGameObjectWithTag("Player");
		}

		if (!GameController.instance.isAndroidVersion)
			gameObject.SetActive (false);

		if (Application.loadedLevelName.Contains ("GameScene") && overlay == null) {
			overlay = LevelHandler.Instance.overlay;
		}

		Reset();
	}

	public void TouchDetected()
	{
		target.GetComponent<PlayerMovement> ().StopMoving ();
		target.GetComponent<PlayerMovement> ().enabled = false;
		PlayerData.MoveFlag = false;
		alreadySelected = true;
		if(!GameObject.Find ("TextBox_Android").GetComponent<FadeInFadeOut> ().isFadingOn)
			target.GetComponent<Displaytextbox> ().TriggerTextbox ();
	}

	public void TurnOffButton()
	{
		transform.GetComponent<Image> ().enabled = false;
		canTrigger = false;
	}
	public void TurnOnButton()
	{
		transform.GetComponent<Image> ().enabled = true;
		canTrigger = false;
		target.GetComponent<PlayerMovement> ().enabled = true;
		PlayerData.MoveFlag = true;
	}
	
	void Reset()
	{
		transform.GetComponent<Image> ().enabled = true;
		gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
	}
	
	void Update()
	{
		if (GameController.instance.isAndroidVersion) {
			if (GameObject.Find ("TextBox_Android").GetComponent<FadeInFadeOut> ().isActivated || overlay.gameObject.activeSelf) {
				if (transform.GetComponent<Image> ().enabled)
					TurnOffButton ();
			} else {
				if (!transform.GetComponent<Image> ().enabled)
					TurnOnButton ();
			}
		}

	}
}
