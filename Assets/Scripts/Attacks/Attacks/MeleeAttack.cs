using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeAttack: Attack {
	[SerializeField]
	private float reach;
	[SerializeField]
	private float sweep;

	protected List<GameObject> enemiesInRange;

	public override void performAttack(GameObject chr){
		enemiesInRange = new List<GameObject> ();

		if (onCooldown) {
			return;
		}

		goOnCooldown ();

		string tagToGet = "Enemy";

		if (chr.CompareTag("Enemy")) {
			tagToGet = "Player";
		} 

		Collider[] enemies = Physics.OverlapSphere(chr.transform.position, reach);

		foreach (Collider hit in enemies) {
			if (hit.gameObject.CompareTag(tagToGet)) {
				enemiesInRange.Add (hit.gameObject);
			}
		}

		foreach (GameObject enemy in enemiesInRange) {
			Vector3 plrToEnemy = enemy.transform.position - chr.transform.position;

			Debug.DrawRay (chr.transform.position, plrToEnemy, Color.black, 2f);
			Debug.DrawRay (chr.transform.position, chr.transform.forward, Color.black, 2f);

			if (Vector3.Dot (chr.transform.forward.normalized, plrToEnemy.normalized) >= sweep) {
				Destructible stats = enemy.GetComponent<Destructible> ();

				stats.takeDamage (baseDamage);
			}

			foreach (GameObject effect in effects) {
				effect.GetComponent<Effect>().activateEffect (enemy);
			}
		}
	}
}
