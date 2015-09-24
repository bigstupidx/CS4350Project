using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	static public PlayerController instance;

	public List<string> triggeredItems;
	public List<string> validItems;
	public List<string> restrictedItems;
	public int currentLevel;

	private float idleTimer;

	public void Awake() {
		instance = this;
		idleTimer = Time.time;
	}

	public void Init(List<string> initialItems) {
		triggeredItems = new List<string>();
		validItems = initialItems;
		restrictedItems = new List<string>();
	}

	public void Load() {
		PlayerState saveState = JsonReader.readPlayerState ();
		this.triggeredItems = saveState.triggeredItems;
		this.validItems = saveState.validItems;
		this.restrictedItems = saveState.restrictedItems;
		this.currentLevel = saveState.currentLevel;
	}

	public void Save() {
		PlayerState saveState = new PlayerState ();
		saveState.triggeredItems = this.triggeredItems;
		saveState.validItems = this.validItems;
		saveState.restrictedItems = this.restrictedItems;
		saveState.currentLevel = this.currentLevel;
		JsonReader.writePlayerState (saveState);
	}

	public bool AbleToTrigger(Item item) {
		string itemId = item.itemId;
		foreach (string requiredItemId in item.requiredItems) {
			if (!triggeredItems.Contains(requiredItemId)) {
				return false;
			}
		}

		return validItems.Contains(itemId) 
			&& !triggeredItems.Contains(itemId) 
			&& !restrictedItems.Contains(itemId);
	}

	public void ItemTriggered(Item item) {
		string itemId = item.itemId;
		triggeredItems.Add(itemId);
		validItems.Remove(itemId);
		foreach (string leadItemId in item.leadItems) {
			validItems.Add(leadItemId);
		}
		restrictedItems.Add (itemId);
		foreach (string restrictedItemId in item.restrictedItems) {
			restrictedItems.Add(restrictedItemId);
			validItems.Remove(restrictedItemId);
		}

		idleTimer = Time.time;
	}

	public void Update()
	{
		if( (Time.time - idleTimer) / 60 >= 1) {
			displayHint();
			idleTimer = Time.time;
		}
	}
	public void displayHint()
	{
		Item validItem = GameController.instance.GetItem( validItems[validItems.Count-1] );
		GameObject.Find("ObjectRespond").GetComponent<FeedTextFromObject>().SetText(validItem.idleDialogue[0]);
		GameObject.Find("TextBox").GetComponent<FadeInFadeOut>().TurnOnTextbox(true);
	}
}

public class PlayerState {
	public List<string> triggeredItems;
	public List<string> validItems;
	public List<string> restrictedItems;
	public int currentLevel;

	public PlayerState() {
		triggeredItems = new List<string> ();
		validItems = new List<string> ();
		restrictedItems = new List<string> ();
	}
}
