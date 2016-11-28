using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PredatorAttackController : MonoBehaviour {
	// public Util util;

	[SerializeField]
	protected List<GameObject> knownAttacks;
	[SerializeField]
	protected List<GameObject> activeAttacks;

	private PredatorController predatorController;
	// private CameraController cam;
	private Animator animator;

	private GameObject target;
	private bool enemyInRange;

	void Start(){
		predatorController = gameObject.GetComponent ("PredatorController") as PredatorController;
		// cam = util.camController;

		animator = gameObject.GetComponent<Animator> ();

		target = GameObject.FindWithTag ("Player");
	}

	void Update(){
		if (predatorController.IsChasing ()) {
			if (target == null) {
				predatorController.StopChasing ();
			} else if (target != null && (transform.position - target.transform.position).magnitude < 5) {
				if (activeAttacks.Count != 0) {
					// if (cam.state == CameraController.CamState.Follow) {

					Quaternion turnDirection = Quaternion.LookRotation (target.transform.position - transform.position, Vector3.up);
					transform.rotation = Quaternion.Slerp (transform.rotation, turnDirection, 0.1f);

					if (activeAttacks [0].GetComponent<Attack> ().performAttack (transform.gameObject)) {
						animator.SetBool ("Swipe", true);

						StartCoroutine (delayedWait ("Swipe", 1));
					}

					// }
					/*else {
						if (!util.chrLogic.IsInLocomotion())
						if (activeAttacks [1].GetComponent<Attack> ().performAttack (transform.gameObject)) {

							animator.SetBool ("Ranged", true);

							StartCoroutine (delayedWait("Ranged", 1));
						}
					}*/
				}
			}
		}
	}

	public void SetTarget (GameObject enemy) {
		target = enemy;
		enemyInRange = true;
	}

	public void RemoveTarget() {
		target = null;
		enemyInRange = false;
	}

	IEnumerator delayedWait(string anim, float time){
		yield return new WaitForSeconds (1);

		animator.SetBool (anim, false);
	}
}