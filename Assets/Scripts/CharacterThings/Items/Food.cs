using UnityEngine;
using System.Collections;

public abstract class Food : Interactable {

	public float use;
	public float hunger;

	protected abstract void eat (GameObject chr);

	public override void interact (GameObject chr, Animator animator){
		animator.SetBool ("Eating", true);

		StartCoroutine (delayedWait("Eating", 1, animator, chr));
	}

	IEnumerator delayedWait(string anim, float time, Animator animator, GameObject chr){
		yield return new WaitForSeconds (1);

		eat (chr);
		use--;

		if (use <= 0) {
			removeFromPlr ();
			Destroy (gameObject);
		}

		animator.SetBool (anim, false);
	}
}
