using UnityEngine;
using System.Collections;

public class Pathfinding : MonoBehaviour {

	private GameObject player;
	private NavMeshAgent agent;

	public GameObject target;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");

		// NavMeshAgent does pathfinding around the map automatically
		agent = GetComponent<NavMeshAgent> ();
	}

	void Update() {

		// set the ally/enemy's target destination to the player
		agent.destination = target.transform.position;
		// needs some work for flocking: arranging neatly and without colliding with each other and the player
	}

}
