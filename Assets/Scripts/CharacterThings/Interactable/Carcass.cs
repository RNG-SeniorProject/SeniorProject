using UnityEngine;
using System.Collections;

public class Carcass : Food {

	protected override void eat(GameObject chr){
		chr.GetComponent<PlayerStats> ().changeHunger(hunger);
	}
}
