using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayerStats : MonoBehaviour {

	public static PlayerStats control;

	//Main base stats
	private float _strength;
	private float _dexterity;
	private float _constitution;
	private float _wisdom;
	private float _charisma;

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

	//Secondary stats
	private float _resistance;
	private float _speed;

	private float _range;
	private float _accuracy;
	private float _radar;

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

	//Make into an enum
	private float _coat;

	public float Coat{
		get{ return _coat;}
		set{ _coat = value;}
	}

	//Volatile stat section

	//Max of volatile stats
	private float _maxHealth;
	private float _maxEnergy;

	//Current value of volatile stats
	public float _health;
	private float _energy;

	private float _hunger;
	private float _thirst;

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

	//Makes sure we only have 1 Player stat that continuous between scenes
	void Awake(){
		if (control == null) {
			DontDestroyOnLoad (gameObject);	
			control = this;
		} else if (control != this){
			Destroy (this);
		}


		MaxHealth = 100;
		Health = 100;
	}

	void Update(){
		//Health -= 1 * Time.deltaTime;
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
class PlayerData{
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