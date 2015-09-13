using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class GameController : MonoBehaviour {

	static public GameController instance;

	private Dictionary<string, Item> items;

	public void Awake() {
		instance = this;
	}

	public void Start() {
		GameController.instance.Init ();
		EndingController.instance.Init ();
		PlayerController.instance.Init (this.getInitialItems());
	}

	public void Init() {
		items = new Dictionary<string, Item> ();
		foreach (Transform t in this.transform) {
			items.Add(t.name, (Item) t.gameObject.GetComponent("Item"));
		}

		JsonData[] itemsData = JsonReader.readItems();
		foreach (JsonData itemData in itemsData) {
			if (!items.ContainsKey((string) itemData["id"])) {
				continue;
			}
			Item item = items[(string) itemData["id"]];
			item.itemId = (string) itemData["id"];
			item.restrictedItems = JsonReader.toStrArray(itemData["restrictedItems"]);
			item.requiredItems = JsonReader.toStrArray(itemData["requiredItems"]);
			item.leadItems = JsonReader.toStrArray(itemData["leadItems"]);
			item.eventDialogue = JsonReader.toStrArray(itemData["eventDialogue"]);
			item.defaultDialogue = JsonReader.toStrArray(itemData["defaultDialogue"]);
			item.idleDialogue = JsonReader.toStrArray(itemData["idleDialogue"]);
			item.endingPoints = JsonReader.toIntArray(itemData["endingPoints"]);
		}


	}

	public void GameOver(EndingType endingType) {
		Debug.Log ("Game Over");
	}

	public void TriggerItem(string itemId) {
		Item item = this.GetItem(itemId);
		if (item == null) {
			return;
		}
		if (PlayerController.instance.AbleToTrigger(item)) {
			PlayerController.instance.ItemTriggered(item);
			EndingController.instance.ItemTriggered(item);
			BroadcastMessage("ItemTriggered", item);
		}
	}

	public Item GetItem(string itemId) {
		Item item;
		bool hasItem = items.TryGetValue(itemId, out item);
		if (hasItem) {
			return item;
		}

		return null;
	}

	private List<string> getInitialItems() {
		List<string> initialItems = new List<string>();
		foreach (KeyValuePair<string, Item> entry in items) {
			if (entry.Value.requiredItems.Length == 0) {
				initialItems.Add(entry.Value.itemId);
			}
		}
		return initialItems;
	}
}
