using UnityEngine;
using System.Collections;

public class Displaytextbox : MonoBehaviour {

	bool isTrigger = false;
	public string colliderName = "";

	private FeedTextFromObject feedText;
	private FadeInFadeOut textBox;

	// Automated flip text
	/*
	private float duration = 1.5f;
	private bool isAutomatedStart = false;
	private float startTime = 0.0f;
	*/

	// Use this for initialization
	void Start () {
		feedText = GameObject.Find ("ObjectRespond").GetComponent<FeedTextFromObject> ();
		textBox = GameObject.Find ("TextBox").GetComponent<FadeInFadeOut> ();
	}

	void OnTriggerEnter(Collider other){
		isTrigger = true;
		colliderName = other.name;
		//Debug.Log ("Enter: " + colliderName);
	}
	
	void OnTriggerExit(Collider other){
		isTrigger = false;
		colliderName = "";
		//Debug.Log ("Exit: " + colliderName);
	}

		
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Space) && !colliderName.Contains("ZoomPoint") ) {

			if (feedText.ind == -1 ){	// textbox is not fed yet

				if (colliderName.Length < 1) { // player stand at out of nowhere
					if(PlayerData.ParentGenderId == 1) // Male Parent
						feedText.SetText ("Papa?");
					else if(PlayerData.ParentGenderId == 2) // Female Parent
						feedText.SetText ("Mama?");
					textBox.TurnOnTextbox( false ); // means do not fade out

					// Automated flip text
					/*
					isAutomatedStart = true;
					startTime = Time.time;
					*/
				}

				else //  player near to interactable object
				{
					Item curr = GameController.instance.GetItem (colliderName);
					bool status = PlayerController.instance.AbleToTrigger (curr);
					string respond = curr.GetRespond (status);

					if(respond.Length > 0){		// not empty respond 
						transform.GetComponent <PlayerMovement>().StopMoving ();//PlayerData.MoveFlag = false;	// disable player move
						feedText.SetText (respond, curr, status);
						textBox.TurnOnTextbox( false ); // means do not fade out
					}
				}
			}
			else{	// textbox fed already
				if( feedText.getIsMoreThanOneLine() ){		// respond more than 1 line
					feedText.ind++;
					if(feedText.ind == feedText.multipleResponds.Length)
					{
						textBox.TurnOnTextbox( true ); // fade out
						//PlayerData.MoveFlag = true;	// ENABLE player move
					}
					else if(feedText.ind < feedText.multipleResponds.Length){
						transform.GetComponent <PlayerMovement>().StopMoving ();
						textBox.TurnOnTextbox( false ); // means do not fade out
					}
				}
				else {		// respond ONLY have 1 line
					if( !textBox.isFadingOn ){
						textBox.TurnOnTextbox( true );
						PlayerData.MoveFlag = true;	// ENABLE player move
					}
				}
			}
		}

		// Automated flip text
		/*
		if (isAutomatedStart) {
			if( (Time.time - startTime) > duration) {
				if( feedText.getIsMoreThanOneLine() ){		// respond more than 1 line
					feedText.ind++;
					startTime = Time.time;
					feedText.UpdateText();
					if(feedText.ind == feedText.multipleResponds.Length)
					{
						textBox.TurnOnTextbox( true ); // fade out
						isAutomatedStart = false;
						//PlayerData.MoveFlag = true;	// ENABLE player move
					}
					else if(feedText.ind < feedText.multipleResponds.Length){
						transform.GetComponent <PlayerMovement>().StopMoving ();
						textBox.TurnOnTextbox( false ); // means do not fade out
					}
				}
				else {		// respond ONLY have 1 line
					if( !textBox.isFadingOn ){
						textBox.TurnOnTextbox( true );
						isAutomatedStart = false;
						PlayerData.MoveFlag = true;	// ENABLE player move
					}
				}
			}
		}
		*/
	}
}
