using UnityEngine;
using System.Collections;

public class AllyController : MonoBehaviour {

	private GameObject player;
	private Animator animator;
	private NavMeshAgent agent;
	private Vector3 target;
	private bool idleWalking;

	public float idleRange = 10;
	public GameObject den;
	public GameObject den2;

	void Start () {
		player = GameObject.FindWithTag ("Player");
		if (player == null)
			Debug.Log ("Player not tagged");

		animator = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();

		target = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

		idleWalking = false;

		Migrate (den2);
	}

	void Update () {
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
		if ((den.transform.position - transform.position).magnitude > idleRange) {
			target = new Vector3 (den.transform.position.x + Random.Range(-idleRange, idleRange), den.transform.position.y, den.transform.position.z + Random.Range(-idleRange, idleRange));
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

	public bool CheckMigration (GameObject newDen) {
		GameObject[] predators = GameObject.FindGameObjectsWithTag ("Predator");

		RaycastHit[] hits = Physics.SphereCastAll(den.transform.position, den.GetComponent<NavMeshObstacle>().size.magnitude / 2, newDen.transform.position - den.transform.position, (newDen.transform.position - den.transform.position).magnitude);
		for (int i = 0; i < hits.Length; i++) {
			for (int j = 0; j < predators.Length; j++) {
				if (hits[i].transform.gameObject.GetInstanceID() == predators[j].GetInstanceID()) {
					return false;
				}
			}
		}

		return true;
	}

	public void Migrate (GameObject newDen) {
		den = newDen;
		target = new Vector3 (den.transform.position.x + Random.Range(-idleRange, idleRange), den.transform.position.y, den.transform.position.z + Random.Range(-idleRange, idleRange));
		agent.SetDestination (target);
		animator.SetFloat ("Speed", 0.5f);
		idleWalking = true;
	}
}
