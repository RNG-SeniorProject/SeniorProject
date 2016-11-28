using UnityEngine;
using System.Collections;

public class DenController : MonoBehaviour {
	public Util util;

	public UIManager uiManager;

	public Den currentDen;

	public float population;

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
		//Change hunger
		if (migrate){ return;}

		changeHunger(HungerModBase * population * Time.deltaTime);

<<<<<<< HEAD
		/*if (population > currentDen.popCap) {
=======
		if (population > currentDen.lifeSupport) {
>>>>>>> NataliesJunk
			panicDying = true;
		} else {
			panicDying = false;
<<<<<<< HEAD
			uiManager.hideStarveWarning ();
		}*/
=======
		}
>>>>>>> NataliesJunk

		if (Hunger <= currentDen.MaxHunger * .15) {
			panicLowFood = true;
		} else {
			panicLowFood = false;
		}
	}

	public void changeHunger(float value){
		Hunger += value;

		uiManager.changeDenHunger ();
	}

	public void startMigration (){
		migrate = true;
		util.packCon.Migrate ();
	}
}
