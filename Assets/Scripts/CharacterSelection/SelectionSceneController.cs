using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectionSceneController : MonoBehaviour {

    public Transform teacher;
    public Transform parent;
    public AmariMovement m1;
	public AmariMovement m2;
	public AmariMovement m3;
	public AmariMovement f4;
	public AmariMovement f5;
	public AmariMovement f6;
    public RawImage blackScreenImage;

     FeedTextFromObject feedText;
    FadeInFadeOut textBox;

    float timer = 0.0f;
	int stage = 0;
	float boxSpeedPerSec = 100.0f;
    public float alphaChgPerSec = 0.5f;
    int m1pt, m2pt, m3pt, f4pt, f5pt, f6pt;
    bool isReadyToProceed = false;
    bool isFedText = false;

    Vector3[] movePoints = { new Vector3(0.0f, 0.5f, 1.5f), new Vector3(-2.0f, 0.5f, 1.5f), new Vector3(-2.0f, 0.5f, -0.5f), new Vector3(0.0f, 0.5f, -0.5f), new Vector3(2.0f, 0.5f, -0.5f), new Vector3(2.0f, 0.5f, 1.5f) };
    AmariMovement selected;
	
	// Automated flip text
	public bool isAutomatedStart = false;
	private float duration = 2.5f;
	private float startTime = 0.0f;
	private int currIndex = 0;

	// Use this for initialization
	void Start () {
        feedText = GameObject.Find("ObjectRespond").GetComponent<FeedTextFromObject>();
        textBox = GameObject.Find("TextBox").GetComponent<FadeInFadeOut>();

		if (GameController.instance.isAndroidVersion)
			isAutomatedStart = true;

        AmariMovement.speed = 3.5f;
        m1pt = 1;
        m2pt = 2;
        m3pt = 3;
        f4pt = 0;
        f5pt = 4;
        f6pt = 5;

        blackScreenImage.color = Color.black;

    }

	public bool ToggleTextRespond()
	{
		Debug.Log ("Enter");
		currIndex++;
		feedText.UpdateText(currIndex);
		
		if (currIndex == feedText.multipleResponds.Length)
		{
			textBox.TurnOnTextbox(true);
			isReadyToProceed = true;
			return false;
		}
		return true;
	}

    // Update is called once per frame
    void FixedUpdate() {

        if (stage < 4)
        {
            // Black screen Fade Out
            Color tempColor = blackScreenImage.color;
            tempColor.a = Mathf.Max(0.0f, tempColor.a - alphaChgPerSec * Time.deltaTime);
            blackScreenImage.color = tempColor;
        }

        if (stage >= 2)
        {
            if (!textBox.isFadingOn)
            {
                feedText.enabled = false;
            }
        }
        if (stage == 0) {

            if (!isFedText)
            {
                feedText.SetText("Are you here to bring Amari to you spouse's place?\\Yes, it's getting late. We should really be going.\\Alright then...");
                textBox.TurnOnTextbox(false);
                isFedText = true;

				if(isAutomatedStart)
					startTime = Time.time;
            }

			if(isAutomatedStart && feedText.getIsMoreThanOneLine() )
			{
				if( (Time.time - startTime) > duration) {
					if(ToggleTextRespond())
						startTime = Time.time;
				}
			}

            if (Input.GetKeyUp(KeyCode.Space) && stage == 0 && feedText.getIsMoreThanOneLine())
            {
				if(currIndex < feedText.multipleResponds.Length)
					ToggleTextRespond();

				/*
                feedText.ind++;
                if (feedText.ind == feedText.multipleResponds.Length)
                {
                    textBox.TurnOnTextbox(true);
                    isReadyToProceed = true;

                }
                else if (feedText.ind < feedText.multipleResponds.Length) {
                    textBox.TurnOnTextbox(false);
                }*/

            }

            if (isReadyToProceed)
            {
                feedText.SetText("...Children, get into your lines!");
                textBox.TurnOnTextbox(false);

				if(isAutomatedStart)
					startTime = Time.time;

                m1.FaceFront();
                m2.FaceFront();
                m3.FaceFront();
                f4.FaceFront();
                f5.FaceFront();
                f6.FaceFront();

                m1.StopMoving();
                m2.StopMoving();
                m3.StopMoving();
                f4.StopMoving();
                f5.StopMoving();
                f6.StopMoving();

                m1.transform.GetComponentInChildren<AmariEyes>().LookAtObject(teacher.gameObject);
                m2.transform.GetComponentInChildren<AmariEyes>().LookAtObject(teacher.gameObject);
                m3.transform.GetComponentInChildren<AmariEyes>().LookAtObject(teacher.gameObject);
                f4.transform.GetComponentInChildren<AmariEyes>().LookAtObject(teacher.gameObject);
                f5.transform.GetComponentInChildren<AmariEyes>().LookAtObject(teacher.gameObject);
                f6.transform.GetComponentInChildren<AmariEyes>().LookAtObject(teacher.gameObject);
                stage = 1;
                timer = 0.0f;

            }
            else
            {
                
                if (!m1.IsMoving())
                {

                    m1pt = (m1pt + 1) % movePoints.Length;
                    m1.MoveTo(movePoints[m1pt]);
                }

                if (!m2.IsMoving())
                {
                    m2pt = (m2pt + 1) % movePoints.Length;
                    m2.MoveTo(movePoints[m2pt]);
                }

                if (!m3.IsMoving())
                {
                    m3pt = (m3pt + 1) % movePoints.Length;
                    m3.MoveTo(movePoints[m3pt]);
                }

                if (!f4.IsMoving())
                {
                    f4pt = (f4pt + 1) % movePoints.Length;
                    f4.MoveTo(movePoints[f4pt]);
                }


                if (!f5.IsMoving())
                {
                    f5pt = (f5pt + 1) % movePoints.Length;
                    f5.MoveTo(movePoints[f5pt]);
                }

                if (!f6.IsMoving())
                {
                    f6pt = (f6pt + 1) % movePoints.Length;
                    f6.MoveTo(movePoints[f6pt]);
                }

            }


        }
        else if(stage == 1) {
            //timer += Time.deltaTime;
			bool canProceed = false;
			if (isAutomatedStart) {
				if( (Time.time - startTime) > duration) {
					isAutomatedStart = false;
					canProceed = true;
				}
			}

			if (Input.GetKeyUp(KeyCode.Space) || canProceed)
			{

                textBox.TurnOnTextbox(true);
                AmariMovement.speed = 3.0f;
                stage = 2;

                // stage 1 init
                m1.MoveTo(new Vector3(0.1f, 0.5f, 0.7f));
                m2.MoveTo(new Vector3(-1.4f, 0.5f, -1.0f));
                m3.MoveTo(new Vector3(0.6f, 0.5f, -1.0f));

                f4.MoveTo(new Vector3(-0.9f, 0.5f, 0.7f));
                f5.MoveTo(new Vector3(1.1f, 0.5f, 0.7f));
                f6.MoveTo(new Vector3(-0.4f, 0.5f, -1.0f));

                m1.transform.GetComponentInChildren<AmariEyes>().LookAtObject(null);
                m2.transform.GetComponentInChildren<AmariEyes>().LookAtObject(null);
                m3.transform.GetComponentInChildren<AmariEyes>().LookAtObject(null);
                f4.transform.GetComponentInChildren<AmariEyes>().LookAtObject(null);
                f5.transform.GetComponentInChildren<AmariEyes>().LookAtObject(null);
                f6.transform.GetComponentInChildren<AmariEyes>().LookAtObject(null);

                

            }
        } else if (stage == 2) {

            
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
				stage = 3;
				// Stage 2 initialisation here
				AmariSelection.selectionEnabled = true;
                m1.FaceFront();
                m2.FaceFront();
                m3.FaceFront();
                f4.FaceFront();
                f5.FaceFront();
                f6.FaceFront();
            }

        } else if (stage == 3) {
            m1.FaceFront();
            m2.FaceFront();
            m3.FaceFront();
            f4.FaceFront();
            f5.FaceFront();
            f6.FaceFront();


			if (AmariSelection.selectionDone) {
				AmariSelection.selectionEnabled = false;
                

                // Find selected amari
                if (PlayerData.HairId == 1)
                    selected = m1;
                else if (PlayerData.HairId == 2)
                    selected = m2;
                else if (PlayerData.HairId == 3)
                    selected = m3;
                else if (PlayerData.HairId == 4)
                    selected = f4;
                else if (PlayerData.HairId == 5)
                    selected = f5;
                else if (PlayerData.HairId == 6)
                    selected = f6;
             

                AmariMovement.speed = 2.0f;
                Vector3 newDest = selected.transform.position;
                newDest.z = 1.5f;
                selected.MoveTo(newDest);
                stage = 4;
            }
		} else if (stage == 4) {


            Color tempColor = blackScreenImage.color;
            tempColor.a = Mathf.Min(1.0f, tempColor.a + alphaChgPerSec * Time.deltaTime);
            blackScreenImage.color = tempColor;

            if (!selected.IsMoving())
            {
                Vector3 newDest = parent.transform.position;
                newDest.y = -0.5f;
                selected.MoveTo(newDest);
            }

            if(tempColor.a >= 1.0f){
                AmariSelection.selectionDone = false;
                GameController.instance.SetStartTime();
                Application.LoadLevel("PlatformGameScene");
				//Application.LoadLevel("GroundIndoorGameScene");
			}

        }
    }
}
