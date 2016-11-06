using UnityEngine;
using System.Collections;

public abstract class Interactable : MonoBehaviour {

	public float test;

	public abstract void interact (GameObject chr);

	void OnTriggerEnter (Collider hit){
		if (hit.gameObject.GetComponent<InteractionController> ()!= null) {
			hit.gameObject.GetComponent<InteractionController> ().interactions.Add (this.gameObject);
		}
	}

	void onTriggerExit (Collider hit){
		if (hit.gameObject.GetComponent<InteractionController> () != null) {
			hit.gameObject.GetComponent<InteractionController> ().interactions.Remove (this.gameObject);
		}
	}
}