using UnityEngine;
using System.Collections;

public class AllyPackController : MonoBehaviour {

	private AllyController[] allyControllers;

	void Start () {
		GameObject[] allies = new GameObject[transform.childCount];
		for (int i = 0; i < allies.Length; i++) {
			allies [i] = transform.GetChild (i).gameObject;
		}

		allyControllers = new AllyController[allies.Length];
		for (int i = 0; i < allies.Length; i++) {
			allyControllers[i] = allies [i].GetComponent ("AllyController") as AllyController;
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

	public void Migrate (GameObject newDen) {
		for (int i = 0; i < allyControllers.Length; i++) {
			allyControllers [i].Migrate (newDen);
		}
	}
}
