using UnityEngine;
using System.Collections;

public enum EndingType: int { 
	Ending1, 
	Ending2, 
	Ending3,
	Ending4,
	Ending5,
	EndingCount
}

public class EndingController : MonoBehaviour {

	private const int ENDING_LIMIT = 100;

	static public EndingController instance;
	public bool isChapter2Activated = false;
	public bool isChapter2Completed = false;

	public int[] endings;
	public EndingType deathReason;

	public void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this);
		} else {
			DestroyImmediate(gameObject);
		}
	}

	public void Init() {
		isChapter2Activated = false;
		isChapter2Completed = false;
		endings = new int[(int) EndingType.EndingCount];
	}

	public void Reset() {
		this.Init ();
	}

	public void Load() {
		EndingSaveState saveState = JsonReader.readEndingSaveState ();
		this.isChapter2Activated = saveState.isChapter2Activated;
		this.isChapter2Completed = saveState.isChapter2Completed;
		this.endings = saveState.endings;
		this.deathReason = saveState.deathReason;
	}
	
	public void Save() {
		EndingSaveState saveState = new EndingSaveState ();
		saveState.isChapter2Activated = this.isChapter2Activated;
		saveState.isChapter2Completed = this.isChapter2Completed;
		saveState.endings = this.endings;
		saveState.deathReason = this.deathReason;
		JsonReader.writeEndingSaveState (saveState);
	}

	public void ResetEndingController(bool _activateChapter2) {
		endings = new int[(int) EndingType.EndingCount];
		isChapter2Activated = _activateChapter2;
		if (!_activateChapter2) {
			isChapter2Completed = false;
		}
		this.Save ();
	}

	public void ItemTriggered(Item item) {
		if (item.type.Equals (Item.TRANSITION_TYPE)) {
			return;
		}
		for (int i = 0; i < endings.Length; i++) {
			if (i < item.endingPoints.Length) {
				endings[i] += item.endingPoints[i];
			}
			if (endings[i] > ENDING_LIMIT) {
				deathReason = (EndingType) i;
				this.Save();
				GameController.instance.GameOver((EndingType) i);
				break;
			}
		}
		this.Save ();
	}
}

public class EndingSaveState {
	public bool isChapter2Activated;
	public bool isChapter2Completed;
	public int[] endings;
	public EndingType deathReason;
	
	public EndingSaveState() {
		isChapter2Activated = false;
		isChapter2Completed = false;
		endings = new int[(int) EndingType.EndingCount];
		deathReason = EndingType.Ending1;
	}
}
