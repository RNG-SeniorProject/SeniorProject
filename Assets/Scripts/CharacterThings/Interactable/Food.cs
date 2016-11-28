using UnityEngine;
using System.Collections;

public abstract class Food : Interactable {

	public float use;
	public float hunger;

	public float timeAlive = 0;

	void Start(){
		Init ();
	}

	public override void Init(){
		base.Init ();

		interactionString = "Eat.";
	}

	void Update(){
		timeAlive += Time.deltaTime;

		if (timeAlive > 120) {
			Destroy (this);
		}
	}

	protected abstract void eat (GameObject chr);

	public override void interact (GameObject chr){
		Animator anim = chr.GetComponent<Animator> ();
		anim.SetBool ("Eating", true);
		AudioSource audio = chr.GetComponent<AudioSource> ();
		if (!audio.isPlaying)
			audio.Play ();

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
