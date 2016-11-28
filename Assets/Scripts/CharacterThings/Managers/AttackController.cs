using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackController : MonoBehaviour {
	public Util util;

	[SerializeField]
	protected List<GameObject> knownAttacks;
	[SerializeField]
	protected List<GameObject> activeAttacks;

	private CameraController cam;
	private Animator animator;

	void Start(){
		cam = util.camController;

		animator = gameObject.GetComponent<Animator> ();
	}

	void Update(){
		if (util.time.paused) {return;}

		if (Input.GetMouseButton (0)) {
			if (activeAttacks.Count != 0) {
				if (cam.state == CameraController.CamState.Follow) {
					if (activeAttacks [0].GetComponent<Attack> ().performAttack (transform.gameObject)) {

						animator.SetBool ("Swipe", true);
						StartCoroutine (delayedWait("Swipe", 1));
					}

				} else {
					if (!util.chrLogic.inLocomotion())
					if (activeAttacks [1].GetComponent<Attack> ().performAttack (transform.gameObject)) {

						animator.SetBool ("Ranged", true);

						StartCoroutine (delayedWait("Ranged", 1));
					}
				}
			}
		}
	}

	IEnumerator delayedWait(string anim, float time){
		yield return new WaitForSeconds (time);

		animator.SetBool (anim, false);
	}
}