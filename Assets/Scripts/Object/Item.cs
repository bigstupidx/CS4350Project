using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour {

	public readonly static string EVENT_TYPE = "event";
	public readonly static string TRANSITION_TYPE = "transition";
	
	public string itemId;
	public string type;
	public int level;
	public string[] requiredItems;
	public string[] restrictedItems;
	public string[] leadItems;
	public string[] hideItems;
	public string[] unhideItems;
	public string[] eventDialogue;
	public string[] defaultDialogue;
	public string[] idleDialogue;
	public int[] endingPoints;
	public double[] offset;
	public bool isInitiallyHidden;
	

	public void ItemTriggered(Item item) {
		
	}

	public void loadEventItemState(ItemState state) {
		itemId = state.id;
		type = EVENT_TYPE;
		level = state.level;
		requiredItems = state.requiredItems;
		restrictedItems = state.restrictedItems;
		leadItems = state.leadItems;
		hideItems = state.hideItems;
		unhideItems = state.unhideItems;
		eventDialogue = state.eventDialogue;
		defaultDialogue = state.defaultDialogue;
		idleDialogue = state.idleDialogue;
		endingPoints = state.endingPoints;
		isInitiallyHidden = state.isInitiallyHidden;
	}

	public void loadTransitionItemState(ItemState state) {
		itemId = state.id;
		type = TRANSITION_TYPE;
		level = state.level;
		requiredItems = state.requiredItems;
		leadItems = state.leadItems;
		eventDialogue = state.eventDialogue;
		defaultDialogue = state.defaultDialogue;
		offset = state.offset;
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
	public string type;
	public int level;
	public string[] requiredItems;
	public string[] restrictedItems;
	public string[] leadItems;
	public string[] hideItems;
	public string[] unhideItems;
	public string[] eventDialogue;
	public string[] defaultDialogue;
	public string[] idleDialogue;
	public int[] endingPoints;
	public double[] offset;
	public bool isInitiallyHidden;

	public ItemState() {
		type = Item.EVENT_TYPE;
		level = -1;
		requiredItems = new string[0];
		restrictedItems = new string[0];
		leadItems = new string[0];
		hideItems = new string[0];
		unhideItems = new string[0];
		eventDialogue = new string[0];
		defaultDialogue = new string[0];
		endingPoints = new int[(int) EndingType.EndingCount];
		offset = new double[2];
		isInitiallyHidden = false;
	}
}
