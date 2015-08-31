using UnityEngine;
using System.Collections;

public class animationContruct : MonoBehaviour {

	Animator animator;
	int moveHash = Animator.StringToHash("CubeAnimation");
	int idleStateHash = Animator.StringToHash ("Base Layer.Idle");

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		//To know the current state of the animation

		AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
		if (Input.GetKey(KeyCode.P)) {
			Debug.Log ("I am here");
			animator.SetBool ("haveToMove", true);
		} else {
			animator.SetBool ("haveToMove", false);
		}
	
	}
}
