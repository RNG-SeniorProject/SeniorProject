using UnityEngine;
using System.Collections;

public class RangedAttack : Attack {

	public override void performAttack (GameObject chr) {

		if (onCooldown) {
			return;
		}

		goOnCooldown ();

		string tagToGet = "Enemy";

		if (chr.CompareTag("Enemy")) {
			tagToGet = "Player";
		}


	}
}
