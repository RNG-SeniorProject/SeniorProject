using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Util : MonoBehaviour{
	public PlayerStats plr;

	public Camera cam;

	public MeleeAttack melee;
	public RangedAttack ranged;

	public CharacterLogic chrLogic;
	public AllyPackController packCon;

	public CameraController camController;
	public InteractionController intController;
	public TimeController time;

	public DenController den;

	public UIManager uiManager;
	public Canvas canvas;
	public Image plrHealthGui;
	public Image plrEnergyGui;
	public Image plrHungerGui;
	public Image interactionUi;

	public Image startScreen;
	public Image pauseScreen;

	public Image migrateWarn;
	public Image starveWarn;

	public Image warningScreen;

	public GameObject enemyHealthPrefab;

	public Image denHungerGui;

	public int plrMeleeDamage;
	public int plrRangedDamage;

	public string plrMeleeBuff;
	public string plrRangedBuff;
}
