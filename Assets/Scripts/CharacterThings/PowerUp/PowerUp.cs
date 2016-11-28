using UnityEngine;
using System.Collections;

public abstract class PowerUp : MonoBehaviour {

	public Util util;

	void Start(){
		Init();
	}

	protected virtual void Init(){
		util = GameObject.Find ("GameManager").GetComponent<Util>();
	}

	public abstract void powerUp ();
}
