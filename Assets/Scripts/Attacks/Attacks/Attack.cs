using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Attack: MonoBehaviour {
	[SerializeField]
	public float baseDamage;
	[SerializeField]
	protected float baseCooldown;
	[SerializeField]
	protected float energyCost;
	[SerializeField]
	protected bool cooldown;

	[SerializeField]
	public List<GameObject> effects;

	[SerializeField]
	protected bool onCooldown;

	protected void goOnCooldown(){
		onCooldown = true;

		StartCoroutine ("goOffCooldown", baseCooldown);
	}

	IEnumerator goOffCooldown(float cooldown){
		yield return new WaitForSeconds (cooldown);
		onCooldown = false;
	}

	public abstract void performAttack (GameObject chr);

}