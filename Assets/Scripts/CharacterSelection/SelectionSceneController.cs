using UnityEngine;
using System.Collections;

public class SelectionSceneController : MonoBehaviour {

	public AmariMovement m1;
	public AmariMovement m2;
	public AmariMovement m3;
	public AmariMovement f4;
	public AmariMovement f5;
	public AmariMovement f6;
	public RectTransform questionBox;
	float timer = 0.0f;
	int stage = 0;
	float boxSpeedPerSec = 200.0f;

	AmariMovement selected;

	// Use this for initialization
	void Start () {
		m1.MoveTo (new Vector3 (-2.6f, 0.5f, 3.0f));
	}
	
	// Update is called once per frame
	void Update () {
		if (stage == 0) {
			stage = 1;

			// stage 1 init
			m1.MoveTo (new Vector3 (-2.6f, 0.5f, 0.0f));
			m2.MoveTo (new Vector3 (-1.7f, 0.5f, 0.0f));
			m3.MoveTo (new Vector3 (-0.8f, 0.5f, 0.0f));
			
			f4.MoveTo (new Vector3 (0.1f, 0.5f, -0.2f));
			f5.MoveTo (new Vector3 (1.0f, 0.5f, -0.2f));
			f6.MoveTo (new Vector3 (1.9f, 0.5f, -0.2f));
		} else if (stage == 1) {



            // Move to one line
            if (!m1.IsMoving())
                m1.FaceFront();
            if (!m2.IsMoving())
                m2.FaceFront();
            if (!m3.IsMoving())
                m3.FaceFront();
            if (!f4.IsMoving())
                f4.FaceFront();
            if (!f5.IsMoving())
                f5.FaceFront();
            if (!f6.IsMoving())
                f6.FaceFront();

            if (!m1.IsMoving () && !m2.IsMoving () && !m3.IsMoving () && !f4.IsMoving () && !f5.IsMoving () && !f6.IsMoving ()) {
				stage = 2;
				// Stage 2 initialisation here
				AmariSelection.selectionEnabled = true;
                m1.FaceFront();
                m2.FaceFront();
                m3.FaceFront();
                f4.FaceFront();
                f5.FaceFront();
                f6.FaceFront();
            }

        } else if (stage == 2) {

            

            if (questionBox.anchoredPosition.y > 0) {
				questionBox.anchoredPosition = new Vector2 (questionBox.anchoredPosition.x, questionBox.anchoredPosition.y - (boxSpeedPerSec * Time.deltaTime));
			}

			if (AmariSelection.selectionDone) {
				AmariSelection.selectionEnabled = false;
				Application.LoadLevel("PlatformGameScene");
				
                // Find selected amari
			}
		} else if (stage == 3) {
            // move selected amari to parent
		}
	}
}
