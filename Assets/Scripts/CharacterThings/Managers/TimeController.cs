using UnityEngine;
using System.Collections;

public class TimeController : MonoBehaviour {
	public bool paused;

	void Start () {
		paused = true;
	}
	
	public void resume(){
		Time.timeScale = 1;
		paused = false;
	}

	public void pause(){
		Time.timeScale = 0;
		paused = true;
	}
}
