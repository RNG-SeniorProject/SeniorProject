using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LifelinkEffect : Effect {
	private Util util;


	[SerializeField]
	protected float healAmmount;
	//[SerializeField]
	//protected List<Util.StatusCondition> statusEffects;

	void Start(){
		util = GameObject.Find ("GameManager").GetComponent<Util> ();
	}

	public override void activateEffect (GameObject chr) {
		util.plr.changeHealth (healAmmount, true);
	}

	public override void changeText(){
		if (util.attackHeal.IsActive ()) {
			util.attackHeal.text = "Your attacks heal you even more!";
		}

		util.attackHeal.gameObject.SetActive (true);
	}
}
