using UnityEngine;
using System.Collections;

public class Displaytextbox : MonoBehaviour {

	bool isTrigger = false;
	public string colliderName = "";

	public GameObject[] textBoxReference = new GameObject[2];

	private BubbleBehaviour interactButton;
	private FeedTextFromObject feedText;
	private FadeInFadeOut textBox;

	private int currIndex = 0;

	// Automated flip text
	/*
	private float duration = 1.5f;
	private bool isAutomatedStart = false;
	private float startTime = 0.0f;
	*/

	// Use this for initialization
	void Start () {
		textBoxReference [0] = GameObject.Find ("TextBox_Android");
		textBoxReference [1] = GameObject.Find ("TextBox");

		if (GameController.instance.isAndroidVersion) {
			interactButton = GameObject.Find ("InteractionButton").GetComponent<BubbleBehaviour> ();
			textBox = textBoxReference[0].GetComponent<FadeInFadeOut>();
			textBoxReference[1].SetActive(false);
		} else {
			textBox = textBoxReference[1].GetComponent<FadeInFadeOut>();
			textBoxReference[0].SetActive(false);
		}

		feedText = GameObject.Find ("ObjectRespond").GetComponent<FeedTextFromObject> ();
	}

	void OnTriggerEnter(Collider other){
		isTrigger = true;
		colliderName = other.name;
		//Debug.Log ("Enter: " + colliderName);
	}
	
	void OnTriggerExit(Collider other){
		isTrigger = false;
		colliderName = "";
	}

	public void toggleRespond()
	{
		if (textBox.isActivated) {
			currIndex++;
			if(currIndex < feedText.multipleResponds.Length){
				feedText.UpdateText(currIndex);
			}
			else{
				textBox.TurnOnTextbox(true);
			}

//			if( feedText.getIsMoreThanOneLine() ){		// respond more than 1 line
//				feedText.UpdateText(currIndex);
//				if(feedText.ind == feedText.multipleResponds.Length)
//				{
//					textBox.TurnOnTextbox( true ); // fade out
//					//PlayerData.MoveFlag = true;	// ENABLE player move
//				}
//				else if(feedText.ind < feedText.multipleResponds.Length){
//					transform.GetComponent <PlayerMovement>().StopMoving ();
//					textBox.TurnOnTextbox( false ); // means do not fade out
//				}
//			}
//			else {		// respond ONLY have 1 line
//				if( !textBox.isFadingOn ){
//					textBox.TurnOnTextbox( true );
//					PlayerData.MoveFlag = true;	// ENABLE player move
//				}
//			}
		}
	}

	public void TriggerTextbox()
	{
		if (textBox.isActivated) {
			toggleRespond ();
		}else{
			// feedtext into textbox
			if (colliderName.Length < 1) { // player stand at out of nowhere
				if (EndingController.instance.isChapter2Activated)
					feedText.SetText ("Amari..");
				else {
					if (PlayerData.ParentGenderId == 1) // Male Parent
						feedText.SetText ("Papa?");
					else if (PlayerData.ParentGenderId == 2) // Female Parent
						feedText.SetText ("Mama?");
				}
			} else { //  player near to interactable object
				Item curr = GameController.instance.GetItem (colliderName);
				bool status = PlayerController.instance.AbleToTrigger (curr);
				if (EndingController.instance.isChapter2Activated && TraceController.instance.storyList.Count > 0) {
					status = TraceController.instance.storyList [0].Contains (colliderName);
				}

				string respond = curr.GetRespond (status, EndingController.instance.isChapter2Activated);
			
				if (respond.Length > 0) {		// not empty respond 
					feedText.SetText (respond, curr, status);
					transform.GetComponent <PlayerMovement> ().StopMoving ();//PlayerData.MoveFlag = false;	// disable player move
				}
			}
			// set currIndex = 0 and Feed text
			currIndex = 0;
			// turn on textbox
			textBox.TurnOnTextbox (false); // means do not fade out


//			if (feedText.ind == -1) {	// textbox is not fed yet
//			
//				if (colliderName.Length < 1) { // player stand at out of nowhere
//					if (EndingController.instance.isChapter2Activated)
//						feedText.SetText ("Amari..");
//					else {
//						if (PlayerData.ParentGenderId == 1) // Male Parent
//							feedText.SetText ("Papa?");
//						else if (PlayerData.ParentGenderId == 2) // Female Parent
//							feedText.SetText ("Mama?");
//					}
//					textBox.TurnOnTextbox (false); // means do not fade out
//				
//					// Automated flip text
//					/*
//					isAutomatedStart = true;
//					startTime = Time.time;
//					*/
//				} else { //  player near to interactable object
//					Item curr = GameController.instance.GetItem (colliderName);
//					bool status = PlayerController.instance.AbleToTrigger (curr);
//					if (EndingController.instance.isChapter2Activated && TraceController.instance.storyList.Count > 0) {
//						status = TraceController.instance.storyList [0].Contains (colliderName);
//					}
//				
//					string respond = curr.GetRespond (status, EndingController.instance.isChapter2Activated);
//				
//					if (respond.Length > 0) {		// not empty respond 
//						transform.GetComponent <PlayerMovement> ().StopMoving ();//PlayerData.MoveFlag = false;	// disable player move
//						feedText.SetText (respond, curr, status);
//						textBox.TurnOnTextbox (false); // means do not fade out
//					}
//				}
//			}
		}

	}

		
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Space) && !colliderName.Contains("ZoomPoint") ) {
			Debug.Log("Enter");
			TriggerTextbox();
		}

		if (GameController.instance.isAndroidVersion) {
			if (colliderName.Length > 0 && !colliderName.Contains("Transition_") ) {
				interactButton.canTrigger = true;
			} else
				interactButton.canTrigger = false;
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
