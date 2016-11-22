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

	public override bool performAttack (GameObject chr) {

		if (!base.performAttack (chr)) {
			return false;
		}

		if (chr.CompareTag("Enemy")) {
			tagToGet = "Player";
		}

		StartCoroutine (createProjectile(.75f, chr));

		return true;
	}

	IEnumerator createProjectile(float time, GameObject chr){
		yield return new WaitForSeconds (time);

		GameObject arrow = (GameObject)Instantiate (projectile, chr.transform.position + new Vector3 (0, .4f, 0), Quaternion.LookRotation (cam.lookDir, Vector3.up));
		arrow.GetComponent<ProjectileCollision> ().source = chr;
		arrow.GetComponent<ProjectileCollision> ().atkInfo = this;

		arrow.GetComponent<Rigidbody> ().AddForce (arrow.transform.forward * forwardStr);
	}
}
