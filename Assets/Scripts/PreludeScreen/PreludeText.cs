using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PreludeText : MonoBehaviour {

	public bool isLeftText = true;
	public bool fadeInState = true;
	private float speed = 0.3f;
	private float factor = 100.0f;

	public Vector3 leftPosition =  new Vector3 (100.0f, 150.0f, 0.0f);
	public Vector3 rightPosition = new Vector3 (800.0f, 150.0f, 0.0f);

	// Use this for initialization
	void Start () {
		transform.position = leftPosition;
		if (GameController.instance.isAndroidVersion) {
			factor  = 250.0f;
		}
	}

	public void PresetLeftAndRight(Vector3 _left, Vector3 _right)
	{
		leftPosition = _left;
		rightPosition = _right;
	}

	void OnEnable()
	{
		ResetPosition (!isLeftText);
	}

	public void ResetPosition(bool _newDirectionIsLeft)
	{
		fadeInState = true;
		GetComponent<Image>().color = new Color(1.0f,1.0f, 1.0f, 0.0f);
		int height = Random.Range (1, 6);

		if (_newDirectionIsLeft) {
			int left = Random.Range (1, 4);
			transform.position = new Vector3( left* (factor/2) , height * (factor/2), 0.0f);
			isLeftText = true;
		} else {
			int right = Random.Range (5, 8);
			transform.position = new Vector3(right * factor, height * (factor/2), 0.0f);
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
