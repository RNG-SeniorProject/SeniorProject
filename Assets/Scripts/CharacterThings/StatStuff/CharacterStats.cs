using UnityEngine;
using System.Collections;

public class CharacterStats : Destructible {
	private float _level;

	[SerializeField]
	protected float maxEnergyDef;
	[SerializeField]
	private float _maxEnergy;
	[SerializeField]
	private float _energy;

	public float Level{
		get{ return _level;}
		set{ _level = value;}
	}

	public float MaxEnergy{
		get{ return _maxEnergy;}
		set{ _maxEnergy = value;}
	}

	public float Energy{
		get{ return _energy;}
		set{ _energy = Mathf.Clamp (value, 0, MaxEnergy); }
	}

	void Awake(){
		MaxHealth = maxHealthDef;
		Health = MaxHealth;

		MaxEnergy = maxEnergyDef;
		Energy = MaxEnergy;
	}
}
