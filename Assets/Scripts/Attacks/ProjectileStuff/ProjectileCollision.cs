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
		Destructible des = col.gameObject.GetComponent<Destructible> ();

		if (des != null && !(col.transform.tag == atkInfo.myTag || col.transform.tag == atkInfo.tagToIgnore)) {
			des.changeHealth (atkInfo.baseDamage, true);

			foreach (GameObject effect in atkInfo.effects) {
				effect.GetComponent<Effect>().activateEffect (col.gameObject);
			}

			Destroy (gameObject);
		}
	}
}
