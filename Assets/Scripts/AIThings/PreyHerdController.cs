﻿using UnityEngine;
using System.Collections;

public class PreyHerdController : MonoBehaviour {

	private PreyController[] preyControllers;

	void Start () {
		GameObject[] prey = new GameObject[transform.childCount];
		for (int i = 0; i < prey.Length; i++) {
			prey [i] = transform.GetChild (i).gameObject;
		}

		preyControllers = new PreyController[prey.Length];
		for (int i = 0; i < prey.Length; i++) {
			preyControllers[i] = prey [i].GetComponent ("PreyController") as PreyController;
		}
	}

	public void StartFleeing () {
		for (int i = 0; i < preyControllers.Length; i++) {
			preyControllers[i].StartFleeing ();
		}
	}

	public void StopFleeing () {
		for (int i = 0; i < preyControllers.Length; i++) {
			preyControllers[i].StopFleeing ();
		}
	}

}
