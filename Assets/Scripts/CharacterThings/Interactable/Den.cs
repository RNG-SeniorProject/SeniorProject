using UnityEngine;
using System.Collections;

public class Den : Interactable {
	private DenController denController;

	public float popCap;
	public float foodSpawn;

	public float MaxHunger;

	void Start(){
		Init ();
	}

	public override void Init(){
		base.Init ();
		denController = util.den;

		interactionString = "Migrate to this den.";

		if (denController.currentDen == this){
			interactionString = "Feed pack.";
		}
	}

	public override void interact (GameObject chr){
		denController = util.den;

		if (denController.migrate) {
			uiManager.displayWarning ("You can't while migrating.");
			return;
		}

		if (this == denController.currentDen) {
			feedFamily (chr);
		} else {
			startTravel (chr);
		}
	}

	private void feedFamily(GameObject chr){
		PlayerStats plr = chr.GetComponent<PlayerStats> ();

		if (plr.Hunger <= plr.MaxHunger / 10) {return;}

		plr.changeHunger (-plr.MaxHunger/10);
		denController.changeHunger (denController.Hunger/10);
	}

	private void startTravel(GameObject chr){
		uiManager = util.uiManager;
		denController.currentDen.interactionString = "Migrate to this den.";
		denController.currentDen = this;
		denController.startMigration ();
		interactionString = "Feed pack.";
		uiManager.updateInteractionText (interactionString);
	}
}
