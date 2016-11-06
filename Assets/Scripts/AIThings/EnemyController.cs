﻿using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

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
		float angle = Vector3.Angle(targetDir, transform.forward);

		// if target is within the angle of vision and within chase range
		if (!targeted && angle < followAngle && targetDir.magnitude <= followDistance) {
			packController.StartChasing();
		} else if (targetDir.magnitude > followDistance && targeted && id == 0) { // target gets out of leader chase range
			packController.StopChasing();
		}

		if (targeted) {
			this.target = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z);
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
		}

		agent.destination = target;
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
}
