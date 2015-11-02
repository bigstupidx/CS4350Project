using UnityEngine;
using System.Collections;

public class ObjectSounds : MonoBehaviour {

	public AudioClip[] clips;
	public float MinTimeBeforePlay = 5.0f;
	public float MaxTimeBeforePlay = 10.0f;

	AudioSource audioSource;

	float timer;
	// Use this for initialization
	void Start () {
		audioSource = transform.GetComponent<AudioSource> ();
		ResetTimer ();
	}

	void ResetTimer (){
		timer = Random.Range (MinTimeBeforePlay, MaxTimeBeforePlay);
	}

	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		if(timer <= 0.0f && clips.Length > 0){

			audioSource.clip = clips[Random.Range (0,clips.Length)];
			audioSource.Play ();
			ResetTimer ();
		}
	}
}
