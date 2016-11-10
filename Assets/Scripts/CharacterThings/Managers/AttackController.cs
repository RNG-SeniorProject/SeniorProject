using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackController : MonoBehaviour {
	[SerializeField]
	protected List<GameObject> knownAttacks;
	[SerializeField]
	protected List<GameObject> activeAttacks;

	[SerializeField]
	protected CameraController cam;

	void Update(){
		if (Input.GetMouseButton (0)) {
			if (activeAttacks.Count != 0) {
				if (cam.state == CameraController.CamState.Follow) {
					activeAttacks [0].GetComponent<Attack> ().performAttack (transform.gameObject);
				} else {
					activeAttacks [1].GetComponent<Attack> ().performAttack (transform.gameObject);
				}
			}
		}
	}
}