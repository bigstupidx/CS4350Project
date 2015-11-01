using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LookAtPoint : MonoBehaviour {
    public GameObject eyeTop;
    public GameObject eyeBottom;
    public GameObject lookAtLocations;

	public GameObject photoMama;
    public GameObject photoPapa;
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
            case 4: tEyeY = 500.0f; h.text = "Amari!"; break;
            case 5: lookAtIndex = 1; fov = 65.0f; h.text = ""; break;
            case 6: lookAtIndex = 2; fov = 65.0f; h.text = ""; break;
            case 7: lookAtIndex = 3; fov = 40.0f; h.text = "I just hope that you are safe."; break;
            case 8: lookAtIndex = 4; fov = 20.0f; h.text = "It has always been my dream to have my own child."; break;
            case 9: lookAtIndex = 5; fov = 25.0f; h.text = "You always bring joy to our lives."; break;
            case 10: lookAtIndex = 6; fov = 28.0f; h.text = "Everyday, I look forward to holding you in my arms. (Choose a photo)"; break;
            case 11: lookAtIndex = 7; fov = 32.0f;
                if (genderSet == 1)
                    h.text = "I remember the first word from your mouth was 'Papa'. Love is all it takes to keep the family together.";
                if (genderSet == 2)
                    h.text = "I remember the first word from your mouth was 'Mama'. Love is all it takes to keep the family together.";

                    break;
            case 12: lookAtIndex = 8; fov = 40.0f;
                if (genderSet == 1)
                    h.text = "But I couldn't control myself, your mom warned me.";
                if (genderSet == 2)
                    h.text = "But I couldn't control myself, your dad warned me.";
                break;
            case 13: lookAtIndex = 9; fov = 50.0f;
                h.text = "I am so sorry Amari, I really am. I promise I will get myself better.";
                break;
            case 14: lookAtIndex = 9; fov = 20.0f;
                if (genderSet == 1)
                    h.text = "But the only way to give you a proper childhood was to leave. Your mom will be able to take care of you, Amari.";
                if (genderSet == 2)
                    h.text = "But the only way to give you a proper childhood was to leave. Your dad will be able to take care of you, Amari.";
                break;
            case 15: tEyeY = 0.0f; fov = 25.0f; h.text = ""; break;

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
                case 8: stateTimer = 50.0f; break;
                case 9: stateTimer = 50.0f; break;
				case 10: stateTimer = 2.0f; freeze = true; photoMama.SetActive(true); photoPapa.SetActive(true); break;
                case 11: stateTimer = 60.0f; break;
                case 12: stateTimer = 50.0f; break;
                case 13: stateTimer = 60.0f; break;
                case 14: stateTimer = 55.0f; break;
                case 15: stateTimer = 50.0f; break;
				case 16: GameController.instance.SetLastLoadedScene(Application.loadedLevelName); Application.LoadLevel("TransitionScene");break;
                default: stateTimer = 100000.0f; break;

            }
        }

        Debug.Log(currentState +" "+ stateTimer);

    }

    public void unfreeze()
    {
        freeze = false;
		photoMama.SetActive(false); photoPapa.SetActive(false);
        Debug.Log("Clicked");
    }
	
	// Update is called once per frame
	void Update () {
        stateRun();

        eyeTop.transform.position = new Vector3(eyeTop.transform.position.x, initialY1+eyeY, eyeTop.transform.position.z);
        eyeBottom.transform.position = new Vector3(eyeBottom.transform.position.x, initialY2 - eyeY, eyeBottom.transform.position.z);
        
    }
}
