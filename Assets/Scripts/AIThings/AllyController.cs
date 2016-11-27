using UnityEngine;
using System.Collections;

public class AllyController : MonoBehaviour {
	public Util util;

	private AllyPackController packCon;
	private DenController den;

	private GameObject player;
	private Animator animator;
	private NavMeshAgent agent;
	private Vector3 target;
	private bool idleWalking;

	public float idleRange = 10;

	void Start () {
		player = GameObject.FindWithTag ("Player");
		if (player == null)
			Debug.Log ("Player not tagged");

		packCon = util.packCon;
		den = util.den;

		animator = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();

		target = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

		idleWalking = false;
	}

	void Update () {
		if (packCon.isMigrating) {
			if (packCon.wait) {
				StopIdleWalk ();
			} else {
				StartIdleWalk ();
			}
		}

		if (Random.value < 0.001) {
			StartIdleWalk ();
		}
		if (idleWalking) {
			if ((target - transform.position).magnitude < 2.5) {
				StopIdleWalk ();
			}
		}
	}

	private void StartIdleWalk () {
		target = new Vector3 (transform.position.x + Random.Range (-idleRange, idleRange), transform.position.y, transform.position.z + Random.Range (-idleRange, idleRange));
		if (((packCon.idlePos + ((idleRange) * den.currentDen.transform.right)) - transform.position).magnitude > idleRange) {
			target = new Vector3 (packCon.idlePos.x + Random.Range (-idleRange, idleRange), packCon.idlePos.y, packCon.idlePos.z + Random.Range (-idleRange, idleRange)) + (idleRange) * den.currentDen.transform.right;
		} else {
			if (packCon.idlePos == util.den.currentDen.transform.position) {
				packCon.isMigrating = false;
				util.den.migrate = false;
				util.uiManager.hideMigrateWarning ();
			}
		}
		agent.SetDestination (target);
		animator.SetFloat ("Speed", 0.5f);
		idleWalking = true;
	}

	private void StopIdleWalk () {
		target = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		agent.ResetPath ();
		animator.SetFloat ("Speed", 0f);
		idleWalking = false;
	}

	public void Migrate () {
		target = new Vector3 (packCon.idlePos.x + Random.Range(-idleRange, idleRange), packCon.idlePos.y, packCon.idlePos.z + Random.Range(-idleRange, idleRange));
		agent.SetDestination (target);
		animator.SetFloat ("Speed", 0.5f);
	}
}
