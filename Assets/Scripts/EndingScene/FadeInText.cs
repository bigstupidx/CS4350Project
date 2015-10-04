using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInText : MonoBehaviour {

	private Text myText;
	private float speed = 0.2f;
	public GameObject reference;
	// Use this for initialization
	void Start () {
		myText = transform.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (myText.color.a < 1.0f) {
			Color temp = myText.color;
			temp.a += speed * Time.deltaTime;
			myText.color = temp;
		} else
			reference.SetActive (true);
	}
}
