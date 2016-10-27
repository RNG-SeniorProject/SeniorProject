using UnityEngine;
using System.Collections;

public class Destructible : MonoBehaviour {

	[SerializeField]
	protected float maxHealthDef;
	[SerializeField]
	protected float healthModDef;

	[SerializeField]
	private float _maxHealth;
	[SerializeField]
	private float _health;
	[SerializeField]
	private float _healthMod;
	[SerializeField]
	private bool _isDead;

	[SerializeField]
	protected PickUpable[] drops;
	[SerializeField]
	protected float[] dropRates;

	public float MaxHealth{
		get{ return _maxHealth;}
		set{ _maxHealth = value;}
	}

	public float Health{
		get{ return _health;}
		set{ _health = Mathf.Clamp (value, 0, MaxHealth); }
	}

	public float HealthMod{
		get{ return _healthMod;}
		set{ _healthMod = value;}
	}

	public bool isDead{
		get{ return _isDead;}
		set{ _isDead = value;}
	}

	void Awake(){
		MaxHealth = maxHealthDef;
		Health = MaxHealth;
	}

	void Update(){
		Health += HealthMod * Time.deltaTime;
	}

	public virtual void takeDamage(float value){
		if (isDead) {return;}

		Health -= value;

		if (Health <= 0) {
			Die ();
		}
	}

	protected virtual void Die(){
		isDead = true;
	}

	protected void dropStuff(){
		//Drop stuff
	}
}
