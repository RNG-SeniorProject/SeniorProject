using UnityEngine;
using System.Collections;

public class PredatorController : MonoBehaviour {

	private GameObject player;
	private GameObject packLeader;
	private PredatorLeaderController packLeaderController;
	private NavMeshAgent agent;
	private bool targeted;
	private Vector3 target;

	public float chaseRange = 10;
	public float visionAngle = 60;
	public float idleRange = 5;
	public float rangeMultiplier = 1;

	void Start () {
		player = GameObject.FindWithTag ("Player");
		if (!player)
			Debug.Log ("Player not tagged");

		GameObject[] predators = new GameObject[transform.parent.childCount];
		for (int i = 0; i < predators.Length; i++) {
			predators [i] = transform.parent.GetChild (i).gameObject;
			PredatorLeaderController predatorLeaderController = predators [i].GetComponent ("PredatorLeaderController") as PredatorLeaderController;
			if (predatorLeaderController) {
				packLeader = predatorLeaderController.gameObject;
				packLeaderController = predatorLeaderController;
			}
		}
		if (!packLeader)
			Debug.Log ("Pack Leader failed to instantiate [PredatorController]");

		agent = GetComponent<NavMeshAgent> ();

		targeted = false;
		target = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
	}

	void Update () {
		Vector3 targetDir = player.transform.position - transform.position;
		float angle = Vector3.Angle (targetDir, transform.forward);

		// if target is within the angle of vision and within chase range
		if (!targeted && angle < visionAngle && targetDir.magnitude < chaseRange) {
			packLeaderController.StartChasing();
		} else if (targeted && targetDir.magnitude > rangeMultiplier * chaseRange && gameObject.GetInstanceID() == packLeader.GetInstanceID()) { // target gets out of leader chase range
			packLeaderController.StopChasing();
		}

		if (targeted) {
			target = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z);
		} else {
			if (Random.value < 0.001) {
				target = new Vector3 (transform.position.x + Random.Range (-idleRange, idleRange), transform.position.y, transform.position.z + Random.Range (-idleRange, idleRange));
				if (packLeader != null && (packLeader.transform.position - transform.position).magnitude > idleRange) {
					target = new Vector3 (packLeader.transform.position.x + Random.Range(-idleRange, idleRange), packLeader.transform.position.y, packLeader.transform.position.z + Random.Range(-idleRange, idleRange));
				}
			}
		}

		agent.SetDestination (target);
	}

	public void StartChasing () {
		targeted = true;
	}

	public bool IsChasing () {
		return targeted;
	}

	public void StopChasing () {
		targeted = false;
		if (packLeader != null) {
			target = new Vector3 (packLeader.transform.position.x + Random.Range (-idleRange, idleRange), packLeader.transform.position.y, packLeader.transform.position.z + Random.Range (-idleRange, idleRange));
		} else {
			target = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		}
		agent.SetDestination (target);
	}


	/*
	// private GameObject player;
	private NavMeshAgent agent;
	private bool targeted;
	private PackController packController;
	private Vector3 target;
	private GameObject player;
	private GameObject enemyLeader;

	public float followDistance;
	public float followAngle;
	public int id;
	public float gap;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
		if (!player) Debug.Log ("Player not tagged");

		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		for (int i = 0; i < enemies.Length; i++) {
			EnemyController enemyController = enemies[i].GetComponent ("EnemyController") as EnemyController;
			if (enemyController.id == 0) {
				enemyLeader = enemies [i];
				break;
			}
		}

		target = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

		// NavMeshAgent does pathfinding around the map automatically
		agent = GetComponent<NavMeshAgent> ();

		packController = transform.parent.gameObject.GetComponent("PackController") as PackController;

		targeted = false;
	}

	void Update() {
		Vector3 targetDir = player.transform.position - transform.position;
		float angle = Vector3.Angle (targetDir, transform.forward);

		// if target is within the angle of vision and within chase range
		if (!targeted && angle < followAngle && targetDir.magnitude <= followDistance) {
			packController.StartChasing();
		} else if (targetDir.magnitude > followDistance && targeted && id == 0) { // target gets out of leader chase range
			packController.StopChasing();
		}

		if (targeted) {
			this.target = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z);
			agent.SetDestination(target);
		} else {
			
			if (id == 0) {
				this.target = new Vector3 (enemyLeader.transform.position.x, enemyLeader.transform.position.y, enemyLeader.transform.position.z);
			} else {
				if (gameObject.transform.position.x < enemyLeader.transform.position.x && gameObject.transform.position.z < enemyLeader.transform.position.z) {
					if ((gameObject.transform.position - enemyLeader.transform.position).magnitude > gap) {
						this.target = new Vector3 (enemyLeader.transform.position.x - id * gap, enemyLeader.transform.position.y, enemyLeader.transform.position.z - id * gap);
					}
				} else if (gameObject.transform.position.x < enemyLeader.transform.position.x && gameObject.transform.position.z >= enemyLeader.transform.position.z) {
					if ((gameObject.transform.position - enemyLeader.transform.position).magnitude > gap) {
						this.target = new Vector3 (enemyLeader.transform.position.x - id * gap, enemyLeader.transform.position.y, enemyLeader.transform.position.z + id * gap);
					}
				} else if (gameObject.transform.position.x >= enemyLeader.transform.position.x && gameObject.transform.position.z < enemyLeader.transform.position.z) {
					if ((gameObject.transform.position - enemyLeader.transform.position).magnitude > gap) {
						this.target = new Vector3 (enemyLeader.transform.position.x + id * gap, enemyLeader.transform.position.y, enemyLeader.transform.position.z - id * gap);
					}
				} else {
					if ((gameObject.transform.position - enemyLeader.transform.position).magnitude > gap) {
						this.target = new Vector3 (enemyLeader.transform.position.x + id * gap, enemyLeader.transform.position.y, enemyLeader.transform.position.z + id * gap);
					}
				}
			}
			/*
			if (id == 0) {
				this.target = new Vector3 (enemyLeader.transform.position.x, enemyLeader.transform.position.y, enemyLeader.transform.position.z);
			} else if (id == 1) {
				this.target = new Vector3 (enemyLeader.transform.position.x + gap, enemyLeader.transform.position.y, enemyLeader.transform.position.z + gap);
			} else if (id == 2) {
				this.target = new Vector3 (enemyLeader.transform.position.x - gap, enemyLeader.transform.position.y, enemyLeader.transform.position.z + gap);
			}

			agent.SetDestination (target);
		}
	}

	public void StartChasing() {
		targeted = true;
		// this.target = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, targetObject.transform.position.z);
	}

	public void StopChasing() {
		targeted = false;
		// this.target = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, targetObject.transform.position.z);
	}

	public bool IsChasing() {
		return targeted;
	}
	*/
}
