using UnityEngine;
using System.Collections;

public class AddEffect : PowerUp {

	public Effect effectM;
	public Effect effectR;

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
		if (effectM != null) {
			effectM.transform.SetParent (attM.transform, true);
			attM.effects.Add (effectM.gameObject);
		}

		if (effectR != null) {
			effectR.transform.SetParent (attR.transform, true);
			attR.effects.Add (effectR.gameObject);
		}
	}
}
