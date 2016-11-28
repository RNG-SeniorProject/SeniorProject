using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeAttack: Attack {
	[SerializeField]
	private float reach;
	[SerializeField]
	private float sweep;

	protected List<GameObject> enemiesInRange;

	public string tagToIgnore;
	private string myTag;

	AudioSource attackSound;

	void Start(){
		myTag = gameObject.transform.parent.parent.gameObject.tag;
		attackSound = GetComponent<AudioSource> ();
	}

	public override bool performAttack(GameObject chr){
		if (!base.performAttack (chr)) {
			return false;
		}

		attackSound.Play ();

		enemiesInRange = new List<GameObject> ();

		Collider[] enemies = Physics.OverlapSphere(chr.transform.position, reach);

		/*foreach (Collider hit in enemies) {
			if (hit.gameObject.CompareTag(tagToGet)) {
				enemiesInRange.Add (hit.gameObject);
			}
		}*/

		foreach (Collider hit in enemies) {
			if (hit.gameObject.GetComponent<Destructible>() != null) {
				if (hit.gameObject.tag != myTag && hit.gameObject.tag != tagToIgnore) {
					enemiesInRange.Add (hit.gameObject);
				}
			}
		}

		foreach (GameObject enemy in enemiesInRange) {
			Vector3 plrToEnemy = enemy.transform.position - chr.transform.position;

			Debug.DrawRay (chr.transform.position, plrToEnemy, Color.black, 2f);
			Debug.DrawRay (chr.transform.position, chr.transform.forward, Color.black, 2f);

			if (Vector3.Dot (chr.transform.forward.normalized, plrToEnemy.normalized) >= sweep) {
				Destructible stats = enemy.GetComponent<Destructible> ();

				PredatorController pred = enemy.GetComponent<PredatorController> ();
				PreyController prey = enemy.GetComponent<PreyController> ();
				if (pred != null) {
					pred.OnHit (chr);
				}
				if (prey != null) {
					prey.OnHit (chr);
				}

				stats.changeHealth (-baseDamage, true);
			}

			foreach (GameObject effect in effects) {
				effect.GetComponent<Effect>().activateEffect (enemy);
			}
		}

		return true;
	}
}
