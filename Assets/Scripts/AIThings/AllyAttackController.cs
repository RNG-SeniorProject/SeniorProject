using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllyAttackController : MonoBehaviour {
	// public Util util;

	[SerializeField]
	protected List<GameObject> knownAttacks;
	[SerializeField]
	protected List<GameObject> activeAttacks;

	private GameObject player;
	private AllyController allyController;
	// private CameraController cam;
	private Animator animator;

	private GameObject target;
	private bool enemyInRange;

	void Start(){
		player = GameObject.FindWithTag ("Player");
		if (player == null)
			Debug.Log ("Player not tagged");

		allyController = gameObject.GetComponent ("AllyController") as AllyController;
		// cam = util.camController;

		animator = gameObject.GetComponent<Animator> ();

		enemyInRange = false;
		target = null;
	}

	void Update(){
		if (enemyInRange && target != null && (transform.position - target.transform.position).magnitude < 5) {
			if (activeAttacks.Count != 0) {
				// if (cam.state == CameraController.CamState.Follow) {
				if (activeAttacks [0].GetComponent<Attack> ().performAttack (transform.gameObject)) {
					Quaternion turnDirection = Quaternion.LookRotation (target.transform.position - transform.position, Vector3.up);
					transform.rotation = Quaternion.Slerp (transform.rotation, turnDirection, 1f);

					animator.SetBool ("Swipe", true);

					StartCoroutine (delayedWait("Swipe", 1));
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

	IEnumerator delayedWait(string anim, float time){
		yield return new WaitForSeconds (1);

		animator.SetBool (anim, false);
	}

	public void SetTarget (GameObject enemy) {
		target = enemy;
		enemyInRange = true;
	}
		
	public void RemoveTarget() {
		target = null;
		enemyInRange = false;
	}
}