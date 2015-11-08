using UnityEngine;
using System.Collections;

public class SkipToPlatform : MonoBehaviour {

	// Use this for initialization
	public void JumpToPlatform()
	{
		Application.LoadLevel("PlatformGameScene");
		GameController.instance.SetStartTime();
	}
	public void JumpToNursery()
	{
		Application.LoadLevel("CharacterSelectionScreen");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Space))
        {
			JumpToPlatform();
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
			JumpToNursery();
        }
    }
}
