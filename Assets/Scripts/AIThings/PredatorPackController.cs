using UnityEngine;
using System.Collections;

public class PredatorPackController : MonoBehaviour {

	private PredatorController[] predatorControllers;

	void Start () {
		GameObject[] predators = new GameObject[transform.childCount];
		for (int i = 0; i < predators.Length; i++) {
			predators [i] = transform.GetChild (i).gameObject;
		}

		predatorControllers = new PredatorController[predators.Length];
		for (int i = 0; i < predators.Length; i++) {
			predatorControllers [i] = predators [i].GetComponent ("PredatorController") as PredatorController;
		}
	}

	public void StartChasing (GameObject enemy) {
		for (int i = 0; i < predatorControllers.Length; i++) {
			if (predatorControllers [i].gameObject != null) {
				predatorControllers [i].StartChasing (enemy);
			}
		}
	}

	public void StopChasing () {
		for (int i = 0; i < predatorControllers.Length; i++) {
			if (predatorControllers [i].gameObject != null) {
				predatorControllers [i].StopChasing ();
			}
		}
	}
}
