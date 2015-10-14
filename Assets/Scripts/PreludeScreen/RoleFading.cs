using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoleFading : MonoBehaviour {

	//public GameObject reference;
	public bool isStaticText = false;
	public string[] texts;
	private Text myText;
	private int curr = 0;
	private float startTime;

	// Use this for initialization
	void Start () {
		myText = transform.GetComponent<Text> ();

		startTime = Time.time;
		myText.text = texts [curr];
		ResetAlpha (false);
	}

	void ResetAlpha(bool _option){
		Color temp = myText.color;
		if (_option)
			temp.a = 1.0f;
		else
			temp.a = 0.0f;
		myText.color = temp;
	}

	void FadeIn(bool _option)
	{
		Color temp = myText.color;
		if (_option) 
			temp.a += ( 0.5f * Time.deltaTime );
		else
			temp.a -= ( 0.5f * Time.deltaTime );

		myText.color = temp;
	}

	void OnEnable()
	{
		startTime = Time.time;
	}

	// Update is called once per frame
	void Update () {
		if (!isStaticText) {
			transform.Translate (30.0f * Vector3.right * Time.deltaTime);

			if(transform.position.x > 500.0f){
				Vector3 newPos = transform.position;
				newPos.x = 0.0f;//-100.0f;
				transform.position = newPos;
				//reference.SetActive(true);
				this.ResetAlpha(false);
				//this.gameObject.SetActive(false);
			}
		}

		float timeDiff = Time.time - startTime; 
		if ((timeDiff) > 6.0f) {
			if(curr < texts.Length-1){
			curr++;
			}
			else
				curr = texts.Length-1;
			myText.text = texts [curr];
			startTime = Time.time;
		} else if ((timeDiff) > 4.0f) {
			FadeIn(false);
		} else if ((timeDiff) > 2.0f) {
			ResetAlpha(true);
		} else {
			FadeIn(true);
		}



		/*
		if ( (Time.time - startTime) > 1.5f) {
			curr++;

			if (curr >= texts.Length)
				this.gameObject.SetActive (false);
			else{
				myText.text = texts [curr];
				startTime = Time.time;
			}
		}
		*/

	}
}
