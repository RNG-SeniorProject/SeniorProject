using UnityEngine;
using System.Collections;

public class DenController : MonoBehaviour {
	public Util util;

	public UIManager uiManager;

	public Den currentDen;

	public int population;

	public GameObject[] individuals;

	public float Hunger;
	public float HungerModBase;

	public bool panicDying;
	public bool panicLowFood;

	public bool migrate;

	void Start(){
		//Set up anything we need
		uiManager = util.uiManager;

		Hunger = currentDen.MaxHunger;
	}

	void Update(){
		if (util.time.paused) {return;}

		//Change hunger
		if (migrate){ return;}

		changeHunger(HungerModBase * population * Time.deltaTime);

		if (population > currentDen.popCap) {
			panicDying = true;
			uiManager.revealStarveWarning ();
		} else {
			panicDying = false;
			uiManager.hideStarveWarning ();
		}
	}

	public void changeHunger(float value){
		Hunger += value;

		uiManager.changeDenHunger ();
	}

	public void startMigration (){
		migrate = true;
		util.packCon.Migrate ();
		uiManager.revealMigrateWarning ();
	}
}
