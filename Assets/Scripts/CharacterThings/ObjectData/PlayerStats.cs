using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : CharacterStats {
	public static PlayerStats control;
	public static GameObject plr;

	private Vector3 respawnPos;
	private Vector3 respawnRot;

	private Image hungerBar;
	private Image hungerSlider;

	#region Main Base Stats
	//Main base stats
	private float _strength = 0;
	private float _dexterity = 0;
	private float _constitution = 0;
	private float _wisdom = 0;
	private float _charisma = 0;

	//Main stat properties

	public float Strength{
		get{ return _strength;}
		set{ _strength = value;}
	}

	public float Dexterity{
		get{ return _dexterity;}
		set{ _dexterity = value;}
	}

	public float Constitution{
		get{ return _constitution;}
		set{ _constitution = value;}
	}

	public float Wisdom{
		get{ return _wisdom;}
		set{ _wisdom = value;}
	}

	public float Charisma{
		get{ return _charisma;}
		set{ _charisma = value;}
	}
	#endregion

	#region Secondary Stats
	//Secondary stats
	private float _resistance;
	private float _speed;

	private float _range;
	private float _accuracy;

	//Make into an enum
	private float _coat;

	//Secondary Stat properties
	public float Resistance{
		get{ return _resistance;}
		set{ _resistance = value;}
	}

	public float Speed{
		get{ return _speed;}
		set{ _speed = value;}
	}

	public float Range{
		get{ return _range;}
		set{ _range = value;}
	}

	public float Accuracy{
		get{ return _accuracy;}
		set{ _accuracy = value;}
	}

	public float Coat{
		get{ return _coat;}
		set{ _coat = value;}
	}
    #endregion   

	#region Volatile Stats
	[SerializeField]
	private float MaxHunger;
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
	#endregion

	void Start(){
		Init ();
	}

	protected override void Init () {
		if (control == null) {
			DontDestroyOnLoad (gameObject);	
			control = this;
		} else if (control != this){
			Destroy (this);
		}

		base.Init ();

		hungerBar = util.plrHungerGui;
		hungerSlider = hungerBar.transform.Find("Mask").Find("Image").GetComponent<Image> ();

		Hunger = MaxHunger;

		plr = util.plr.gameObject;

		respawnPos = plr.transform.position;
		respawnRot = plr.transform.eulerAngles;
	}

	void Update(){
		update ();
	}

	protected override void update(){
		base.update ();
		changeHunger (hungerModBase * Time.deltaTime);

		if ((Hunger/MaxHunger) < .25f) {
			takeDamage (-30 * Time.deltaTime, true);
		}

		if (Health <= 0) {
			//respawnPlr ();
		}
	}

	public void changeHunger(float value){
		if (isDead) {return;}

		Hunger += value;

		hungerSlider.fillAmount = Hunger / MaxHunger;
	}

	public void respawnPlr(){
		plr.transform.position = respawnPos;
		plr.transform.eulerAngles = respawnRot;

		Init ();
	}
}