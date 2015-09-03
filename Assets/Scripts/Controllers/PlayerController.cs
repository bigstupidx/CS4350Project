using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	static public PlayerController instance;

	public List<string> triggeredItems;
	public List<string> validItems;
	public List<string> restrictedItems;

	public void Awake() {
		instance = this;
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
		string itemId = item.itemId;
		triggeredItems.Add(itemId);
		validItems.Remove(itemId);
		foreach (string leadItemId in item.leadItems) {
			validItems.Add(leadItemId);
		}
		restrictedItems.Add (itemId);
		foreach (string restrictedItemId in item.restrictedItems) {
			restrictedItems.Add(restrictedItemId);
		}
	}
}
