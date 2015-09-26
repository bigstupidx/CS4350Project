using UnityEngine;
using System.Collections;

public class PlayerTransition : MonoBehaviour {

	public void OnTriggerEnter(Collider other){
		switch (other.name) {
			case "Ground":
				{
					Debug.Log("I am going to Ground");
					//Application.LoadLevel("GroundGameScene");
					Application.LoadLevel("Testing_scene_1");
					//LevelHandler.Instance.LoadSpecific ("GroundGameScene");
					break;
				}
			case "Platform":
				{
					//Application.LoadLevel ("PlatformGameScene");
					Application.LoadLevel("Testing_scene");
					break;
				}
		}
	}
}
