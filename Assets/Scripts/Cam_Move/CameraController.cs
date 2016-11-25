using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	[SerializeField]
	private float distanceAwayFollow;
	[SerializeField]
	private float distanceUpFollow;

	[SerializeField]
	private float distanceAwayAim;
	[SerializeField]
	private float distanceUpAim;
	[SerializeField]
	private float distanceRightAim;

	[SerializeField]
	private float smooth;
	[SerializeField]
	private Transform follow;
	[SerializeField]
	public Vector3 lookDir;
	[SerializeField]
	private float mouseRotSpeed;
	[SerializeField]
	private float camSmoothDampTime = 0.1f;

	private Vector3 targetPosition;
	private Vector3 velocityCamSmooth = Vector2.zero;

	public CamState state;

	public float upVector;

	public enum CamState {
		Follow, 
		Aim,
		Target
	}

	void Start () {
		//followXForm = GameObject.FindWithTag ("Player").transform.Find("Follow");
		//lookDir = followXForm.forward;

		state = CamState.Follow;
	}

	void Update () {
		if (Input.GetMouseButtonDown (1)) {
			if (state == CamState.Follow) {
				state = CamState.Aim;
				//Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = true;
			} else if (state == CamState.Aim) {
				state = CamState.Follow;
			}
		}
	}

	void LateUpdate(){
		Vector3 characterOffset = follow.position + new Vector3 (0f, 1.5f, 0f);

		if (state == CamState.Follow) {
			lookDir = characterOffset - this.transform.position;
			lookDir.y = 0;
			lookDir.Normalize ();

			targetPosition = characterOffset + Vector3.up * distanceUpFollow - lookDir * distanceAwayFollow;

			this.transform.RotateAround (follow.position, Vector3.up, Input.GetAxis ("Mouse X") * mouseRotSpeed * Time.deltaTime);

			CompensateForWalls (characterOffset, ref targetPosition);

			smoothPosition (this.transform.position, targetPosition);

			transform.LookAt (follow.position);

		} else if (state == CamState.Aim) {
			lookDir = follow.forward;
			lookDir.Normalize ();

			targetPosition = characterOffset + follow.up * distanceUpAim - follow.forward * distanceAwayAim + follow.right * distanceRightAim;

			upVector = Mathf.Clamp(upVector + Input.GetAxis ("Mouse Y") * (mouseRotSpeed/5) * Time.deltaTime, -2, 6);

			lookDir = lookDir * 5 + new Vector3 (0, upVector, 0);
		
			CompensateForWalls (characterOffset, ref targetPosition);

			smoothPosition (this.transform.position, targetPosition);

			transform.LookAt (follow.position + lookDir);
		}
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
