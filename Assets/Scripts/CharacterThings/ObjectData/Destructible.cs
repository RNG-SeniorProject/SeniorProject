using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Destructible : MonoBehaviour {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
	[SerializeField]
	protected Util util;

	[SerializeField]
	private float _maxHealth;
	[SerializeField]
	private float _health;
	[SerializeField]
	private float _healthModBase;
	[SerializeField]
	private float _healthMod;

	[SerializeField]
	private GameObject[] drops;

	private bool _isdead;

	protected Canvas canvas;
	protected Camera cam;
	protected UIManager uiManager;

	public Image healthBar;
	public Image healthSlider;
	public float UITime;

	public float MaxHealth{
		get{ return _maxHealth;}
		set{ _maxHealth = value;}
	}

	public float Health{
		get{ return _health;}
		set{ _health = Mathf.Clamp (value, 0, MaxHealth); }
	}

	public float HealthModBase{
		get{ return _healthModBase;}
		set{ _healthModBase = value;}
	}

	public float HealthMod{
		get{ return _healthMod;}
		set{ _healthMod = value;}
	}

	public bool isDead{
		get{return _isdead;}
		set{ _isdead = value;}
	}

	void Start(){
		Init ();
	}

	protected virtual void Init(){
		Health = MaxHealth;

		canvas = util.canvas;
		cam = util.cam;

		uiManager = util.uiManager;
		UITime = 9999;
	}

	void Update(){
		if (util.time.paused) {return;}

		update ();
	}

	protected virtual void update(){
		changeHealth(HealthModBase * Time.deltaTime, false);
	}

	public virtual void changeHealth(float value, bool visible){
		if (isDead) {return;}

		Health += value;

		uiManager.changeEnemyHealth (this, visible);
		uiManager.changePlayerHealth (this);

		if (Health <= 0) {
			isDead = true;

			uiManager.onEnemyDeath (this);
			Die ();
		}
	}

	protected virtual void Die(){
		Destroy (gameObject);

		foreach (GameObject drop in drops) {
			Instantiate (drop, transform.position, transform.rotation);
		}
	}
}