using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInFadeOut : MonoBehaviour {

	public bool isActivated = false;
	public bool isFadingOn = true;
	public float alpha;

	private Color defaultColor;
	private float fadeSpeed = 0.0f;

	private FeedTextFromObject feedText;
		
	// Use this for initialization
	void Start () {
		feedText = GameObject.Find ("ObjectRespond").GetComponent<FeedTextFromObject> ();
		defaultColor = gameObject.GetComponent<Image> ().material.color;
		gameObject.GetComponent<Image>().material.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 0.0f);
		alpha = 0.0f;
		isActivated = false;
	}

	public void TurnOnTextbox(bool _fadingOption)
	{
		isActivated = true;
		isFadingOn = _fadingOption;
		gameObject.GetComponent<Image>().material.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 1.0f);

		int wordCount = transform.GetComponentInChildren<Text>().text.Length;
		fadeSpeed = 0.40f;
		
		if(wordCount >= 10 && wordCount <= 20)
			fadeSpeed = 0.35f;
		else if( wordCount >= 20 )
			fadeSpeed = 0.30f;
	}

	public bool getStatus()
	{
		return isActivated;
	}

	public float getAlpha()
	{
		return alpha;
	}
	
	// Update is called once per frame
	void Update () {

		if (isActivated) {
			if(!isFadingOn)
			{
				PlayerData.MoveFlag = false;
			}
			else if (isFadingOn && gameObject.GetComponent<Image> ().material.color.a > 0.0f) {
				alpha = gameObject.GetComponent<Image> ().material.color.a;
				alpha -= (fadeSpeed * Time.deltaTime);
				gameObject.GetComponent<Image> ().material.color = new Color (defaultColor.r, defaultColor.g, defaultColor.b, alpha);
				feedText.setAlpha(alpha);
				PlayerData.MoveFlag = true;
			}

			if (gameObject.GetComponent<Image> ().material.color.a <= 0.0f) {
				isActivated = false;
			}
		}
	}
}
