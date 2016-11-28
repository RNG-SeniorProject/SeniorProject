using UnityEngine;
using System.Collections;

public abstract class Food : Interactable {

	public float use;
	public float hunger;

	void Start(){
		Init ();
	}

	public override void Init(){
		base.Init ();

		interactionString = "Eat.";
	}

	protected abstract void eat (GameObject chr);

	public override void interact (GameObject chr){
		Animator anim = chr.GetComponent<Animator> ();
		anim.SetBool ("Eating", true);

		StartCoroutine (delayedWait("Eating", 1, anim, chr));
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
