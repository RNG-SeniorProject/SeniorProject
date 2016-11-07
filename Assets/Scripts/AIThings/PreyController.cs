using UnityEngine;
using System.Collections;

public class PreyController : MonoBehaviour {

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
		} else {
			this.target = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		}

		agent.destination = target;
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
	}
}
