using UnityEngine;
using System.Collections;

public class PackController : MonoBehaviour {

	private GameObject[] enemies;
	private EnemyController[] enemyControllers;
	private GameObject enemyLeader;

	// Use this for initialization
	void Start () {
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");

		enemyControllers = new EnemyController[enemies.Length];
		for (int i = 0; i < enemies.Length; i++) {
			enemyControllers[i] = enemies[i].GetComponent ("EnemyController") as EnemyController;
		}

		for (int i = 0; i < enemies.Length; i++) {
			if (enemyControllers[i].id == 0) {
				enemyLeader = enemies [i];
			}
		}
	}

	public void StartChasing() {
		for (int i = 0; i < enemies.Length; i++) {
			enemyControllers[i].StartChasing ();
		}
	}

	public void StopChasing() {
		for (int i = 0; i < enemies.Length; i++) {
			enemyControllers[i].StopChasing ();
		}
	}
}
