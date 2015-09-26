using UnityEngine;
using System.Collections;

public class PlayerTransition : MonoBehaviour {

	public void OnTriggerEnter(Collider other){
		switch (other.name) {
			case "Ground":
				{
					LevelHandler.Instance.LoadSpecific ("GroundGameScene");
					break;
				}
			case "Platform":
				{
					LevelHandler.Instance.LoadSpecific ("PlatformGameScene");
					break;
				}
		}
	}
}
