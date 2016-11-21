using UnityEngine;
using System.Collections;

public abstract class Food : Interactable {

	public float use;
	public float hunger;

	protected abstract void eat (GameObject chr);

	public override void interact (GameObject chr){
		if (use <= 0) {
			Destroy (gameObject);
			return;
		}
		eat (chr);
		use--;
	}
}
