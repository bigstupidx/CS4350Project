using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackToTitleScene : MonoBehaviour {

	RectTransform myRect;
	Text myText;

	public GameObject reference;
	private float speed = 0.7f;
	private bool toggle = false;
	// Use this for initialization
	void Start () {
		myRect = transform.GetComponent<RectTransform> ();
		myText = transform.GetComponent<Text> ();
	}
	
	public void onClick(){
		if(transform.name.Equals ("BackToTitleScene")){
			Application.LoadLevel ("TitleScene");
		}
	}

	void Update()
	{
		Color temp = myText.color;
		if (toggle) {
			temp.a -= speed * Time.deltaTime;
		}
		else
			temp.a += speed * Time.deltaTime;

		myText.color = temp;

		if (myText.color.a <= 0.0f ||  myText.color.a >= 1.0f)
			toggle = !toggle;

		if (Input.GetKeyUp (KeyCode.Space) && reference.activeSelf == false) {
			reference.SetActive(true);
		}

	}

	
}
