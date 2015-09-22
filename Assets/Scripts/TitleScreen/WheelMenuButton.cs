using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WheelMenuButton : MonoBehaviour {
	public static float posLeft = -700.0f;
	public static float posCenter = 0.0f;
	public static float posRight = 700.0f;
	static float totalMoveTimeInSec = 0.2f;
	static float halfScreenWidth = 1080;
	static float blackFadeAlphaChg = 0.3f;

	RectTransform myRect;
	Text myText;

	public WheelMenuButton leftFriend;
	public WheelMenuButton rightFriend;
	public RawImage blackScreen;
	public int myID;

	float moveTime = 0.0f;
	float dest;
	static bool isGoingLeft = false; // false == left, true == right
	static int choice = -1;

	// Use this for initialization
	void Start () {
		myRect = transform.GetComponent<RectTransform> ();
		myText = transform.GetComponent<Text> ();
		blackScreen.enabled = false;
	}

	public void onClick(){
		if (choice != -1)
			return;

		if (myRect.anchoredPosition.x == 0) {

			// Set menu choice
			Debug.Log (transform.name + " EXECTUE");
			choice = myID;


			if(choice == 1){
				// set black screen to begin fading
				blackScreen.enabled = true;
				Color tempColor = blackScreen.color;
				tempColor.a = 0.0f;
				blackScreen.color = tempColor;
			}
			else{
				// So menu moves if choices arent implemnted yet
				choice = -1;
			}
		}
		
		else {

			if(myRect.anchoredPosition.x <= 0){
				isGoingLeft = false;
			}
			else{
				isGoingLeft = true;
			}

			leftFriend.StartMove (posLeft);
			this.StartMove (posCenter);
			rightFriend.StartMove (posRight);
		}

	}

	public void StartMove(float newDest){
		dest = newDest;
		moveTime = totalMoveTimeInSec;
	}

	// Update is called once per frame
	void Update () {
		if (blackScreen.enabled) {
			// Fade black in
			Color tempColor = blackScreen.color;
			tempColor.a = Mathf.Min(1.0f, tempColor.a + blackFadeAlphaChg*Time.deltaTime);
			blackScreen.color = tempColor;

			Debug.Log (blackScreen.color.a);

			// Change Scene if menu choice selected
			if(blackScreen.color.a >= 1.0f && choice != -1){
				if(choice == 1){
					Application.LoadLevel("PreludeScene");
				}
			}
		}

		if (moveTime > 0) {
			float currX = myRect.anchoredPosition.x;
			float lengthLeft = 0; 
			float moveAmt = 0;
			float halfMyWidth = myRect.rect.width/2;
			Debug.Log (myRect.name + ": " + dest + "   currX: " + currX);
			if(isGoingLeft){
				if(dest>currX){
					lengthLeft = -(currX+halfScreenWidth)-(halfScreenWidth-dest) - halfMyWidth;
				}
				else{
					lengthLeft = dest - currX;
				}

				moveAmt = Mathf.Max (lengthLeft, lengthLeft/moveTime*Time.deltaTime);


			}
			else{
				if(dest<currX){
					lengthLeft = (halfScreenWidth-currX) + (halfScreenWidth+dest)+ halfMyWidth;
				}
				else{
					lengthLeft = dest-currX;
				}

				moveAmt = Mathf.Min (lengthLeft, lengthLeft/moveTime*Time.deltaTime);
			}

			myRect.anchoredPosition = new Vector2(myRect.anchoredPosition.x+moveAmt, myRect.anchoredPosition.y);

			if( (myRect.anchoredPosition.x >= (halfScreenWidth + halfMyWidth)) && !isGoingLeft){ // out of screen right
				myRect.anchoredPosition = new Vector2(-halfScreenWidth - halfMyWidth, myRect.anchoredPosition.y); 
			}
			else if( (myRect.anchoredPosition.x <=  (-halfScreenWidth - halfMyWidth)) && isGoingLeft){// out of screen left
				myRect.anchoredPosition = new Vector2(halfScreenWidth + halfMyWidth, myRect.anchoredPosition.y); 
			}

			moveTime -= Time.deltaTime;
			
		}

		if (myRect.anchoredPosition.x == 0) {
			myText.fontStyle = FontStyle.Bold;
			myText.fontSize = 60;
		} else {
			myText.fontStyle = FontStyle.Normal;
			myText.fontSize = 50;
		}
	}
}
