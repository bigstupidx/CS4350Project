using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	static public PlayerController instance;

	public List<string> triggeredItems;
	public List<string> validItems;
	public List<string> restrictedItems;

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
		Debug.Log("PlayerController");
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
		GameObject.Find("TextBox").GetComponent<FadeInFadeOut>().TurnOnTextbox();
	}
}
