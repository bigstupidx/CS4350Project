using UnityEngine;
using System.Collections;

public class PlayerSound : MonoBehaviour {

    PlayerMovement movementScript;

    AudioSource audioSource;
    AudioClip[] idleAutoClips;
    AudioClip idleDialogueClip;
    AudioClip[] dialogueAudioClips;
    AudioClip[] specificDialogueClips;
    public static bool AllowIdleAutoSound = true;

    static float MinIdleTime = 10.0f;
    static float MaxIdleTime = 20.0f;

    float idleTimer = 0.0f;

    // Use this for initialization
    void Start () {
        audioSource = transform.GetComponent<AudioSource>();
        movementScript = transform.GetComponent<PlayerMovement>();

        // Load idle auto clips
        idleAutoClips = new AudioClip[5];
        idleAutoClips[0] = Resources.Load<AudioClip>("Sound/Amari/humming");
        idleAutoClips[1] = Resources.Load<AudioClip>("Sound/Amari/i_want_to_go_home");
        idleAutoClips[2] = Resources.Load<AudioClip>("Sound/Amari/what_should_i_do");
		idleAutoClips[3] = Resources.Load<AudioClip>("Sound/Amari/what_is_this");
		idleAutoClips[4] = Resources.Load<AudioClip>("Sound/Amari/woah");

        ResetIdleTimer();

        // Load idle dialogue clip
        if (PlayerData.ParentGenderId== 1) {
            idleDialogueClip = Resources.Load<AudioClip>("Sound/Amari/papa");
        }
        else
        {
            idleDialogueClip = Resources.Load<AudioClip>("Sound/Amari/mama");
        }

        // Load standard dialogue clips
        dialogueAudioClips = new AudioClip[9];
        dialogueAudioClips[0] = Resources.Load<AudioClip>("Sound/Amari/awawa");
        dialogueAudioClips[1] = Resources.Load<AudioClip>("Sound/Amari/chuckle");
        dialogueAudioClips[2] = Resources.Load<AudioClip>("Sound/Amari/hmm");
        dialogueAudioClips[3] = Resources.Load<AudioClip>("Sound/Amari/lets_see");
        dialogueAudioClips[4] = Resources.Load<AudioClip>("Sound/Amari/maybe..");
		dialogueAudioClips[5] = Resources.Load<AudioClip>("Sound/Amari/oh");
		dialogueAudioClips[6] = Resources.Load<AudioClip>("Sound/Amari/oh_no");
		dialogueAudioClips[7] = Resources.Load<AudioClip>("Sound/Amari/awright");
		dialogueAudioClips[8] = Resources.Load<AudioClip>("Sound/Amari/booya");

        specificDialogueClips = new AudioClip[5];
        specificDialogueClips[0] = Resources.Load<AudioClip>("Sound/InteractableSFX/coin_3");
        specificDialogueClips[1] = Resources.Load<AudioClip>("Sound/InteractableSFX/coin_splash");
        specificDialogueClips[2] = Resources.Load<AudioClip>("Sound/InteractableSFX/dog_bark_1");
        specificDialogueClips[3] = Resources.Load<AudioClip>("Sound/InteractableSFX/eating_cake");
        specificDialogueClips[4] = Resources.Load<AudioClip>("Sound/InteractableSFX/gulp");

    }
	
	// Update is called once per frame
	void Update () {

        if (AllowIdleAutoSound && !movementScript.isMoving())
        {
            idleTimer -= Time.deltaTime;
            if(idleTimer <= 0)
            {
                PlayIdleAutoDialogueSound();
                ResetIdleTimer();
            }
        }
        else
        {
            ResetIdleTimer();
        }

	}

    void ResetIdleTimer()
    {
        idleTimer = Random.Range(MinIdleTime, MaxIdleTime);
    }

    void PlayIdleAutoDialogueSound()
    {
        AudioClip selectedClip = idleAutoClips[Random.Range(0, idleAutoClips.Length)];
        PlaySound(selectedClip);
    }

    public void PlayDialgoueSound(string colliderName, bool status)
    {
        AudioClip selectedClip;
        if (colliderName.Contains("WishingFountain") && status)
        {
            selectedClip = specificDialogueClips[1];
        }
        else if (colliderName.Equals("CoinPurse") && status)
        {
            selectedClip = specificDialogueClips[0];
        }
        else if (colliderName.Contains("Dog"))
        {
            selectedClip = specificDialogueClips[2];
        }
        else if (colliderName.Equals("TableWithCake") && status)
        {
            selectedClip = specificDialogueClips[3];
        }
        else if (colliderName.Equals("WaterCooler") && status)
        {
            selectedClip = specificDialogueClips[4];
        }
        else
        {
            selectedClip = dialogueAudioClips[Random.Range(0, dialogueAudioClips.Length)];
        }

        PlaySound(selectedClip);
    }

    public void PlayIdleDialogueSound() {
        ResetIdleTimer();
        PlaySound(idleDialogueClip);
    }



    public void PlaySound(AudioClip clip) {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }




}
