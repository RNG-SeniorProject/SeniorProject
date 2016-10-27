using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackController : MonoBehaviour {
	[SerializeField]
	protected List<GameObject> knownAttacks;
	[SerializeField]
	protected List<GameObject> activeAttacks;

	void Update(){
		if (Input.GetMouseButton (0)) {
			activeAttacks [0].GetComponent<Attack> ().performAttack (transform.gameObject);
		} else if (Input.GetMouseButton (1)) {
			activeAttacks [1].GetComponent<Attack> ().performAttack (transform.gameObject);
		}
	}
}