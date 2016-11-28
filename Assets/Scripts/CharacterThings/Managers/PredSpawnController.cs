using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PredSpawnController : MonoBehaviour {

	public Util util;

	public List<GameObject> predPacks;

	public int maxPred = 1;

	public float spawnTime = 1000;

	void Start(){
		util = GameObject.Find ("GameManager").GetComponent<Util> ();
	}

	void Update(){
		if (util.den.currentDen != this) {
			spawnTime += Time.deltaTime;

			if (spawnTime > 60) {
				spawnNearPlayerDen ();
			}
		}

		foreach (GameObject pred in predPacks) {
			if (pred.transform.childCount == 0) {
				removePred (pred);
			}
		}
	}

	public void addPred(GameObject pred){
		predPacks.Add (pred);
		spawnTime = 0;

	}

	public void removePred(GameObject pred){
		predPacks.Remove (pred);
	}

	public void spawnNearPlayerDen(){
		if (predPacks.Count < maxPred) {
			float rand = Random.value;

			GameObject pred;

			if (rand < .8){
				pred = (GameObject)Instantiate (util.lonePred, transform.position, Quaternion.identity);
			} else {
				pred = (GameObject)Instantiate (util.lonePack, transform.position, Quaternion.identity);
			} 

			addPred (pred);
		}
	}
}
