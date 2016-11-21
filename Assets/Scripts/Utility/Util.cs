using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Util : MonoBehaviour{
	public Camera cam;
	public CameraController camController;

	public PlayerStats plr;

	public Canvas canvas;
	public Image plrHealthGui;
	public Image plrEnergyGui;
	public Image plrHungerGui;
	public GameObject enemyHealthPrefab;
}
