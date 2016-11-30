using UnityEngine;
using System.Collections;

public class PreyLeaderController : MonoBehaviour {

	private PreyHerdController preyHerdController;

	void Start () {
		preyHerdController = transform.parent.GetComponent ("PreyHerdController") as PreyHerdController;
	}

	public void StartFleeing (Vector3 enemyPos) {
		preyHerdController.StartFleeing (enemyPos);
	}

	public void StopFleeing () {
		preyHerdController.StopFleeing ();
	}

}
