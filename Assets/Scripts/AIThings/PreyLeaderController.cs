using UnityEngine;
using System.Collections;

public class PreyLeaderController : MonoBehaviour {

	private PreyHerdController preyHerdController;

	void Start () {
		preyHerdController = transform.parent.GetComponent ("PreyHerdController") as PreyHerdController;
	}

	public void StartFleeing () {
		preyHerdController.StartFleeing ();
	}

	public void StopFleeing () {
		preyHerdController.StopFleeing ();
	}

}
