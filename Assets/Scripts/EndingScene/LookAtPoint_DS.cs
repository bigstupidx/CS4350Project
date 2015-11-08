using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LookAtPoint_DS : MonoBehaviour {
    public GameObject eyeTop;
    public GameObject eyeBottom;
    public GameObject lookAtLocations;
    
    public GameObject textHandler;

    static public int genderSet;
    private int currentState;
    private float stateTimer;
    private float eyeY;
    private float tEyeY;
    private float initialY1;
    private float initialY2;
    private int lookAtIndex;
    private Vector3 eyeTarget;
    private float fov;
    private Camera cam;
    private bool freeze;


    // Use this for initialization
    void Start () {
        genderSet = 0;
        currentState = 0;
        eyeY = 0.0f;
        initialY1 = eyeTop.transform.position.y;
        initialY2 = eyeBottom.transform.position.y;
        stateTimer = 10.0f;
        lookAtIndex = 0;
        eyeTarget = transform.position + new Vector3(0.0f, 0.0f, -1.0f);
        fov = 85.0f;
        cam = this.GetComponent<Camera>();
        freeze = false;

		// Reset Save file when game completed
		PlayerController.instance.Reset();
		PlayerController.instance.Save();

		EndingController.instance.Reset ();
		EndingController.instance.Save();
		
		Destroy (GameController.instance);
		Destroy (PlayerController.instance);
		Destroy (EndingController.instance);
		Destroy (TraceController.instance);
    }

    void stateRun()
    {
        Text h = textHandler.GetComponent<Text>();

        switch (currentState)
        {
            case 0: tEyeY = 50.0f; fov = 85.0f; h.text = ""; break;
            case 1: tEyeY = 0.0f; break;
            case 2: tEyeY = 100.0f; break;
            case 3: tEyeY = 0.0f; break;
            case 4: tEyeY = 500.0f; h.text = ""; break;
            case 5: lookAtIndex = 1; fov = 65.0f; h.text = ""; break;
            case 6: lookAtIndex = 2; fov = 65.0f; h.text = ""; break;
            case 7: lookAtIndex = 3; fov = 65.0f; h.text = ""; break;
            case 8: lookAtIndex = 4; fov = 50.0f; h.text = ""; break;
            case 9: lookAtIndex = 5; fov = 55.0f; h.text = ""; break;
            case 10: lookAtIndex = 6; fov = 28.0f; h.text = ""; break;
            case 11: lookAtIndex = 7; fov = 20.0f;break;
            case 12: lookAtIndex = 8; fov = 25.0f;break;
            case 13: lookAtIndex = 9; fov = 30.0f;break;
            case 14: lookAtIndex = 9; fov = 60.0f;break;
            case 15: tEyeY = 0.0f; fov = 100.0f; h.text = ""; break;

        }

        eyeY += (tEyeY - eyeY) * 1.0f * Time.deltaTime;
        cam.fieldOfView+= (fov - cam.fieldOfView) * 1.0f * Time.deltaTime;
        
        eyeTarget += (lookAtLocations.transform.GetChild(lookAtIndex).transform.position- eyeTarget)*2.0f*Time.deltaTime;
        transform.LookAt(eyeTarget);

        if (stateTimer > 0.0f)
        {
            if(!freeze)
                stateTimer -= 10 * Time.deltaTime;
        }
        else
        {
            currentState++;
            switch (currentState)
            {
                case 0: stateTimer = 10.0f; break;
                case 1: stateTimer = 10.0f; break;
                case 2: stateTimer = 10.0f; break;
                case 3: stateTimer = 10.0f; break;
                case 4: stateTimer = 20.0f; break;
                case 5: stateTimer = 10.0f; break;
                case 6: stateTimer = 15.0f; break;
                case 7: stateTimer = 30.0f; break;
                case 8: stateTimer = 10.0f; break;
                case 9: stateTimer = 5.0f; break;
				case 10: stateTimer = 5.0f; break;
                case 11: stateTimer = 40.0f; break;
                case 12: stateTimer = 30.0f; break;
                case 13: stateTimer = 20.0f; break;
                case 14: stateTimer = 40.0f; break;
                case 15: stateTimer = 80.0f; break;
				case 16: GameController.instance.SetLastLoadedScene(Application.loadedLevelName); Application.LoadLevel("CreditScene");break;
                default: stateTimer = 100000.0f; break;

            }
        }

        Debug.Log(currentState +" "+ stateTimer);

    }

    public void unfreeze()
    {
        freeze = false;
        //Debug.Log("Clicked");
    }
	
	// Update is called once per frame
	void Update () {
        stateRun();

        eyeTop.transform.position = new Vector3(eyeTop.transform.position.x, initialY1+eyeY, eyeTop.transform.position.z);
        eyeBottom.transform.position = new Vector3(eyeBottom.transform.position.x, initialY2 - eyeY, eyeBottom.transform.position.z);
        
    }
}
