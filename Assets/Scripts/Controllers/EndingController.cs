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

	int[] endings;

	public void Awake() {
		instance = this;
	}

	public void Init() {
		endings = new int[(int) EndingType.EndingCount];
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
				GameController.instance.GameOver((EndingType) i);
				break;
			}
		}
	}
}
