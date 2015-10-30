using UnityEngine;
using System.Collections;

public class LookAtPoint : MonoBehaviour {
    public GameObject eyeTop;
    public GameObject eyeBottom;
    public GameObject lookAtLocations;

	public GameObject photoMama;
	public GameObject photoPapa;

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
        

        switch (currentState)
        {
            case 0: tEyeY = 50.0f; fov = 85.0f; break;
            case 1: tEyeY = 0.0f; break;
            case 2: tEyeY = 100.0f; break;
            case 3: tEyeY = 0.0f; break;
            case 4: tEyeY = 500.0f; break;
            case 5: lookAtIndex = 1; fov = 65.0f; break;
            case 6: lookAtIndex = 2; fov = 65.0f; break;
            case 7: lookAtIndex = 3; fov = 40.0f; break;
            case 8: lookAtIndex = 4; fov = 20.0f; break;
            case 9: lookAtIndex = 5; fov = 25.0f; break;
            case 10: lookAtIndex = 6; fov = 28.0f; break;
            case 11: lookAtIndex = 7; fov = 32.0f; break;
            case 12: lookAtIndex = 8; fov = 40.0f; break;
            case 13: lookAtIndex = 9; fov = 50.0f; break;
            case 14: lookAtIndex = 9; fov = 20.0f; break;
            case 15: tEyeY = 0.0f; fov = 25.0f; break;

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
                case 4: stateTimer = 10.0f; break;
                case 5: stateTimer = 10.0f; break;
                case 6: stateTimer = 15.0f; break;
                case 7: stateTimer = 30.0f; break;
                case 8: stateTimer = 50.0f; break;
                case 9: stateTimer = 50.0f; break;
				case 10: stateTimer = 20.0f; freeze = true; photoMama.SetActive(true); photoPapa.SetActive(true); break;
                case 11: stateTimer = 50.0f; break;
                case 12: stateTimer = 50.0f; break;
                case 13: stateTimer = 20.0f; break;
                case 14: stateTimer = 50.0f; break;
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
