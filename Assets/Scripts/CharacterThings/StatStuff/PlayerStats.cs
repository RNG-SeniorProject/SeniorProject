using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : CharacterStats {
	public static PlayerStats control;
	public static GameObject plr;

	private Vector3 respawnPos;
	private Vector3 respawnRot;

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
	private float _radar;

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

	public float Radar{
		get{ return _radar;}
		set{ _radar = value;}
	}

	public float Coat{
		get{ return _coat;}
		set{ _coat = value;}
	}
    #endregion   

	#region Volatile Stats
	[SerializeField]
	private float maxHungerDef;
	[SerializeField]
	private float maxThirstDef;

	[SerializeField]
	private float hungerModDef;
	[SerializeField]
	private float thirstModDef;

	[SerializeField]
	private float _hunger;
	[SerializeField]
	private float _thirst;

	private float _hungerMod;
	private float _thirstMod;

	public float Hunger{
		get{ return _hunger;}
		set{ _hunger = Mathf.Clamp(0, 1000, value);}
	}

	public float Thirst{
		get{ return _thirst;}
		set{ _thirst = Mathf.Clamp(0, 1000, value);}
	}

	public float HungerMod{
		get{ return _hungerMod;}
		set{ _hungerMod = value;}
	}

	public float ThirstMod{
		get{ return _thirstMod;}
		set{ _thirstMod = value;}
	}
	#endregion

	void Awake(){
		if (control == null) {
			DontDestroyOnLoad (gameObject);	
			control = this;
		} else if (control != this){
			Destroy (this);
		}

		MaxHealth = maxHealthDef;
		Health = MaxHealth;

		MaxEnergy = maxEnergyDef;
		Energy = MaxEnergy;
		Hunger = maxHungerDef;
		Thirst = maxThirstDef;

		plr = GameObject.FindWithTag ("Player");

		respawnPos = plr.transform.position;
		respawnRot = plr.transform.eulerAngles;
	}

	void Update(){
		Hunger += hungerModDef;

		if (Health <= 0) {
			respawnPlr ();
		}
	}

	public void respawnPlr(){
		plr.transform.position = respawnPos;
		plr.transform.eulerAngles = respawnRot;

		MaxHealth = maxHealthDef;
		Health = MaxHealth;

		MaxEnergy = maxEnergyDef;
		Energy = MaxEnergy;
		Hunger = maxHungerDef;
		Thirst = maxThirstDef;
	}
}