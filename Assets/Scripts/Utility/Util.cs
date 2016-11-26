using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Util : MonoBehaviour{
	public PlayerStats plr;

	public Camera cam;

	public CharacterLogic chrLogic;
	public AllyPackController packCon;

	public CameraController camController;
	public InteractionController intController;

	public DenController den;

	public UIManager uiManager;
	public Canvas canvas;
	public Image plrHealthGui;
	public Image plrEnergyGui;
	public Image plrHungerGui;
	public Image interactionUi;

	public GameObject enemyHealthPrefab;

	public Image denHungerGui;
}
