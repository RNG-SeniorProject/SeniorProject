using UnityEngine;
using System.Collections;

public class Pathfinding : MonoBehaviour {

	// private GameObject player;
	private NavMeshAgent agent;
	private bool targeted;

	public GameObject target;
	public float followDistance;
	public float followAngle;

	// Use this for initialization
	void Start () {
		/* player = GameObject.FindWithTag ("Player");
		if (!player) {
			Debug.Log ("Player not tagged");
		} */

		// NavMeshAgent does pathfinding around the map automatically
		agent = GetComponent<NavMeshAgent> ();
		targeted = false;
	}

	void Update() {
		Vector3 targetDir = target.transform.position - transform.position;
		float angle = Vector3.Angle(targetDir, transform.forward);
		Debug.Log (angle);

		// if target is within the angle of vision and within chase range
		if (!targeted && angle < followAngle && targetDir.magnitude <= followDistance) {
			targeted = true; // starts chasing
		} else if (targetDir.magnitude > followDistance && targeted) { // target gets out of chase range
			targeted = false; // stops chasing
		}

		if (targeted) { // set the ally/enemy's target destination to the player
			agent.destination = target.transform.position;
		} else { // stop chasing, stop in place
			agent.destination = transform.position;
		}
		// needs some work for flocking: arranging neatly and without colliding with each other and the player
	}

}
