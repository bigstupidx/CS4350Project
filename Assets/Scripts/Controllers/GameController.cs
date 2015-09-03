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
		PlayerController.instance.Init (this.getInitialItems());
	}

	public void Init() {
		items = new Dictionary<string, Item> ();
		foreach (Transform t in this.transform) {
			items.Add(t.name, (Item) t.gameObject.GetComponent("Item"));
		}

		JsonData[] itemsData = JsonReader.readItems();
		foreach (JsonData itemData in itemsData) {
			Item item = items[(string) itemData["id"]];
			if (item == null) {
				continue;
			}
			item.itemId = (string) itemData["id"];
			item.restrictedItems = JsonReader.toStrArray(itemData["restrictedItems"]);
			item.requiredItems = JsonReader.toStrArray(itemData["requiredItems"]);
			item.leadItems = JsonReader.toStrArray(itemData["leadItems"]);
			item.eventDialouge = JsonReader.toStrArray(itemData["eventDialouge"]);
			item.defaultDialouge = JsonReader.toStrArray(itemData["defaultDialouge"]);
			item.endingPoints = JsonReader.toIntArray(itemData["endingPoints"]);
		}
	}

	public void GameOver(EndingType endingType) {

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
