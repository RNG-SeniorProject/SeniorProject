using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	[SerializeField]
	private float distanceAway;
	[SerializeField]
	private float distanceUp;
	[SerializeField]
	private float smooth;
	[SerializeField]
	private Transform followXForm;
	[SerializeField]
	private Vector3 lookDir;
	[SerializeField]
	private float mouseRotSpeed;

	private Vector3 targetPosition;
	private Vector3 velocityCamSmooth = Vector2.zero;

	[SerializeField]
	private float camSmoothDampTime = 0.1f;

	void Start () {
		//followXForm = GameObject.FindWithTag ("Player").transform.Find("Follow");
		//lookDir = followXForm.forward;
	}

	void Update () {
	
	}

	void LateUpdate(){
		Vector3 characterOffset = followXForm.position + new Vector3(0f, distanceUp, 0f);

		lookDir = characterOffset - this.transform.position;
		lookDir.y = 0;
		lookDir.Normalize ();

		targetPosition = characterOffset + Vector3.up * distanceUp - lookDir * distanceAway;

		CompensateForWalls (characterOffset, ref targetPosition);

		smoothPosition(this.transform.position, targetPosition);

		Debug.DrawRay (followXForm.position, targetPosition - followXForm.position);

		//this.transform.RotateAround (followXForm.position, Vector3.up, Input.GetAxis("Mouse X") * mouseRotSpeed * Time.deltaTime);

		transform.LookAt (followXForm);
	}

	private void smoothPosition(Vector3 fromPos, Vector3 toPos){
		this.transform.position = Vector3.SmoothDamp (fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
	}

	private void CompensateForWalls(Vector3 fromObject, ref Vector3 toTarget){
		RaycastHit wallHit = new RaycastHit ();
		if (Physics.Linecast (fromObject, toTarget, out wallHit)) {
			toTarget = new Vector3 (wallHit.point.x, toTarget.y, wallHit.point.z);
		}
	}
}
