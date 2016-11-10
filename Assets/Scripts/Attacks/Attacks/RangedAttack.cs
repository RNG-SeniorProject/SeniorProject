using UnityEngine;
using System.Collections;

public class RangedAttack : Attack {

	[SerializeField]
	protected GameObject projectile;
	[SerializeField]
	protected float forwardStr;
	[SerializeField]
	protected CameraController cam;

	public string tagToGet = "Enemy";

	public override void performAttack (GameObject chr) {

		if (onCooldown) {
			return;
		}

		goOnCooldown ();

		if (chr.CompareTag("Enemy")) {
			tagToGet = "Player";
		}

		GameObject arrow = (GameObject)Instantiate(projectile, chr.transform.position + new Vector3(0,1f,0), Quaternion.LookRotation(cam.lookDir, Vector3.up));
		arrow.GetComponent<ProjectileCollision> ().source = chr;
		arrow.GetComponent<ProjectileCollision> ().atkInfo = this;

		arrow.GetComponent<Rigidbody> ().AddForce (arrow.transform.forward * forwardStr);
	}
}
