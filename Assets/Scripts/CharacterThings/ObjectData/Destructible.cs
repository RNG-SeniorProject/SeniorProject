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

	private Canvas canvas;
	private Camera cam;

	private Image healthBar;
	private Image healthSlider;

	private Vector3 UIScale;

	private float UITime = 0;
	private float UIMax = 3;

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

		if (transform.tag == "Player") {
			healthBar = util.plrEnergyGui;
			healthSlider = healthBar.transform.Find("Mask").Find("Image").GetComponent<Image> ();
		}
	}

	void Update(){
		update ();
	}

	protected virtual void update(){
		takeDamage(HealthModBase * Time.deltaTime, false);

		if (transform.tag != "Player")
		if (healthBar != null)
		if (healthBar.rectTransform.parent == canvas.transform) {
			UITime += Time.deltaTime;

			healthBar.rectTransform.localScale = UIScale;
			healthBar.rectTransform.rotation = Quaternion.identity;

			healthBar.rectTransform.position = cam.WorldToScreenPoint (transform.position + new Vector3(0, 2f, 0f)) - new Vector3(healthBar.rectTransform.rect.width/8,0,0);

			healthSlider.fillAmount = Health / MaxHealth;

			if (UITime >= UIMax || healthBar.rectTransform.position.z <= 0) {
				UITime = 0;
				healthBar.rectTransform.SetParent (transform, false);
			}
		}
	}

	public virtual void takeDamage(float value, bool visible){
		if (isDead) {return;}

		Health += value;

		if (visible) {
			if (transform.tag != "Player") {
				if (healthBar == null){
					GameObject temp = (GameObject)Instantiate (util.enemyHealthPrefab, transform.position, Quaternion.identity);
					healthBar = temp.GetComponent<Image>();
					healthSlider = healthBar.transform.Find("Mask").Find("Image").GetComponent<Image> ();

					UIScale = healthBar.rectTransform.localScale;
				}

				UITime = 0;

				healthBar.rectTransform.SetParent (canvas.transform, false);
				healthBar.rectTransform.localScale = UIScale;

				healthBar.rectTransform.position = cam.WorldToScreenPoint (transform.position);
				healthSlider.fillAmount = Health / MaxHealth;
			}
		}

		if (transform.tag == "Player") {
			healthSlider.fillAmount = Health / MaxHealth;
		}

		if (Health <= 0) {
			isDead = true;

			if (transform.tag != "Player") {
				healthBar.rectTransform.SetParent (transform, false);
			}

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