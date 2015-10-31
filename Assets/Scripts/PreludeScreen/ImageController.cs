using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageController : MonoBehaviour {

	public GameObject reference;
	public GameObject gameTitle;
	public GameObject preludeCredit;
    public GameObject rollingText;
    public Camera cameraHandler;
	public GameObject skipOption;
	public GameObject touchDetector;
	

    public Texture2D[] partOneFrame;
	public Sprite[] partOneText;
    public Vector3[] cameraPos;
    public Vector3[] cameraLookAt;
    private int index = 0;
	private int touchDetectedCount = 0;

	public bool transitToNextScene = false;
	public bool fastForwardSelected = false;

	private float startTime;
    private float cyclingX;
    private float cyclingY;
    private float cyclingZ;

    // Use this for initialization
    void Start () {
		startTime = Time.time;
        cameraHandler.transform.position = cameraPos[0];
        cameraHandler.transform.LookAt(cameraLookAt[0]);
        preludeCredit.SetActive (true);

        cyclingX = Random.Range(0.0f, 360.0f);
        cyclingY = Random.Range(0.0f, 360.0f);
        cyclingZ = Random.Range(0.0f, 360.0f);
    }

	
	public void FastForward()
	{
		skipOption.SetActive (false);
		rollingText.SetActive (false);
		preludeCredit.SetActive (false);
		index = 5;
		reference.GetComponent<FadeToClear>().TransitToNextScene();
	}
	
	public void TurnOnSkipOption()
	{
		if (touchDetectedCount >= 2) {
			skipOption.SetActive (true);
			touchDetector.SetActive(false);
		} else {
			touchDetectedCount++;
		}
	}
	
	
	// Update is called once per frame
	void Update () {
        cyclingX += Random.Range(0.0f, 3.0f) * Time.deltaTime;
        cyclingY += Random.Range(0.0f, 3.0f) * Time.deltaTime;
        cyclingZ += Random.Range(0.0f, 3.0f) * Time.deltaTime;

        cyclingX = cyclingX % 360;
        cyclingY = cyclingY % 360;
        cyclingZ = cyclingY % 360;

        Vector3 offset = new Vector3(Mathf.Cos(cyclingX) * 0.025f, Mathf.Cos(cyclingY) * 0.05f, Mathf.Cos(cyclingZ) * 0.025f);
        cameraHandler.transform.LookAt(cameraLookAt[index]+ offset);


        float currTime = Time.time - startTime;

		if (Input.GetKeyUp (KeyCode.Space)) {
			TurnOnSkipOption();
		}
		
		
		if (currTime > 4.4f) {
			if(!rollingText.activeSelf){
				if(index < partOneText.Length ){
					rollingText.SetActive(true);
					rollingText.GetComponent<Image>().sprite = partOneText[index];

					preludeCredit.GetComponent<MeshRenderer>().enabled = true;
					preludeCredit.GetComponent<MeshRenderer>().material.mainTexture = partOneFrame[index];
                    cameraHandler.transform.position = cameraPos[index+1];
                    Debug.Log(index);

                    startTime = Time.time;
					index++;
				}
				else
				{
					gameTitle.SetActive(true);
					reference.GetComponent<FadeToClear>().TransitToNextScene();
				}
			}else
			{
				if (currTime > 6.5f) {
					preludeCredit.GetComponent<MeshRenderer>().enabled = false;
				}
			}
		}
	}
}
