using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInFadeOut : MonoBehaviour {

	public bool isActivated = false;
	public bool isFadingOn = false;
	public float alpha;

	private Color defaultColor;
	private float fadeSpeed = 0.7f;

	private FeedTextFromObject feedText;

	public GameObject button;
		
	// Use this for initialization
	void Start () {
		feedText = GameObject.Find ("ObjectRespond").GetComponent<FeedTextFromObject> ();
		defaultColor = new Color( (148.0f/255.0f) , (159.0f/255.0f), (213.0f/255.0f), 1.0f);
		gameObject.GetComponent<Image>().color = new Color( (148.0f/255.0f) , (159.0f/255.0f), (213.0f/255.0f), 0.0f);
		alpha = 0.0f;
		isActivated = false;
		button.SetActive (false);
	}

	public void TurnOnTextbox(bool _fadingOption)
	{
		isActivated = true;
		button.SetActive (true);
		isFadingOn = _fadingOption;
		gameObject.GetComponent<Image>().color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 1.0f);
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
			if(!isFadingOn) // fading is OFF
			{
				PlayerData.MoveFlag = false;
			}
			else if (isFadingOn && gameObject.GetComponent<Image> ().color.a > 0.0f) {
				button.SetActive(false);
				alpha = gameObject.GetComponent<Image> ().color.a;
				alpha -= (fadeSpeed * Time.deltaTime);
				gameObject.GetComponent<Image> ().color = new Color (defaultColor.r, defaultColor.g, defaultColor.b, alpha);
				feedText.setAlpha(alpha);
				PlayerData.MoveFlag = true;
			}
		}
	}

	void LateUpdate()
	{
		if (gameObject.GetComponent<Image> ().color.a <= 0.0f) {
			isActivated = false;
			isFadingOn = false;
		}
	}
}
