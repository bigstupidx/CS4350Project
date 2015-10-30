using UnityEngine;
using System.Collections;

public class FinalPhoto : MonoBehaviour {

	public Texture2D[] photoSet = new Texture2D[2];

	public void SetFinalPhoto(int choice)
	{
		if (choice == 1)
			transform.GetComponent<MeshRenderer> ().material.mainTexture = photoSet [0];
		else
			transform.GetComponent<MeshRenderer> ().material.mainTexture = photoSet [1];
	}
}
