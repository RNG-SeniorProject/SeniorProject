using UnityEngine;
using System.Collections;

public class HomewardOrb : MonoBehaviour {

	public Util util;

	public GameObject orb;
	public Vector3 offset;

	private PlayerStats plr;
	private DenController den;
	private bool visible = false;


	void Start () {
		plr = util.plr;
		den = util.den;
	}

	void Update () {

		if (Input.GetKeyDown("q")) {
			if (visible) {
				visible = false;
			} else if (!visible) {
				visible = true;
				orb.transform.position = plr.transform.position + offset;
			}

			orb.SetActive (visible);
		}

		if (visible) {
			//Point home
			Vector3 ray = den.currentDen.transform.position - plr.transform.position;
			Vector3 plrPos = plr.transform.position;
	
			ray.y = 0;
			plrPos.y = 0;
			ray.Normalize();

			orb.transform.position = plr.transform.position + (ray * offset.x) + new Vector3(0, offset.y,0);
		}
	}
}
