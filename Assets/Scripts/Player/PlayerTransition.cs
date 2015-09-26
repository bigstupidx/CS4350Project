using UnityEngine;
using System.Collections;

public class PlayerTransition : MonoBehaviour {

	public void OnTriggerEnter(Collider other){
		switch (other.name) {
			case "Ground":
				{
					Debug.Log("I am going to Ground");
					Application.LoadLevel("GroundGameScene");
					//LevelHandler.Instance.LoadSpecific ("GroundGameScene");
					break;
				}
			case "Platform":
				{
					Application.LoadLevel ("PlatformGameScene");
					break;
				}
		}
	}
}
