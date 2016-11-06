using UnityEngine;
using System.Collections;

public class RangedAttack : Attack {

	[SerializeField]
	protected GameObject projectile;
	[SerializeField]
	protected float forwardStr;

	public string tagToGet = "Enemy";

	public override void performAttack (GameObject chr) {

		if (onCooldown) {
			return;
		}

		goOnCooldown ();

		if (chr.CompareTag("Enemy")) {
			tagToGet = "Player";
		}

		GameObject arrow = (GameObject)Instantiate(projectile, chr.transform.position + new Vector3(0,1f,0), chr.transform.rotation);
		arrow.GetComponent<ProjectileCollision> ().source = chr;
		arrow.GetComponent<ProjectileCollision> ().atkInfo = this;

		arrow.GetComponent<Rigidbody> ().AddForce (transform.forward * forwardStr);
	}
}
