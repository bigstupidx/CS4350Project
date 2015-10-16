using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageBehaviour : MonoBehaviour {

	public bool fadeInState = true;
	public bool isSelectionFrame = false;

	public bool isMoving = false;
	public bool isMoveLeft = false;
	public bool isZoomIn = false;
	public float moveSpeed = 5.0f;
	private float speed = 0.3f;
	private ImageController reference;

	public bool selectionEnabled = false;
	public Vector3 initialPosition;
	public Vector3 initialScale = new Vector3 (1.1f, 1.1f, 1.0f);

	void Start()
	{
		reference = GameObject.Find ("ImageController").GetComponent<ImageController> ();
		//transform.position = initialPosition;
	}

	void OnEnable () {

		fadeInState = true;
		transform.position = initialPosition;
		transform.localScale = initialScale;

		if (isMoving) {
			int dir = Random.Range (1, 999);
			int zoom = Random.Range (1, 999);

			if(transform.name.CompareTo("CutsceneImage_Centre") != 0){
				isMoveLeft = false;
				isZoomIn = false;
				if(dir%2 == 0)
					isMoveLeft = true;
				if(zoom%2 == 0)
					isZoomIn = true;
			}
			else{
				isMoveLeft = !isMoveLeft;

				if(isMoveLeft)
					transform.position = new Vector3( 200.0f, initialPosition.y, initialPosition.z);
				else
					transform.position = new Vector3( 700.0f, initialPosition.y, initialPosition.z);
			}

		}
		GetComponent<Image> ().color = new Color(1.0f,1.0f,1.0f, 0.01f);

		if (isSelectionFrame) {
			selectionEnabled = true;
		}
	}

	public void SetParentID ()
	{
		if (selectionEnabled) {
			if(gameObject.name.Contains("Left") )
			   PlayerData.ParentGenderId = 1;
			else
			   PlayerData.ParentGenderId = 2;

			selectionEnabled = false;
			reference.hasSelectionMade = true;
		}
	}

	public void SelectionHover ()
	{
		if (selectionEnabled) {
			GetComponent<Image>().color = Color.white;
			if(gameObject.name.Contains("Left") ){
				GameObject.Find("CutsceneImage_Right").GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.51f);
			}
			else
				GameObject.Find("CutsceneImage_Left").GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.51f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Color newAlpha = GetComponent<Image> ().color;

		if (isSelectionFrame) {
			// fade in till max
			if (newAlpha.a < 0.5f){// && !selectionEnabled) {
				newAlpha.a += (speed * Time.deltaTime);
				GetComponent<Image> ().color = newAlpha;
			} else {  // hit max already
				//selectionEnabled = true;

				if(reference.hasSelectionMade){
					GetComponent<Image> ().color = new Color(1.0f,1.0f,1.0f, 0.5f);
					isSelectionFrame = false;
					fadeInState = false;
				}
			}

		} else {

			if( isMoving ){
				if(transform.name.CompareTo("CutsceneImage_Centre") != 0){
					if( isZoomIn ){
						Vector3 newScale = transform.localScale;
						newScale.x += (moveSpeed*0.0005f) * Time.deltaTime;
						newScale.y += (moveSpeed*0.0005f) * Time.deltaTime;
						transform.localScale = newScale;
					}else{
						Vector3 newScale = transform.localScale;
						newScale.x -= (moveSpeed*0.0005f) * Time.deltaTime;
						newScale.y -= (moveSpeed*0.0005f) * Time.deltaTime;
						transform.localScale = newScale;
					}
				}

				if( isMoveLeft )
					transform.Translate (moveSpeed * Vector3.right * Time.deltaTime);
				else
					transform.Translate (moveSpeed * Vector3.left * Time.deltaTime);
			}


			if (newAlpha.a >= 1.5f) {
				fadeInState = false;
			} else if (newAlpha.a < 0.0f) {
				if(reference.hasSelectionMade && reference.transitToNextScene){
					reference.LoadNextLevel();
				}
				GetComponent<Image>().color = new Color(1.0f,1.0f, 1.0f, -1.0f);
				gameObject.SetActive (false);
			}

			if (fadeInState) {
				newAlpha.a += (speed * Time.deltaTime);
				GetComponent<Image> ().color = newAlpha;
			} else {
				newAlpha.a -= (speed * Time.deltaTime);
				GetComponent<Image> ().color = newAlpha;
			}
		} //  end of NOT selection frame
	}
}
