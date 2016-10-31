using UnityEngine;
using System.Collections;

public class VisionCones : MonoBehaviour {

	public GameObject target;

	void Update () {
		Vector3 targetDir = target.transform.position - transform.position;
		float angle = Vector3.Angle(targetDir, transform.forward);

		if(angle < 30.0f)
			Debug.Log( "close" );
	}
}
