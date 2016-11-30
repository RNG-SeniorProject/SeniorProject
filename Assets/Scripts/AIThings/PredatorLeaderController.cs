using UnityEngine;
using System.Collections;

public class PredatorLeaderController : MonoBehaviour {

	private PredatorPackController predatorPackController;

	void Start () {
		predatorPackController = transform.parent.gameObject.GetComponent ("PredatorPackController") as PredatorPackController;
		if (!predatorPackController)
			Debug.Log ("PredatorPackController not found [PredatorLeaderController]");
	}

	public void StartChasing (GameObject enemy) {
		predatorPackController.StartChasing (enemy);
	}

	public void StopChasing () {
		predatorPackController.StopChasing ();
	}
}
