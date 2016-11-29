using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Den : Interactable {
	private DenController denController;

	public float popCap;
	public float foodSpawn;

	public List<GameObject> herds;

	public float spawnTime = 1000;

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

		spawnTime = 1000;
	}

	void Update(){
		spawnTime += Time.deltaTime;

		if (herds.Count == 0) {
			spawnTime += Time.deltaTime * 2;
		}

		if (spawnTime > 60) {
			util.preySpawn.spawnNearPlayerDen (this);
		}

		foreach (GameObject herd in herds) {
			if (herd.transform.childCount == 0) {
				removeHerd (herd);
			}
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

		if (plr.Hunger <= plr.MaxHunger * .1f) {return;}

		plr.changeHunger (-plr.MaxHunger * .025f);
		denController.changeHunger (MaxHunger/10);
	}

	private void startTravel(GameObject chr){
		uiManager = util.uiManager;
		denController.currentDen.interactionString = "Migrate to this den.";
		denController.currentDen = this;
		denController.startMigration ();
		interactionString = "Feed pack.";
		uiManager.updateInteractionText (interactionString);
	}

	public void addHerd(GameObject herd){
		herds.Add (herd);
		spawnTime = 0;

	}

	public void removeHerd(GameObject herd){
		herds.Remove (herd);
	}
}
