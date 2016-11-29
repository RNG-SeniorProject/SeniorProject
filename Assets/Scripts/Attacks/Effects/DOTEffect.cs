using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DOTEffect : Effect {
	private Util util;

	[SerializeField]
	protected float damagePerTic;
	[SerializeField]
	protected float tickPerSec;
	[SerializeField]
	protected float duration;
	//[SerializeField]
	//protected List<Util.StatusCondition> statusEffects;

	void Start(){
		util = GameObject.Find ("GameManager").GetComponent<Util> ();
	}

	public override void activateEffect (GameObject chr) {
		StartCoroutine (performEffect (chr));
	}

	IEnumerator performEffect(GameObject chr){
		float timer = 0f;

		while (timer < duration && chr != null){
			chr.GetComponent<Destructible>().changeHealth (-damagePerTic, true);
			timer += 1/tickPerSec;
			yield return new WaitForSeconds (1/tickPerSec);	
		}	
	}

	public override void changeText(){
		if (util.attackBleed.IsActive ()) {
			util.attackBleed.text = "Your attacks cause a stronger bleed!";
		}

		util.attackBleed.gameObject.SetActive (true);
	}
}
