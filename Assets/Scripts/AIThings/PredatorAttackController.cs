using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PredatorAttackController : MonoBehaviour {
	// public Util util;

	[SerializeField]
	protected List<GameObject> knownAttacks;
	[SerializeField]
	protected List<GameObject> activeAttacks;

	private GameObject player;
	private PredatorController predatorController;
	// private CameraController cam;
	private Animator animator;

	void Start(){
		player = GameObject.FindWithTag ("Player");
		if (player == null)
			Debug.Log ("Player not tagged");

		predatorController = gameObject.GetComponent ("PredatorController") as PredatorController;
		// cam = util.camController;

		animator = gameObject.GetComponent<Animator> ();
	}

	void Update(){
<<<<<<< HEAD
		if (predatorController.IsChasing ()) {
			if (target == null) {
				predatorController.StopChasing ();
			} else if (target != null && (transform.position - target.transform.position).magnitude < 5) {
				if (activeAttacks.Count != 0) {
					// if (cam.state == CameraController.CamState.Follow) {
					if (activeAttacks [0].GetComponent<Attack> ().performAttack (transform.gameObject)) {
						Quaternion turnDirection = Quaternion.LookRotation (target.transform.position - transform.position, Vector3.up);
						transform.rotation = Quaternion.Slerp (transform.rotation, turnDirection, 1f);
=======
		if (predatorController.IsChasing() && (transform.position - player.transform.position).magnitude < 5) {
			if (activeAttacks.Count != 0) {
				// if (cam.state == CameraController.CamState.Follow) {
					if (activeAttacks [0].GetComponent<Attack> ().performAttack (transform.gameObject)) {
>>>>>>> NataliesJunk

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

	IEnumerator delayedWait(string anim, float time){
		yield return new WaitForSeconds (1);

		animator.SetBool (anim, false);
	}
}