using UnityEngine;
using System.Collections;

public class PlayerTransition : MonoBehaviour {

	private Vector3 pos_inside_gantry;
	private Vector3 pos_outside_gantry;

	void Start()
	{
		pos_inside_gantry = GameObject.Find("Gantry_Inside").transform.position;
		pos_outside_gantry = GameObject.Find("Gantry_Outside").transform.position;

		pos_outside_gantry.z += 1.0f;
		pos_inside_gantry.z -= 1.0f;
	}

	public void OnTriggerEnter(Collider other){
		string colliderName = other.name;
		Item item = GameController.instance.GetItem (colliderName);
		if (item.type == Item.TRANSITION_TYPE) {
			GameController.instance.TriggerItem(item.itemId);
			return;
		}
		switch (other.name) {
			case "Ground":
				{
					//Debug.Log("I am going to Ground");
					//Application.LoadLevel("GroundGameScene");
					//LevelHandler.Instance.LoadSpecific ("GroundGameScene");
					LevelHandler.Instance.LoadSpecific ("Testing_scene_1");
					break;
				}
			case "Platform":
				{
					//Application.LoadLevel ("PlatformGameScene");
					LevelHandler.Instance.LoadSpecific ("PlatformGameScene");
					break;
				}
			case "Sewage":
			{
				LevelHandler.Instance.LoadSpecific ("Testing_scene_0");
				break;
			}
			case "Gantry_Outside":
			{
				gameObject.GetComponent<PlayerMovement>().StopMoving();
				transform.position = pos_inside_gantry;
				break;
			}
			case "Gantry_Inside":
			{
				gameObject.GetComponent<PlayerMovement>().StopMoving();
				transform.position = pos_outside_gantry;
				break;
			}
		}
	}
}
