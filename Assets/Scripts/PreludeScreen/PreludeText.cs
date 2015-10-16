using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PreludeText : MonoBehaviour {

	public bool isLeftText = true;
	public bool fadeInState = true;
	private float speed = 0.3f;

	private Vector3 leftPosition =  new Vector3 (100.0f, 50.0f, 0.0f);
	private Vector3 rightPosition = new Vector3 (800.0f, 50.0f, 0.0f);

	// Use this for initialization
	void Start () {
		transform.position = leftPosition;
	}

	void OnEnable()
	{
		ResetPosition (!isLeftText);
	}

	public void ResetPosition(bool _newDirectionIsLeft)
	{
		fadeInState = true;
		GetComponent<Image>().color = new Color(1.0f,1.0f, 1.0f, 0.0f);
		if (_newDirectionIsLeft) {
			transform.position = leftPosition;
			isLeftText = true;
		} else {
			transform.position = rightPosition;
			isLeftText = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Color newAlpha = GetComponent<Image> ().color;

		if(isLeftText)
			transform.Translate (20.0f * Vector3.right * Time.deltaTime);
		else
			transform.Translate (20.0f * Vector3.left * Time.deltaTime);

		if (newAlpha.a >= 1.0f) {
			fadeInState = false;
		} else if (newAlpha.a < 0.0f) {
			GetComponent<Image>().color = new Color(1.0f,1.0f, 1.0f, -0.1f);
			//ResetPosition(!isLeftText);
			gameObject.SetActive (false);
		}
		
		if (fadeInState) {
			newAlpha.a += (speed * Time.deltaTime);
			GetComponent<Image> ().color = newAlpha;
		} else {
			newAlpha.a -= (speed * Time.deltaTime);
			GetComponent<Image> ().color = newAlpha;
		}
	}
}
