using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Attack: MonoBehaviour {
	public string animationType;

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

	public bool useEnergy(GameObject chr){
		return chr.GetComponent<CharacterStats>().useEnergy (-energyCost);
	}

	public virtual bool performAttack (GameObject chr){
		if (onCooldown) {
			return false;
		}

		if (!useEnergy(chr)) {
			return false;
		}

		goOnCooldown ();

		return true;
	}

}