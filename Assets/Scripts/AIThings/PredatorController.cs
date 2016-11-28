using UnityEngine;
using System.Collections;

public class PredatorController : MonoBehaviour {

	private GameObject player;
	private GameObject packLeader;
	private PredatorLeaderController packLeaderController;
	private Animator animator;
	private NavMeshAgent agent;
	private bool targeted;
	private bool idleWalking;
	private Vector3 target;
	private bool waitingToChase;

	private AudioSource aggressiveAudio;

	public float chaseRange = 10;
	public float visionAngle = 60;
<<<<<<< HEAD
	public float idleRange = 10;
	public float chaseCooldown = 1;
=======
	public float idleRange = 5;
	public float chaseCooldown = 5;
>>>>>>> NataliesJunk
	public float rangeMultiplier = 1;

	void Start () {
		player = GameObject.FindWithTag ("Player");
		if (player == null)
			Debug.Log ("Player not tagged");

		if (transform.parent != null) {
			GameObject[] predators = new GameObject[transform.parent.childCount];
			for (int i = 0; i < predators.Length; i++) {
				predators [i] = transform.parent.GetChild (i).gameObject;
				PredatorLeaderController predatorLeaderController = predators [i].GetComponent ("PredatorLeaderController") as PredatorLeaderController;
				if (predatorLeaderController) {
					packLeader = predatorLeaderController.gameObject;
					packLeaderController = predatorLeaderController;
					break;
				}
			}
			if (packLeader == null)
				Debug.Log ("Pack Leader failed to instantiate [PredatorController]");
		}

		animator = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();

		targeted = false;
		idleWalking = false;
		waitingToChase = false;
		target = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
<<<<<<< HEAD

		enemy = player;
		enemyPos = player.transform.position;

		aggressiveAudio = GetComponent<AudioSource> ();
=======
>>>>>>> NataliesJunk
	}

	void Update () {
		Vector3 targetDir = player.transform.position - transform.position;
		float angle = Vector3.Angle (targetDir, transform.forward);

		// if target is within the angle of vision and within chase range
<<<<<<< HEAD
		if (!targeted && enemy != null && (enemyPos - transform.position).magnitude < chaseRange) {
=======
		if (!targeted && angle < visionAngle && targetDir.magnitude < chaseRange) {
>>>>>>> NataliesJunk
			if (packLeader != null) {
				packLeaderController.StartChasing ();
			} else {
				StartChasing ();
			}
<<<<<<< HEAD
		} else if (targeted && (enemyPos - transform.position).magnitude > (rangeMultiplier * chaseRange)) { // target gets out of leader chase range
			if (packLeader != null && gameObject.GetInstanceID() == packLeader.GetInstanceID()) {
=======
		} else if (targeted && targetDir.magnitude > rangeMultiplier * chaseRange && gameObject.GetInstanceID() == packLeader.GetInstanceID()) { // target gets out of leader chase range
			if (packLeader != null) {
>>>>>>> NataliesJunk
				packLeaderController.StopChasing ();
			} else {
				StopChasing ();
			}
		}

		if (targeted) {
<<<<<<< HEAD
			if (!aggressiveAudio.isPlaying) {
				aggressiveAudio.Play ();
			}
			if (den == null || (den.transform.position - transform.position).magnitude < 100) {
				attackCon.SetTarget (enemy);
				target = new Vector3 (enemyPos.x, enemyPos.y, enemyPos.z);
				agent.SetDestination (target);
				animator.SetFloat ("Speed", 1f);
			} else {
				packLeaderController.StopChasing ();
			}
		} else {
			aggressiveAudio.Stop ();
			if (!disturbed && Random.value < 0.001) {
				StartIdleWalk ();
			}
			if (disturbed) {
				if (!aggressiveAudio.isPlaying) {
					aggressiveAudio.Play ();
				}
				if ((target - transform.position).magnitude < 2.5) {
					aggressiveAudio.Stop ();
					disturbed = false;
					agent.ResetPath ();
					animator.SetFloat ("Speed", 0.0f);
				}
			}
=======
			target = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z);
			agent.SetDestination (target);
			animator.SetFloat ("Speed", 1f);
		} else {
			if (Random.value < 0.001) {
				StartIdleWalk ();
			}
>>>>>>> NataliesJunk
			if (idleWalking) {
				if ((target - transform.position).magnitude < 2.5) {
					StopIdleWalk ();
				}
			}
		}
	}
		
	private void StartIdleWalk () {
		target = new Vector3 (transform.position.x + Random.Range (-idleRange, idleRange), transform.position.y, transform.position.z + Random.Range (-idleRange, idleRange));
		if (packLeader != null && (packLeader.transform.position - transform.position).magnitude > idleRange) {
			target = new Vector3 (packLeader.transform.position.x + Random.Range(-idleRange, idleRange), packLeader.transform.position.y, packLeader.transform.position.z + Random.Range(-idleRange, idleRange));
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

	public void StartChasing () {
		if (!waitingToChase) {
			targeted = true;
		}
	}

	public bool IsChasing () {
		return targeted;
	}

	public void StopChasing () {
		targeted = false;
		StartIdleWalk ();
		waitingToChase = true;
		StartCoroutine ("ChaseCooldown", chaseCooldown);
	}

	IEnumerator ChaseCooldown(float cooldown){
		yield return new WaitForSeconds (cooldown);
		waitingToChase = false;
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
