using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour {

	public string itemId;
	public string[] requiredItems;
	public string[] restrictedItems;
	public string[] leadItems;
	public string[] eventDialouge;
	public string[] defaultDialouge;
	public int[] endingPoints;

	public void ItemTriggered(Item item) {

	}
}
