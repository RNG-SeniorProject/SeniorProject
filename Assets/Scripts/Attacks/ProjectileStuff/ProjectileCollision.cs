using UnityEngine;
using System.Collections;

public class ProjectileCollision : MonoBehaviour {

	public GameObject source;
	public RangedAttack atkInfo;
	public float lifetime;

	private float lifeLived = 0;

	void Update(){
		lifeLived += Time.deltaTime;

		if (lifeLived >= lifetime) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider col){

		/*if (col.gameObject.CompareTag (atkInfo.tagToGet)) {
			col.gameObject.GetComponent<CharacterStats> ().takeDamage (atkInfo.baseDamage);

			foreach (GameObject effect in atkInfo.effects) {
				effect.GetComponent<Effect>().activateEffect (col.gameObject);
			}

			Destroy (gameObject);
		}*/

		if (col.gameObject.GetComponent<Destructible>() != null && !col.transform.CompareTag("Player")) {
			col.gameObject.GetComponent<CharacterStats> ().changeHealth (atkInfo.baseDamage, true);

			foreach (GameObject effect in atkInfo.effects) {
				effect.GetComponent<Effect>().activateEffect (col.gameObject);
			}

			Destroy (gameObject);
		}
	}
}
