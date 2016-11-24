using UnityEngine;
using System.Collections;

public class PreyController : MonoBehaviour {

	private GameObject player;
	private GameObject herdLeader;
	private PreyLeaderController herdLeaderController;
	private NavMeshAgent agent;
	private bool alarmed;
	private Vector3 target;

	public float chaseRange = 10;
	public float visionAngle = 60;
	public float idleRange = 5;
	public float rangeMultiplier = 1;

	void Start () {
		player = GameObject.FindWithTag ("Player");
		if (!player)
			Debug.Log ("Player not tagged");

		if (transform.parent != null) {
			GameObject[] prey = new GameObject[transform.parent.childCount];
			for (int i = 0; i < prey.Length; i++) {
				prey [i] = transform.parent.GetChild (i).gameObject;
				PreyLeaderController preyLeaderController = prey [i].GetComponent ("PreyLeaderController") as PreyLeaderController;
				if (preyLeaderController) {
					herdLeader = preyLeaderController.gameObject;
					herdLeaderController = preyLeaderController;
					break;
				}
			}
			if (!herdLeader)
				Debug.Log ("Herd Leader failed to instantiate [PreyController]");
		}
		
		agent = GetComponent<NavMeshAgent> ();

		alarmed = false;
		target = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
	}

	void Update () {
		Vector3 targetDir = player.transform.position - transform.position;
		float angle = Vector3.Angle (targetDir, transform.forward);

		// if target is within the angle of vision and within chase range
		if (!alarmed && angle < visionAngle && targetDir.magnitude < chaseRange) {
			if (herdLeader != null) {
				herdLeaderController.StartFleeing ();
			} else {
				StartFleeing ();
			}
		} else if (alarmed && targetDir.magnitude > rangeMultiplier * chaseRange) {
			StopFleeing ();
		}

		if (alarmed) {
			target = new Vector3 (transform.position.x - targetDir.x, transform.position.y - targetDir.y, transform.position.z - targetDir.z);
		} else {
			if (Random.value < 0.001) {
				target = new Vector3 (transform.position.x + Random.Range (-idleRange, idleRange), transform.position.y, transform.position.z + Random.Range (-idleRange, idleRange));
				if (herdLeader != null && (herdLeader.transform.position - transform.position).magnitude > idleRange) {
					target = new Vector3 (herdLeader.transform.position.x + Random.Range(-idleRange, idleRange), herdLeader.transform.position.y, herdLeader.transform.position.z + Random.Range(-idleRange, idleRange));
				}
			}
		}

		agent.SetDestination (target);
	}

	public void StartFleeing () {
		alarmed = true;
	}

	public bool IsFleeing () {
		return alarmed;
	}

	public void StopFleeing () {
		alarmed = false;
		if (herdLeader != null) {
			target = new Vector3 (herdLeader.transform.position.x + Random.Range (-idleRange, idleRange), herdLeader.transform.position.y, herdLeader.transform.position.z + Random.Range (-idleRange, idleRange));
		} else {
			target = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		}
		agent.SetDestination (target);
	}

	/*
	// private GameObject player;
	private NavMeshAgent agent;
	private bool targeted;
	private Vector3 target;
	private GameObject player;

	public float detectDistance;
	public float detectAngle;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
		if (!player) Debug.Log ("Player not tagged");

		target = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

		// NavMeshAgent does pathfinding around the map automatically
		agent = GetComponent<NavMeshAgent> ();

		targeted = false;
	}

	void Update() {
		Vector3 targetDir = player.transform.position - transform.position;
		float angle = Vector3.Angle(targetDir, transform.forward);

		// if target is within the angle of vision and within chase range
		if (!targeted && angle < detectAngle && targetDir.magnitude <= detectDistance) {
			StartFleeing ();
		} else if (targetDir.magnitude > detectDistance && targeted) { // target gets out of leader chase range
			StopFleeing ();
		}

		if (targeted) {
			this.target = transform.position - targetDir;
			agent.SetDestination (target);
		} else {
			agent.ResetPath ();
		}
	}

	public void StartFleeing() {
		targeted = true;
		// this.target = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, targetObject.transform.position.z);
	}

	public void StopFleeing() {
		targeted = false;
		// this.target = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, targetObject.transform.position.z);
	}

	public bool IsFleeing() {
		return targeted;
	}*/
}
