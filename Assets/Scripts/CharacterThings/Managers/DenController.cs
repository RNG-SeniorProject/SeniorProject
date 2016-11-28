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

	public bool migrate;

	public float birthTime;
	public float starveTime;

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

		if (Hunger <= 0) {
			starveTime += Time.deltaTime;
			if (starveTime > 30) {
				foreach (AllyController ally in util.allyPack.allyControllers) {
					if (!ally.followPlayer) {
						util.allyPack.RemovePackMember (ally.gameObject);
						Destroy (ally.gameObject);

						starveTime = 0;
						break;
					}
				}
			}
		} else {
			birthTime += Time.deltaTime;

			if (birthTime > 90 * population / (currentDen.popCap * 2/3)) {
				birthTime = 0;
				GameObject newAlly = (GameObject)Instantiate (util.packmember, currentDen.transform.position + currentDen.transform.forward*10, Quaternion.identity);
				newAlly.transform.SetParent (util.allyPack.transform);		
				util.allyPack.AddPackMember (newAlly);
			}
		}
	}

	public void changeHunger(float value){
		Hunger = Mathf.Clamp(Hunger + value, 0, currentDen.MaxHunger);

		uiManager.changeDenHunger ();
	}

	public void startMigration (){
		migrate = true;
		util.packCon.Migrate ();
		uiManager.revealMigrateWarning ();
	}
}
