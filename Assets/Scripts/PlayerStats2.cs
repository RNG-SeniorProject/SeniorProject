using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayerStats2 : MonoBehaviour {

	public static PlayerStats2 control;
	public static GameObject plr;

	private Vector3 respawnPos;
	private Vector3 respawnRot;

	#region Stat Variables/Properties

	#region Main Base Stats
	//Main base stats
	private float _level;

	private float _strength;
	private float _dexterity;
	private float _constitution;
	private float _wisdom;
	private float _charisma;

	//Main stat properties
	public float Level{
		get{ return _level;}
		set{ _level = value;}
	}

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
	//Volatile stat section

	//Some defaults for volatile stats
	[SerializeField]
	private float maxHealthDef;
	[SerializeField]
	private float maxEnergyDef;
	[SerializeField]
	private float maxHungerDef;
	[SerializeField]
	private float maxThirstDef;

	[SerializeField]
	private float healthModDef;
	[SerializeField]
	private float hungerModDef;
	[SerializeField]
	private float thirstModDef;

	//Max of volatile stats
	[SerializeField]
	private float _maxHealth;
	[SerializeField]
	private float _maxEnergy;

	//Current value of volatile stats
	[SerializeField]
	private float _health;
	[SerializeField]
	private float _energy;

	[SerializeField]
	private float _hunger;
	[SerializeField]
	private float _thirst;

	//Overtime modifier values
	private float _healthMod;
	private float _hungerMod;
	private float _thirstMod;

	//Volitile Stat properties
	public float MaxHealth{
		get{ return _maxHealth;}
		set{ _maxHealth = value;}
	}

	public float MaxEnergy{
		get{ return _maxEnergy;}
		set{ _maxEnergy = value;}
	}

	public float Health{
		get{ return _health;}
		set{ 
			if (value > _maxHealth) {
				_health = _maxHealth;
			} else {
				_health = value;
			}
		}
	}

	public float Energy{
		get{ return _energy;}
		set{ 
			if (value > _maxEnergy) {
				_energy = _maxEnergy;
			} else {
				_energy = value;
			}
		}
	}

	public float Hunger{
		get{ return _hunger;}
		set{ _hunger = value;}
	}

	public float Thirst{
		get{ return _thirst;}
		set{ _thirst = value;}
	}

	public float HealthMod{
		get{ return _healthMod;}
		set{ _healthMod = value;}
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

	#endregion

	//Makes sure we only have 1 Player stat that continuous between scenes
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

		plr = GameObject.FindWithTag ("Player").transform.parent.gameObject;
		respawnPos = plr.transform.position;
		respawnRot = plr.transform.eulerAngles;
	}

	void Update(){
		HungerMod = hungerModDef;

		/*
		Hunger -= HungerMod * Time.deltaTime;
		Thirst -= thirstModDef * Time.deltaTime;
	 
		float temp = 0;
		if (Hunger > 81) {
			temp = -.4f;
		} else if (Hunger < 40){
			temp = .8f;
		}

		HealthMod = healthModDef + temp;

		Health -= HealthMod * Time.deltaTime;
		*/

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

	//Saves player data to file
	public void save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

		PlayerData data = new PlayerData ();

		bf.Serialize (file, data);
		file.Close ();
	}

	//Loads player data from a file
	public void load(){
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

			PlayerData data = (PlayerData)bf.Deserialize (file);

			file.Close ();

			//Now update local values with saved values
		}
	}
}

[Serializable]
class PlayerData2{
	//Main base stats
	public float strength;
	public float dexterity;
	public float constitution;
	public float wisdom;
	public float charisma;

	//Secondary stats
	public float resistance;
	public float speed;

	public float range;
	public float accuracy;
	public float radar;

	//Make into an enum
	public float coat;

	//Volatile stat section

	//Max of volatile stats
	public float maxHealth;
	public float maxEnergy;

	//Current value of volatile stats
	public float health;
	public float energy;

	public float hunger;
	public float thirst;
}