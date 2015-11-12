using UnityEngine;
using System.Collections;

public class Displaytextbox : MonoBehaviour {

	bool isTrigger = false;
	public string colliderName = "";

	public GameObject[] textBoxReference = new GameObject[2];
	public bool canTextBoxDisplay = false;

	private BubbleBehaviour interactButton;
	private FeedTextFromObject feedText;
	private FadeInFadeOut textBox;
    private PlayerSound playerSound;

	private int currIndex = 0;
	

	void Start () {
		textBoxReference [0] = GameObject.Find ("TextBox_Android");
		textBoxReference [1] = GameObject.Find ("TextBox");

        playerSound = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSound>();

		if (GameController.instance.isAndroidVersion) {
			interactButton = GameObject.Find ("InteractionButton").GetComponent<BubbleBehaviour> ();
			textBox = textBoxReference[0].GetComponent<FadeInFadeOut>();
			textBoxReference[1].SetActive(false);
		} else {
			textBox = textBoxReference[1].GetComponent<FadeInFadeOut>();
			textBoxReference[0].SetActive(false);
		}

		feedText = textBox.transform.GetChild(0).GetComponent<FeedTextFromObject> ();
	}

	void OnTriggerEnter(Collider other){
		isTrigger = true;
		colliderName = other.name;
	}
	
	void OnTriggerExit(Collider other){
		isTrigger = false;
		colliderName = "";
	}

	public void toggleRespond()
	{
		if (textBox.isActivated) {
			if(GameController.instance.isAndroidVersion){
				interactButton.TurnOffButton();
			}
			currIndex++;

			if(currIndex < feedText.multipleResponds.Length){
				feedText.UpdateText(currIndex);
			}
			else{
				if(!textBox.isFadingOn){
					textBox.TurnOnTextbox(true);
					if(GameController.instance.isAndroidVersion){
						interactButton.canTrigger = false;
					}
				}
			}
		}
	}

	public void TriggerTextbox()
	{
		if (canTextBoxDisplay) {
			if (textBox.isActivated) {
				if (GameController.instance.isAndroidVersion) {
					interactButton.TurnOffButton ();
				}
				toggleRespond ();
			} else {
				// feedtext into textbox
				if (colliderName.Length < 1) { // player stand at out of nowhere
					textBox.SetEventStatus (false);
					if (EndingController.instance.isChapter2Activated)
						feedText.SetText ("Amari..");
					else {
						if (PlayerData.ParentGenderId == 1) // Male Parent
							feedText.SetText ("Papa?");
						else if (PlayerData.ParentGenderId == 2) // Female Parent
							feedText.SetText ("Mama?");
						playerSound.PlayIdleDialogueSound ();
					}
				} else { //  player near to interactable object
					Item curr = GameController.instance.GetItem (colliderName);
					bool status = PlayerController.instance.AbleToTrigger (curr);

					if (!EndingController.instance.isChapter2Activated) {
						playerSound.PlayDialgoueSound (colliderName, status);
					}

					if (EndingController.instance.isChapter2Activated && TraceController.instance.storyList.Count > 0) {
						status = TraceController.instance.storyList [0].Contains (colliderName);
						if(status)
							GameController.instance.SetChapter2ObjectTime(colliderName);
					}

					textBox.SetEventStatus (status);

					string respond = curr.GetRespond (status, EndingController.instance.isChapter2Activated);
			
					if (respond.Length > 0) {		// not empty respond 
						feedText.SetText (respond, curr, status);
						transform.GetComponent <PlayerMovement> ().StopMoving ();	// disable player move
					}
				}

				currIndex = 0;
				textBox.TurnOnTextbox (false); // means do not fade out
			}
		}
	}

		
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Space) && !colliderName.Contains("ZoomPoint") ) {
			TriggerTextbox();
		}

		if (GameController.instance.isAndroidVersion) {
			if (colliderName.Length > 0 && !colliderName.Contains("Transition_") ) {
				if(!textBox.isActivated){
					interactButton.TurnOnButton();
					interactButton.canTrigger = true;

					if (EndingController.instance.isChapter2Activated && TraceController.instance.storyList.Count > 0) {
						interactButton.itemStatus = TraceController.instance.storyList [0].Contains (colliderName);
					}
					else
						interactButton.itemStatus = PlayerController.instance.AbleToTrigger( GameController.instance.GetItem(colliderName) );

				}
				else
					interactButton.TurnOffButton();
			} 
		}
	}
}
