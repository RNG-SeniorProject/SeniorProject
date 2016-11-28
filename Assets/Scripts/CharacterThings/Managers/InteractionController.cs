using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionController : MonoBehaviour {
	public Util util;

	private CameraController cam;
	private Animator animator;

	public List<Interactable> interactions;

	void Start () {
		cam = util.camController;

		animator = gameObject.GetComponent<Animator> ();
	}

	void Update () {
		if (util.time.paused) {return;}

		if (interactions.Count <= 0) {
			return;
		}

		if (Input.GetKeyDown ("e")) {
			Interactable temp = interactions [0];
			temp.interact (gameObject);
		}
	}
}
