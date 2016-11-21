using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackController : MonoBehaviour {
	public Util util;

	[SerializeField]
	protected List<GameObject> knownAttacks;
	[SerializeField]
	protected List<GameObject> activeAttacks;

	private CameraController cam;

	void Start(){
		cam = util.camController;
	}

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