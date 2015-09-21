using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WheelMenuButton : MonoBehaviour {
	public static float posLeft = -700.0f;
	public static float posCenter = 0.0f;
	public static float posRight = 700.0f;
	static float totalMoveTimeInSec = 0.2f;
	static float halfScreenWidth = 1080;

	RectTransform myRect;
	Text myText;

	public WheelMenuButton leftFriend;
	public WheelMenuButton rightFriend;


	float moveTime = 0.0f;
	float dest;
	static bool isGoingLeft = false; // false == left, true == right

	// Use this for initialization
	void Start () {
		myRect = transform.GetComponent<RectTransform> ();
		myText = transform.GetComponent<Text> ();
	}

	public void onClick(){

		if (myRect.anchoredPosition.x == 0) {
			Debug.Log (transform.name + " EXECTUE");
			if(transform.name.Equals ("NewStory")){
				Application.LoadLevel("MainScene");
			}
			if(transform.name.Equals ("BackToTitleScene")){
				Application.LoadLevel ("TitleScene");
			}
		} else {

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
