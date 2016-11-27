using UnityEngine;
using System.Collections;

public class PredatorLeaderController : MonoBehaviour {

	private PredatorPackController predatorPackController;

	void Start () {
		predatorPackController = transform.parent.GetComponent ("PredatorPackController") as PredatorPackController;
	}

	public GameObject GetDen () {
		return predatorPackController.GetDen ();
	}

	public void StartChasing () {
		predatorPackController.StartChasing ();
	}

	public void StopChasing () {
		predatorPackController.StopChasing ();
	}
}
