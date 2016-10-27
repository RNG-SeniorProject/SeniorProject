using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DOTEffect : Effect {

	[SerializeField]
	protected float damagePerTic;
	[SerializeField]
	protected float tickPerSec;
	[SerializeField]
	protected float duration;
	//[SerializeField]
	//protected List<Util.StatusCondition> statusEffects;

	public override void activateEffect (GameObject chr) {
		StartCoroutine (performEffect (chr));
	}

	IEnumerator performEffect(GameObject chr){
		float timer = 0f;

		while (timer < duration){
			chr.GetComponent<Destructible>().takeDamage (damagePerTic);
			timer += 1/tickPerSec;
			yield return new WaitForSeconds (1/tickPerSec);	
		}	
	}
}
