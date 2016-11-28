using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

	public Util util;

	private TimeController time;

	private Canvas canvas;
	private Camera cam;

	private PlayerStats plr;

	private DenController den;

	private Image startScreen;
	private Image pauseScreen;

	public Image migrateWarn;
	public Image starveWarn;

	private Image warningScreen;
	private Text warningText;
	private float warningTime;
	private float warningMaxTime = 4;

	private Image plrHealthBar;
	private Image plrEnergyBar;
	private Image plrHungerBar;

	private Image denHungerBar;

	private Image plrHealthSlider;
	private Image plrEnergySlider;
	private Image plrHungerSlider;

	private Image denHungerSlider;

	private Image interactionUI;

	private GameObject enemyHealthPrefab;
	private Vector3 enemyHealthUIScale;

	public float uiMaxTime;

	void Start(){
		time = util.time;

		canvas = util.canvas;
		cam = util.cam;

		plr = util.plr;

		plrHealthBar = util.plrHealthGui;
		plrHealthSlider = plrHealthBar.transform.Find ("Mask").Find ("Image").GetComponent<Image> ();

		plrEnergyBar = util.plrEnergyGui;
		plrEnergySlider = plrEnergyBar.transform.Find ("Mask").Find ("Image").GetComponent<Image> ();

		plrHungerBar = util.plrHungerGui;
		plrHungerSlider = plrHungerBar.transform.Find ("Mask").Find ("Image").GetComponent<Image> ();

		denHungerBar = util.denHungerGui;
		denHungerSlider = denHungerBar.transform.Find ("Mask").Find ("Image").GetComponent<Image> ();

		enemyHealthPrefab = util.enemyHealthPrefab;
		enemyHealthUIScale = enemyHealthPrefab.GetComponent<Image> ().rectTransform.localScale;

		interactionUI = util.interactionUi;

		den = util.den;

		startScreen = util.startScreen;
		pauseScreen = util.pauseScreen;

		starveWarn = util.starveWarn;
		migrateWarn = util.migrateWarn;

		warningScreen = util.warningScreen;
		warningText = warningScreen.transform.Find ("Text").GetComponent<Text>();
	}

	void Update(){
		if (util.time.paused) {return;}

		if (Input.GetKeyDown ("return")) {
			if (time.paused) {
				unpause ();
			} else {
				pause ();
			}
		}

		warningTime += Time.deltaTime;

		if (warningTime > warningMaxTime) {
			hideWarning ();
		}
	}

	public void initEnemyHealth(Destructible chr){
		GameObject temp = (GameObject)Instantiate (enemyHealthPrefab, chr.transform.position, Quaternion.identity);
		chr.healthBar = temp.GetComponent<Image>();
		chr.healthSlider = chr.healthBar.transform.Find("Mask").Find("Image").GetComponent<Image> ();
	}

	public void changeEnemyHealth(Destructible chr, bool visible){
		if (chr.transform.tag == "Player") { return;}

		if ((chr.transform.position - plr.transform.position).magnitude > 30) {
			return;
		}

		if (chr.healthBar == null) {
			initEnemyHealth (chr);
		}

		if (visible) { 
			chr.UITime = 0;

			chr.healthSlider.fillAmount = chr.Health / chr.MaxHealth;
		}

		updateEnemyHealthLoc (chr);
	}

	public void updateEnemyHealthLoc(Destructible chr){
		if (chr.UITime < uiMaxTime) {
			chr.healthBar.rectTransform.SetParent (canvas.transform, false);

		} else if (chr.UITime >= uiMaxTime || chr.healthBar.rectTransform.position.z >= 0) {
			chr.healthBar.rectTransform.SetParent (chr.transform, false);
		}

		chr.UITime += Time.deltaTime;

		chr.healthBar.rectTransform.localScale = enemyHealthUIScale;
		chr.healthBar.rectTransform.rotation = Quaternion.identity;

		chr.healthBar.rectTransform.position = cam.WorldToScreenPoint (chr.transform.position + new Vector3(0, 2f, 0f)) - new Vector3(chr.healthBar.rectTransform.rect.width/8,0,0);
	}

	public void onEnemyDeath(Destructible chr){
		if (chr.transform.tag == "Player") { return;}
		chr.healthBar.rectTransform.SetParent (chr.transform, false);
	}

	public void initInteractGui(Interactable inter){
		interactionUI.gameObject.SetActive (true);
	}

	public void removeInteractLoc(Interactable inter){
		interactionUI.gameObject.SetActive (false);
	}

	public void changePlayerHealth(Destructible chr){
		if (chr.transform.tag == "Player") {
			plrHealthSlider.fillAmount = plr.Health / plr.MaxHealth;
		}
	}

	public void changePlayerEnergy(Destructible chr){
		if (chr.transform.tag == "Player") {
			plrEnergySlider.fillAmount = plr.Energy / plr.MaxEnergy;
		}
	}

	public void changePlayerHunger(Destructible chr){
		plrHungerSlider.fillAmount = (plr.Hunger /plr.MaxHunger);
	}

	public void changeDenHunger(){
		denHungerSlider.fillAmount = (den.Hunger /den.currentDen.MaxHunger);
	}

	public void updateInteractionText(string text){
		util.interactionUi.transform.Find("Text").GetComponent<Text>().text = "'e': " + text;
	}






	public void startOnClick(){
		time.resume ();
		startScreen.gameObject.SetActive (false);
	}

	public void pause(){
		time.pause ();
		pauseScreen.gameObject.SetActive (true);
	}

	public void unpause(){
		time.resume ();
		pauseScreen.gameObject.SetActive (false);
	}

	public void revealMigrateWarning(){
		migrateWarn.gameObject.SetActive (true);
	}

	public void hideMigrateWarning(){
		migrateWarn.gameObject.SetActive (false);
	}

	public void revealStarveWarning(){
		starveWarn.gameObject.SetActive (true);
	}

	public void hideStarveWarning(){
		starveWarn.gameObject.SetActive (false);
	}

	public void displayWarning(string message){
		warningTime = 0;
		warningScreen.gameObject.SetActive (true);
		warningText.text = message;
	}

	public void hideWarning(){
		warningScreen.gameObject.SetActive (false);
	}
}
