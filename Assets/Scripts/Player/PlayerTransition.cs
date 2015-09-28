using UnityEngine;
using System.Collections;

public class PlayerTransition : MonoBehaviour {

	public void OnTriggerEnter(Collider other){
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
		}
	}
}
