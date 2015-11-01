using UnityEngine;
using System.Collections;

public class UISounds : MonoBehaviour {

    AudioSource[] audioSources;

	// Use this for initialization
	void Start () {

        audioSources = transform.GetComponents<AudioSource>();

	}
	
    public void PlayClickSound()
    {
        audioSources[1].Play();
    }

    public void PlayHoverSound()
    {
        audioSources[0].Play();
    }


    // Update is called once per frame
    void Update () {
	
	}
}
