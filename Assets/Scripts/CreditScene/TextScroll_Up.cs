using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextScroll_Up : MonoBehaviour {
	public GameObject logo;
	public GameObject header;
	public GameObject text;
	public GameObject end;
	public GameObject canvas;
    public float sceneSpeed;

    private Color logo_temp;
	private Color header_temp;
	private Color text_temp;
	private Color end_temp;

	private float stateTimer;
    private float contentTextVelocity;
	private int currentState;

	// Use this for initialization
	void Start () {
		text_temp = text.GetComponent<Image> ().color;
		logo_temp = logo.GetComponent<Image>().color;
		header_temp = header.GetComponent<Image> ().color;
		end_temp = end.GetComponent<Image> ().color;

		header_temp.a = 0.0f;
		logo_temp.a = 0.0f;
		text_temp.a = 0.0f;
		end_temp.a = 0.0f;

		text.GetComponent<Image> ().color = text_temp;
		header.GetComponent<Image> ().color = header_temp;
		logo.GetComponent<Image> ().color = logo_temp;
		end.GetComponent<Image> ().color = end_temp;

		currentState = 0;
		stateTimer = 20.0f;
        contentTextVelocity = 0.0f;
    }

	void run()
	{
		switch (currentState) {
		case 0: logo_temp.a += 1.0f * Time.deltaTime; logo.GetComponent<Image>().color = logo_temp; break;
		case 1: header_temp.a += 1.0f * Time.deltaTime; header.GetComponent<Image> ().color = header_temp; break;
		case 2: text_temp.a += 1.0f * Time.deltaTime; text.GetComponent<Image> ().color = text_temp; end_temp.a += 1.0f * Time.deltaTime; end.GetComponent<Image> ().color = end_temp;break;
		case 3: up(); break;
		case 4: text_temp.a -= 1.0f * Time.deltaTime; text.GetComponent<Image> ().color = text_temp; break;
		case 5: end_temp.a -= 1.0f * Time.deltaTime; end.GetComponent<Image> ().color = end_temp; break;
		case 6: Application.LoadLevel("TitleScene"); break;
		}
		if (stateTimer > 0.0f) {
				stateTimer -= 10 * Time.deltaTime;
		} else {
			currentState++;
			switch (currentState) {
			case 0: stateTimer = 20.0f; break;
			case 1: stateTimer = 20.0f; break;
			case 2: stateTimer = 20.0f; break;
			case 3: stateTimer = 1000.0f; break;
			case 4: stateTimer = 60.0f; break;
			case 5: stateTimer = 60.0f; break;
			case 6: stateTimer = 10.0f; break;
			default: stateTimer = 100000.0f; break;
			}
		}
		Debug.Log (currentState + " " + stateTimer);
	}

	void up() {
        if (contentTextVelocity < sceneSpeed)
        {
            contentTextVelocity += 0.01f;
            contentTextVelocity *= 1.05f;
        }

        float temp =  (float)canvas.GetComponent<RectTransform> ().rect.height/ 1080.0f;
		Debug.Log (temp);
		text.transform.Translate (Vector3.up * contentTextVelocity * temp * Time.deltaTime);
		logo.transform.Translate (Vector3.up * contentTextVelocity * temp * Time.deltaTime);
		header.transform.Translate (Vector3.up * contentTextVelocity * temp * Time.deltaTime);
		end.transform.Translate (Vector3.up * contentTextVelocity * temp * Time.deltaTime);
		if (end.transform.position.y >= Screen.height/2) {
			currentState++;
			stateTimer = 60.0f;
		}
	}

	// Update is called once per frame
	void Update () {
		run ();
	}
}
