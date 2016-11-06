using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionController : MonoBehaviour {

	public List<GameObject> interactions;

	void Start () {
	
	}

	void Update () {
		if (interactions.Count <= 0) {
			return;
		}

		//print (interactions [0]);

		if (Input.GetKeyDown ("e")) {
			GameObject temp = interactions [0];
			temp.GetComponent<Interactable>().interact (gameObject);
			//interactions.RemoveAt (0);
			//Destroy (interactions [0]);
		}
	}
}
