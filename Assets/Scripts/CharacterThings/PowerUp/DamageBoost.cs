using UnityEngine;
using System.Collections;

public class DamageBoost : PowerUp {

	public float percIncrease;
	private MeleeAttack attM;
	private RangedAttack attR;

	void Start(){
		Init ();
	}

	protected override void Init(){
		base.Init ();

		attM = util.melee;
		attR = util.ranged;
	}

	public override void powerUp (){
		attM.baseDamage *= percIncrease;
		attR.baseDamage *= percIncrease;

		util.attackbase.text = util.attackbase.text + "+";
	}
}
