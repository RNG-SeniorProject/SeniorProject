using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	public float Health;
	public BoxCollider coll;

	void Start () {
		Health = 100;
		coll = GetComponent<BoxCollider> ();
	}

	public void takeDamage(float amount){
		Health -= amount;

		if (Health <= 0) {
			Die ();
		}
	}

	public void Die(){
		coll.isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
