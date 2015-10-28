using UnityEngine;
using System.Collections;

public class ModelRotate : MonoBehaviour
{
    public GameObject blackScreen;
    public GameObject plane;
    public GameObject spotLight;

    //Camera view variables.
    private int zoomPhase;
    public Camera cameraHandler;
    private float cyclingX;
    private float cyclingY;
    private float cyclingZ;
    private bool cyclingDir;
    private float cyclingZoom;
    private AudioSource audioF;
    private AudioSource flash;
    public static bool flashLight =false;

    public float[] angles = { 0, 20, 40, 60, 80 };
	int currAngle = 0;
	public float displayTime = 5.0f;
	public float blackOutTime = 0.05f;
	float timeLeft = 0;
	float blackScreenTimeLeft = 0;
	GameObject[] objArr;
    int currIndex = 0;
	// Use this for initialization
	void Start ()
    {
        timeLeft = displayTime;
		blackScreen.SetActive (false);

		Transform[] tArr = transform.GetComponentsInChildren<Transform> ();
		GameObject[] objTempArr = new GameObject[tArr.Length];
		int i = 0;
		foreach(Transform t in tArr){
			if(!t.gameObject.Equals(transform.gameObject)&& t.name.Contains("obj")){
				objTempArr[i] = t.gameObject;
				objTempArr[i].SetActive(false);
				i++;
			}
		}
		objArr = new GameObject[i];
		for (int j=0; j<i; j++) {
			objArr[j] = objTempArr[j];
		}

		SelectRandomObject ();

        //Camera view create event.
        zoomPhase = 0;
        cyclingX = Random.Range(0.0f, 360.0f);
        cyclingY = Random.Range(0.0f, 360.0f);
        cyclingZ = Random.Range(0.0f, 360.0f);
        cyclingZoom = Random.Range(0.5f, 1.0f);
        audioF = GetComponent<AudioSource>();
        flash = spotLight.GetComponent<AudioSource>();
    }

	void SelectRandomObject(){
        int prev = currIndex;
        objArr[currIndex].SetActive (false);
        while(currIndex== prev)
            currIndex = (int)Random.Range (0.0f, objArr.Length - 0.1f);
		objArr [currIndex].SetActive (true);
	}

    // Update is called once per frame
    void Update() {

        cyclingX += Random.Range(0.0f, 5.0f) * Time.deltaTime;
        cyclingY += Random.Range(0.0f, 5.0f) * Time.deltaTime;
        cyclingZ += Random.Range(0.0f, 5.0f) * Time.deltaTime;
        cyclingZoom = Random.Range(0.1f, 0.5f);

        if (cyclingDir) {
            if (cameraHandler.fieldOfView > 10) {
                cameraHandler.fieldOfView -= cyclingZoom;
                if(!audioF.isPlaying)
                {
                    audioF.Play();
                }
            }
        }
        else
        {
            if (cameraHandler.fieldOfView < 30) { 
                cameraHandler.fieldOfView += cyclingZoom;
                if (!audioF.isPlaying)
                {
                    audioF.Play();
                }
            }
        }

        cyclingX = cyclingX % 360;
        cyclingY = cyclingY % 360;
        cyclingZ = cyclingZ % 360;

        switch(zoomPhase)
        {
            case 0://Reset to normal view.
                cyclingDir = false;

                if (Random.Range(0.0f, 100.0f) < 0.2f)
                    zoomPhase = 1;
                break;
            case 1://Zoom in.
                cyclingDir = true;

                if (Random.Range(0.0f, 100.0f) < 0.2f)
                    zoomPhase = 2;
                break;
            case 2://Zoom in in.
                cyclingDir = true;

                if (Random.Range(0.0f, 100.0f) < 5.0f)
                    zoomPhase = 3;
                break;
            case 3://Zoom in out.
                cyclingDir = false;

                if (Random.Range(0.0f, 100.0f) < 5.0f)
                    zoomPhase = 2;

                if (Random.Range(0.0f, 100.0f) < 1.0f)
                    zoomPhase = 4;
                break;
            case 4:
                zoomPhase = 0;
                break;
        }
        

        Vector3 offset = new Vector3(Mathf.Cos(cyclingX)*0.25f, Mathf.Cos(cyclingY) * 0.25f + 1.0f, Mathf.Cos(cyclingZ) * 0.25f);
        cameraHandler.transform.LookAt(offset + new Vector3(-cameraHandler.fieldOfView/20.0f, 0.0f,0.0f),new Vector3(0.0f, 1.0f, 0.0f));

        if (blackScreenTimeLeft >= 0) {
			blackScreenTimeLeft -= Time.deltaTime;



            if (blackScreenTimeLeft <= 0)
            {
                blackScreen.SetActive(false);
                flashLight = false;
            }
            else
                return;
		}


		timeLeft -= Time.deltaTime;
		if (timeLeft <= 0)
		{
			int oldAngle = currAngle;

			while(oldAngle == currAngle)
				currAngle = Random.Range(0, angles.Length-1);


			transform.Rotate (Vector3.up *  -angles[oldAngle], Space.World);
			transform.Rotate (Vector3.up * angles[currAngle], Space.World);
            plane.transform.rotation = transform.rotation;
            SelectRandomObject();
			timeLeft = displayTime;
			blackScreenTimeLeft = blackOutTime;
			blackScreen.SetActive(true);
            cameraHandler.fieldOfView = 30.0f;
            flash.Play();
            flashLight = true;
        }
	}
}
