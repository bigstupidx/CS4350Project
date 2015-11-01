using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PreludeText : MonoBehaviour {

	public bool isMoving = true;
	public bool isLeftText = true;
	public bool fadeInState = true;
	private float speed = 0.3f;
	private float wFactor;
	private float hFactor;

	// Use this for initialization
	void Start () {
		wFactor = Screen.width / 10;
		hFactor = Screen.height / 6;
		ResetPosition (true);
	}

	void OnEnable()
	{
		ResetPosition (!isLeftText);
	}

	public void ResetPosition(bool _newDirectionIsLeft)
	{
		fadeInState = true;

		if (isMoving) {
			GetComponent<Image>().color = new Color(1.0f,1.0f, 1.0f, 0.0f);
			int height = Random.Range (1, 5);

			if (_newDirectionIsLeft) {
				int left = Random.Range (2, 6);
				transform.position = new Vector3 (left * wFactor, height * hFactor, 0.0f);
				isLeftText = true;
			} else {
				int right = Random.Range (5, 7);
				transform.position = new Vector3 (right * wFactor, height * hFactor, 0.0f);
				isLeftText = false;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		Color newAlpha;

		if (isMoving) {
			newAlpha = GetComponent<Image> ().color;

			if (isLeftText)
				transform.Translate (20.0f * Vector3.right * Time.deltaTime);
			else
				transform.Translate (20.0f * Vector3.left * Time.deltaTime);
		} else
			newAlpha = GetComponent<Text> ().color;

		if (newAlpha.a >= 1.0f) {
			fadeInState = false;
		} else if (newAlpha.a < 0.0f) {
			if(isMoving){
				GetComponent<Image>().color = new Color(1.0f,1.0f, 1.0f, -0.1f);
				gameObject.SetActive (false);
			}
			else
				fadeInState = true;
		}
		
		if (fadeInState) {
			newAlpha.a += (speed * Time.deltaTime);
		} else {
			newAlpha.a -= (speed * Time.deltaTime);
		}

		if(isMoving)
			GetComponent<Image> ().color = newAlpha;
		else
			GetComponent<Text> ().color = newAlpha;
	}
}
