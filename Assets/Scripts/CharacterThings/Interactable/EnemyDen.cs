using UnityEngine;
using System.Collections;

public class EnemyDen : Interactable {
	public int population;
	public PredatorPackController pack;

	public PowerUp[] pwrUp;

	void Start(){
		Init ();
	}

	public override void Init(){
		base.Init ();

		interactionString = "Capture this den.";
		population = 0;//pack.transform.childCount;
	}

	public override void interact (GameObject chr){
		if (population > 0) {
			uiManager.displayWarning ("Enemies are still around.");
			return;
		}

		convertDen ();
	}

	private void convertDen (){
		gameObject.GetComponent<Den> ().active = true;
		active = false;
		removeFromPlr ();
		gameObject.GetComponent<Den> ().addToPlr ();

		foreach (PowerUp PU in transform.Find("PowerUps").GetComponents<PowerUp>()) {
			PU.powerUp ();

			uiManager.displayWarning ("Your attacks got boosted. Check the pause screen to see what's new.");
		}
	}
}
