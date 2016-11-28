using UnityEngine;
using System.Collections;

public class PredatorPackController : MonoBehaviour {

	private PredatorController[] predatorControllers;
	private GameObject den;

	void Start () {
		den = transform.parent.gameObject;

		GameObject[] predators = new GameObject[transform.childCount];
		for (int i = 0; i < predators.Length; i++) {
			predators [i] = transform.GetChild (i).gameObject;
		}

		predatorControllers = new PredatorController[predators.Length];
		for (int i = 0; i < predators.Length; i++) {
			predatorControllers [i] = predators [i].GetComponent ("PredatorController") as PredatorController;
		}
	}

	/* public GameObject GetDen () {
		return den;
	} */

	public void StartChasing () {
		for (int i = 0; i < predatorControllers.Length; i++) {
			if (predatorControllers [i].gameObject != null) {
				predatorControllers [i].StartChasing ();
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
