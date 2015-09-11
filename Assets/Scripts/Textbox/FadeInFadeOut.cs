using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInFadeOut : MonoBehaviour {

	public static bool isActivated = false;
	public static GameObject interactingObject;
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Image>().material.color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp (KeyCode.Space)) {
			if(isActivated == false)
			{
				isActivated = true;
				gameObject.GetComponent<Image>().material.color = new Color(255.0f, 255.0f, 255.0f, 255.0f);
				//interactingObject = GameObject.FindGameObjectWithTag("Player").GetComponent<triggerTextBox>().currentObject;
			}
		}

		if (isActivated) {
			if (gameObject.GetComponent<Image> ().material.color.a > 0.0f) {
				float alpha = gameObject.GetComponent<Image> ().material.color.a;
				alpha -= (100 * Time.deltaTime);
				gameObject.GetComponent<Image> ().material.color = new Color(255.0f, 255.0f, 255.0f, alpha);
			}

			if(gameObject.GetComponent<Image>().material.color.a <= 0.0f){
				isActivated = false;
			}
		}

	
	}
}
