using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlackScreenFading : MonoBehaviour {

	bool isTransferingToMain = false;
	bool isClearing = true;
	public float alphaChgPerSec = 0.3f;
	Image blackScreenImage;
	//float timer = 0.0f;
	// Use this for initialization
	void Start () {

		blackScreenImage = transform.GetComponent<Image> ();
		blackScreenImage.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			isClearing = false;
			isTransferingToMain = true;
		}

		if (isClearing) {
			// Fade Out
			Color tempColor = blackScreenImage.color;
			tempColor.a = Mathf.Max(0.0f,tempColor.a - alphaChgPerSec*Time.deltaTime);
			blackScreenImage.color = tempColor;
		} else {

			// Fade In
			Color tempColor = blackScreenImage.color;
			tempColor.a = Mathf.Min(1.0f,tempColor.a + alphaChgPerSec*Time.deltaTime);
			blackScreenImage.color = tempColor;

			if(isTransferingToMain && blackScreenImage.color.a>=1.0f){
				Application.LoadLevel("MainScene");
			}
		}
	}
}
