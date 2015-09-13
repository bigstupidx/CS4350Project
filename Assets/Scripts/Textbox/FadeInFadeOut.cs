using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInFadeOut : MonoBehaviour {

	public static bool isActivated = false;
	public static float alpha;

	private Color defaultColor;
	private float fadeSpeed = 0.0f;
		
		// Use this for initialization
	void Start () {
		defaultColor = gameObject.GetComponent<Image> ().material.color;
		gameObject.GetComponent<Image>().material.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 0.0f);
		alpha = 0.0f;
	}

	public void TurnOnTextbox()
	{
		isActivated = true;
		gameObject.GetComponent<Image>().material.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, defaultColor.a);

		int wordCount = transform.GetComponentInChildren<Text>().text.Length;
		fadeSpeed = 0.35f;
		
		if(wordCount >= 10 && wordCount <= 20)
			fadeSpeed = 0.25f;
		else if( wordCount >= 20 )
			fadeSpeed = 0.15f;

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp (KeyCode.Space)) {
			if(isActivated == false || FeedTextFromObject.moreThanOneLine == false)
			{
				TurnOnTextbox();
			}
		}

		if (isActivated) {
			if ( FeedTextFromObject.moreThanOneLine == false &&  gameObject.GetComponent<Image> ().material.color.a > 0.0f) {
				alpha = gameObject.GetComponent<Image> ().material.color.a;
				alpha -= (fadeSpeed * Time.deltaTime);
				gameObject.GetComponent<Image> ().material.color = new Color (defaultColor.r, defaultColor.g, defaultColor.b, alpha);
			}

			if (gameObject.GetComponent<Image> ().material.color.a <= 0.0f) {
				isActivated = false;
			}
		} else {
			gameObject.GetComponentInChildren<FeedTextFromObject>().SetText("");
		}

	
	}
}
