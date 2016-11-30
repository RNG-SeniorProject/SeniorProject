using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllyController : MonoBehaviour {
	public Util util;

	private AllyPackController packCon;
	private AllyAttackController attackCon;
	private DenController den;

	private GameObject player;
	private Animator animator;
	private NavMeshAgent agent;
	private Vector3 target;
	private bool idleWalking;
	private bool waitingToChase;

	List<GameObject> enemiesInRange;
	private GameObject enemy;
	private string tagToIgnore;
	private string myTag;

	public bool followPlayer;

	public float idleRange = 10;
	public float visionAngle = 60;
	public int chaseCooldown = 1;

	void Start () {
		util = GameObject.Find ("GameManager").GetComponent<Util> ();

		player = GameObject.FindWithTag ("Player");
		if (player == null)
			Debug.Log ("Player not tagged");

		packCon = util.packCon;
		attackCon = gameObject.GetComponent ("AllyAttackController") as AllyAttackController;
		den = util.den;

		animator = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();

		target = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

		idleWalking = false;
		waitingToChase = false;

		myTag = gameObject.tag;
		tagToIgnore = "Player";

		StartIdleWalk ();
	}

	void Update () {
		if (followPlayer) {
			target = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z);
			if ((transform.position - target).magnitude > idleRange / 2) {
				agent.SetDestination (target);
				animator.SetFloat ("Speed", 1.0f);
			} else {
				agent.ResetPath ();
				animator.SetFloat ("Speed", 0.0f);
			}

			if (enemy != null) {
				if ((player.transform.position - transform.position).magnitude < 2 * idleRange) {
					target = new Vector3 (enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
					agent.SetDestination (target);
					animator.SetFloat ("Speed", 1.0f);
				} else {
					enemy = null;
					attackCon.RemoveTarget ();
					target = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z);
					agent.SetDestination (target);
					animator.SetFloat ("Speed", 1.0f);
					waitingToChase = true;
					StartCoroutine ("ChaseCooldown", chaseCooldown);
				}
			} else {
				enemiesInRange = new List<GameObject> ();
				Collider[] enemies = Physics.OverlapSphere (player.transform.position, 15);

				foreach (Collider hit in enemies) {
					if (hit.gameObject.GetComponent<Destructible> () != null) {
						if (hit.gameObject.tag != myTag && hit.gameObject.tag != tagToIgnore) {
							float angle = Vector3.Angle (hit.gameObject.transform.position - transform.position, transform.forward);
							if (Mathf.Abs(angle) < visionAngle) {
								enemiesInRange.Add (hit.gameObject);
							}
						}
					}
				}

				if (enemiesInRange.Count > 0) {
					GameObject newEnemy = enemiesInRange [Random.Range (0, enemiesInRange.Count)];
					enemy = newEnemy;
					attackCon.SetTarget (enemy);
					target = new Vector3 (enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
					agent.SetDestination (target);
					animator.SetFloat ("Speed", 1.0f);
				}
			}
		} else {
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
	}

	public void StartIdleWalk () {
		target = new Vector3 (transform.position.x + Random.Range (-idleRange, idleRange), transform.position.y, transform.position.z + Random.Range (-idleRange, idleRange));
		if (((packCon.idlePos + ((idleRange) * den.currentDen.transform.right)) - transform.position).magnitude > idleRange) {
			target = new Vector3 (packCon.idlePos.x + Random.Range (-idleRange, idleRange), packCon.idlePos.y, packCon.idlePos.z + Random.Range (-idleRange, idleRange)) + 2 * idleRange * den.currentDen.transform.right;
		} else {
			if (packCon.idlePos == util.den.currentDen.transform.position) {
				packCon.isMigrating = false;
				util.den.migrate = false;
				util.den.Hunger = util.den.currentDen.MaxHunger;
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

	IEnumerator ChaseCooldown(float cooldown){
		yield return new WaitForSeconds (cooldown);
		waitingToChase = false;
	}

	public void SwapFollowMode () {
		if (followPlayer)
			followPlayer = false;
		else
			followPlayer = true;
	}

	public void Migrate () {
		target = new Vector3 (packCon.idlePos.x + Random.Range(-idleRange, idleRange), packCon.idlePos.y, packCon.idlePos.z + Random.Range(-idleRange, idleRange));
		agent.SetDestination (target);
		animator.SetFloat ("Speed", 0.5f);
	}
}
