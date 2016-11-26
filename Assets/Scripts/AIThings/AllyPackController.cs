using UnityEngine;
using System.Collections;

public class AllyPackController : MonoBehaviour {

	public Util util;

	public Vector3 idlePos;
	private AllyController[] allyControllers;
	public bool isMigrating;

	void Start () {
		idlePos = util.den.currentDen.transform.position;

		GameObject[] allies = new GameObject[transform.childCount];
		for (int i = 0; i < allies.Length; i++) {
			allies [i] = transform.GetChild (i).gameObject;
		}

		allyControllers = new AllyController[allies.Length];
		for (int i = 0; i < allies.Length; i++) {
			allyControllers[i] = allies [i].GetComponent ("AllyController") as AllyController;
		}

		isMigrating = false;
	}

	void Update () {
		if (isMigrating) {
			if (!CheckMigration ()) {
				idlePos = allyControllers [0].transform.position;
			} else {
				idlePos = util.den.currentDen.transform.position;
			}
		}
	}

	public void AddPackMember (GameObject newPackMember) {
		AllyController[] newAllyControllers = new AllyController[allyControllers.Length + 1];
		for (int i = 0; i < allyControllers.Length; i++) {
			newAllyControllers [i] = allyControllers [i];
		}
		newAllyControllers [newAllyControllers.Length - 1] = newPackMember.GetComponent ("AllyController") as AllyController;

		allyControllers = newAllyControllers;
	}

	public void RemovePackMember (int id) {
		AllyController[] newAllyControllers = new AllyController[allyControllers.Length - 1];
		for (int i = 0; i < id; i++) {
			newAllyControllers [i] = allyControllers [i];
		}
		for (int i = id; i < allyControllers.Length; i++) {
			newAllyControllers [i] = allyControllers [i + 1];
		}
		allyControllers = newAllyControllers;
	}

	public void RemovePackMember (GameObject packMember) {
		AllyController[] newAllyControllers = new AllyController[allyControllers.Length - 1];
		bool removed = false;
		for (int i = 0; i < allyControllers.Length; i++) {
			if (packMember.GetInstanceID() == allyControllers[i].gameObject.GetInstanceID()) {
				removed = true;
				for (int j = i + 1; i < newAllyControllers.Length; i++, j++) {
					newAllyControllers [i] = allyControllers [j];
				}
				break;
			} else {
				newAllyControllers [i] = allyControllers [i];
			}
		}
		if (!removed)
			Debug.Log ("Failed to remove pack member");
		allyControllers = newAllyControllers;
	}

	public bool CheckMigration () {
		GameObject[] predators = GameObject.FindGameObjectsWithTag ("Predator");

		RaycastHit[] hits = Physics.SphereCastAll(idlePos, 15, util.den.currentDen.transform.position - allyControllers[0].transform.position, 30);
		for (int i = 0; i < hits.Length; i++) {
			for (int j = 0; j < predators.Length; j++) {
				if (hits[i].transform.gameObject.GetInstanceID() == predators[j].GetInstanceID()) {
					return false;
				}
			}
		}

		return true;
	}

	public void Migrate () {
		isMigrating = true;
		idlePos = util.den.currentDen.transform.position;
	}
}
