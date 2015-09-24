using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour {
	
	public string itemId;
	public int level;
	public string[] requiredItems;
	public string[] restrictedItems;
	public string[] leadItems;
	public string[] eventDialogue;
	public string[] defaultDialogue;
	public string[] idleDialogue;
	public int[] endingPoints;
	

	public void ItemTriggered(Item item) {
		
	}

	public void loadItemState(ItemState state) {
		itemId = state.id;
		level = state.level;
		requiredItems = state.requiredItems;
		restrictedItems = state.restrictedItems;
		leadItems = state.leadItems;
		eventDialogue = state.eventDialogue;
		defaultDialogue = state.defaultDialogue;
		idleDialogue = state.idleDialogue;
		endingPoints = state.endingPoints;
	}

	public string GetRespond(bool isActivated)
	{
		if (isActivated) {
			return eventDialogue [0];
		} else {
			return defaultDialogue[(Random.Range(1, defaultDialogue.Length * 256) % defaultDialogue.Length)];
		}
	}

}

public class ItemState {
	public string id;
	public int level;
	public string[] requiredItems;
	public string[] restrictedItems;
	public string[] leadItems;
	public string[] eventDialogue;
	public string[] defaultDialogue;
	public string[] idleDialogue;
	public int[] endingPoints;

	public ItemState() {
		level = -1;
	}
}
