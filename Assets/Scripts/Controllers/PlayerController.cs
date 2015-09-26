using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	static public PlayerController instance;

	public List<string> triggeredItems;
	public Dictionary<string, string> validItems;
	public Dictionary<string, string> restrictedItems;
	public int currentLevel;

	private float idleTimer;

	public void Awake() {
		instance = this;
		DontDestroyOnLoad (this);
		idleTimer = Time.time;
	}

	public void Init() {
		triggeredItems = new List<string>();
		validItems = new Dictionary<string, string> ();
		restrictedItems = new Dictionary<string, string>();
	}

	public void AddInitialItems(List<string> initialItems) {
		foreach (string itemId in initialItems) {
			validItems.Add(itemId, "true");
		}
	}

	public void Load() {
		PlayerState saveState = JsonReader.readPlayerState ();
		this.triggeredItems = saveState.triggeredItems;
		validItems = new Dictionary<string, string> ();
		foreach (string itemId in saveState.validItems) {
			validItems.Add(itemId, "true");
		}
		restrictedItems = new Dictionary<string, string> ();
		foreach (string itemId in saveState.restrictedItems) {
			validItems.Add(itemId, "true");
		}
		this.currentLevel = saveState.currentLevel;
	}

	public void Save() {
		PlayerState saveState = new PlayerState ();
		saveState.triggeredItems = this.triggeredItems;
		saveState.validItems = new List<string>(this.validItems.Keys);
		saveState.restrictedItems = new List<string>(this.restrictedItems.Keys);
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

		return validItems.ContainsKey(itemId) 
			&& !triggeredItems.Contains(itemId) 
			&& !restrictedItems.ContainsKey(itemId);
	}

	public void ItemTriggered(Item item) {
		string itemId = item.itemId;
		if (item.type.Equals (Item.EVENT_TYPE)) {
			triggeredItems.Add(itemId);
			validItems.Remove(itemId);
			restrictedItems.Add(itemId, "true");
		}

		foreach (string leadItemId in item.leadItems) {
			validItems.Add(leadItemId, "true");
		}

		foreach (string restrictedItemId in item.restrictedItems) {
			restrictedItems.Add(restrictedItemId, "true");
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
		List<string> items = new List<string> (validItems.Keys);
		Item validItem = GameController.instance.GetItem( items[items.Count - 1] );
		GameObject.Find("ObjectRespond").GetComponent<FeedTextFromObject>().SetText(validItem.idleDialogue[0]);
		GameObject.Find("TextBox").GetComponent<FadeInFadeOut>().TurnOnTextbox(false);
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
