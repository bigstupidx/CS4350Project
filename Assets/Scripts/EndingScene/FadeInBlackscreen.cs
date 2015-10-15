using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInBlackscreen : MonoBehaviour {
	
	private Image myText;
	private float speed = 0.5f;
	// Use this for initialization
	void Start () {
		myText = transform.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (myText.color.a < 1.0f) {
			Color temp = myText.color;
			temp.a += speed * Time.deltaTime;
			myText.color = temp;
		} else {
			TraceController.instance.Init();

			Destroy(GameController.instance);
			Destroy(PlayerController.instance);
			Destroy(EndingController.instance);

			Application.LoadLevel("TitleScene");

		}
	}
}
