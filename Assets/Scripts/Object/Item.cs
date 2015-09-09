using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour {
	
	public string itemId;
	public string[] requiredItems;
	public string[] restrictedItems;
	public string[] leadItems;
	public string[] eventDialogue;
	public string[] defaultDialogue;
	public int[] endingPoints;
	

	public void ItemTriggered(Item item) {
		
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
