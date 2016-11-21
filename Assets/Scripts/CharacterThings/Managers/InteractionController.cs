using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionController : MonoBehaviour {
	public Util util;

	private CameraController cam;
	private Animator animator;

	public List<GameObject> interactions;

	void Start () {
		cam = util.camController;

		animator = gameObject.GetComponent<Animator> ();
	}

	void Update () {
		if (interactions.Count <= 0) {
			return;
		}

		if (Input.GetKeyDown ("e")) {
			GameObject temp = interactions [0];
			temp.GetComponent<Interactable>().interact (gameObject, animator);
			//interactions.RemoveAt (0);
			//Destroy (interactions [0]);
		}
	}
}
