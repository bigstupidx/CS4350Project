using UnityEngine;
using System.Collections;

public class PlayerTransition : MonoBehaviour {

	public void OnTriggerEnter(Collider other){
		string colliderName = other.name;
		Item item = GameController.instance.GetItem (colliderName);
		if (item.type == Item.TRANSITION_TYPE) {
			GameController.instance.TriggerItem(item.itemId);
			return;
		}
	}
}
