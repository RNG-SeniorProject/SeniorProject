using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
	public GameObject enemy;

	void Start () {
		InvokeRepeating ("Spawn", 1, 30);
	}

	private void Spawn(){
		Instantiate (enemy, new Vector3(0,5,0), new Quaternion(0,0,0,0));
	}
}
