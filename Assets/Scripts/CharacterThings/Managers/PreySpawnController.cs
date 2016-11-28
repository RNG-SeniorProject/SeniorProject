using UnityEngine;
using System.Collections;

public class PreySpawnController : MonoBehaviour {

	public Util util;

	void Start(){
		util = GameObject.Find ("GameManager").GetComponent<Util> ();

	}

	public void spawnNearPlayerDen(Den den){
		if (den.herds.Count < den.foodSpawn) {
			float rand = Random.value;

			GameObject herd;

			float randx = Random.Range (-1, 1);
			float randy = Random.Range (-1, 1);

			if (rand < .1){
				herd = (GameObject)Instantiate (util.lonePrey, den.transform.position + (den.transform.right * 10) + (32 * new Vector3(randx, 0, randy)), Quaternion.identity);
			} else if (rand < .6) {
				herd = (GameObject)Instantiate (util.smallHerd, den.transform.position + (den.transform.right * 10) + (32 * new Vector3(randx, 0, randy)), Quaternion.identity);
			} else if (rand < .9) {
				herd = (GameObject)Instantiate (util.mediumHerd, den.transform.position + (den.transform.right * 10) + (32 * new Vector3(randx, 0, randy)), Quaternion.identity);
			} else {
				herd = (GameObject)Instantiate (util.bigHerd, den.transform.position + (den.transform.right * 10) + (32 * new Vector3(randx, 0, randy)), Quaternion.identity);
			}

			den.addHerd (herd);
		}
	}
}
