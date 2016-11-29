using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : CharacterStats {
	public static PlayerStats control;
	public static GameObject plr;

	private Vector3 respawnPos;
	private Vector3 respawnRot;

	[SerializeField]
	public float MaxHunger;
	[SerializeField]
	private float hungerModBase;

	[SerializeField]
	private float _hunger;
	[SerializeField]
	private float _hungerMod;

	public float Hunger{
		get{ return _hunger;}
		set{ _hunger = Mathf.Clamp(value, 0, 1000);}
	}

	public float HungerMod{
		get{ return _hungerMod;}
		set{ _hungerMod = value;}
	}

	void Start(){
		Init ();
	}

	protected override void Init () {
		/*if (control == null) {
			DontDestroyOnLoad (gameObject);	
			control = this;
		} else if (control != this){
			Destroy (this);
		}*/

		base.Init ();

		Hunger = MaxHunger;

		plr = util.plr.gameObject;

		respawnPos = plr.transform.position;
		respawnRot = plr.transform.eulerAngles;
	}

	void Update(){
		if (util.time.paused) {return;}

		update ();
	}

	protected override void update(){
		base.update ();
		changeHunger (hungerModBase * Time.deltaTime);

		if ((Hunger/MaxHunger) < .25f) {
			changeHealth (-10 * Time.deltaTime, true);
		} else if (Hunger/MaxHunger > .8f){
			changeHealth (5 * Time.deltaTime, true);
		}

		if (Health <= 0) {
			//respawnPlr ();
		}
	}

	public void changeHunger(float value){
		if (isDead) {return;}

		Hunger += value;

		uiManager.changePlayerHunger (this);
	}

	public void respawnPlr(){
		plr.transform.position = respawnPos;
		plr.transform.eulerAngles = respawnRot;

		Init ();
	}

	protected override void Die () {
		util.uiManager.gameOver ();
		Destroy (gameObject);
	}
}