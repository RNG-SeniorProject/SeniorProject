using UnityEngine;
using UnityEngine.UI;
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
		population = pack.transform.childCount;
	}

	public override void interact (GameObject chr){
		if (pack.transform.childCount > 0) {
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

		util.allyPack.allyCap++;
		util.packSize.transform.Find ("Max").GetComponent<Text> ().text = util.allyPack.allyCap.ToString();

		foreach (PowerUp PU in transform.Find("PowerUps").GetComponents<PowerUp>()) {
			PU.powerUp ();

			uiManager.displayWarning ("Your attacks got boosted. Check the pause screen to see what's new.");
		}
	}
}
