using UnityEngine;
using System.Collections;

public class CharacterLogic : MonoBehaviour {

	[SerializeField]
	private Animator animator;
	[SerializeField]
	private float directionDampTime = .25f;
	[SerializeField]
	private CameraController gamecam;
	[SerializeField]
	private float directionSpeed = 3.0f;
	[SerializeField]
	private float rotationDegreePerSecond = 120f;

	private float speed = 0.0f;
	private float direction = 0f;
	private float hor = 0.0f;
	private float ver = 0.0f;
	private float mouseHor = 0.0f;
	private float mouseVer = 0.0f;

	private AnimatorStateInfo stateInfo;

	private int m_LocomotionId = 0;

	void Start () {
		animator = GetComponentInChildren<Animator> ();

		if (animator.layerCount >= 2) {
			animator.SetLayerWeight (1, 1);
		}

		m_LocomotionId = Animator.StringToHash ("Base Layer.Locomotion");
	}

	void Update () {
		if (animator) {
			stateInfo = animator.GetCurrentAnimatorStateInfo (0);

			hor = Input.GetAxis ("Horizontal");
			ver = Input.GetAxis ("Vertical");

			mouseHor = Mathf.Clamp(Input.GetAxis ("Mouse X"), -1f, 1f);
			mouseVer = Input.GetAxis ("Mouse Y");

			stickToWorldspace (this.transform, gamecam.transform, ref direction, ref speed);

			animator.SetFloat ("Speed", speed);
			animator.SetFloat ("Direction", direction, directionDampTime, Time.deltaTime);

			if (gamecam.state == CameraController.CamState.Aim) {
				Quaternion deltaRotation = Quaternion.Euler (new Vector3(0, rotationDegreePerSecond * mouseHor, 0));
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.transform.rotation * deltaRotation, 2 * Time.deltaTime);
			}
		}
	}

	void FixedUpdate(){
		if (IsInLocomotion () && ((direction >= 0 && hor >= 0) || (direction < 0 && hor < 0))) {
			Vector3 rotationAmount = Vector3.Lerp (Vector3.zero, new Vector3 (0f, rotationDegreePerSecond * (hor < 0f ? -1f : 1f), 0f), Mathf.Abs (hor));
				
			Quaternion deltaRotation = Quaternion.Euler ((rotationAmount) * Time.deltaTime);
			this.transform.rotation = (this.transform.rotation * deltaRotation);
		}
	}

	public void stickToWorldspace(Transform root, Transform camera, ref float directionOut, ref float speedOut){
		Vector3 rootDirection = root.forward;
		Vector3 inputDirection = new Vector3 (hor, 0, ver);

		speedOut = inputDirection.sqrMagnitude;

		Vector3 CamDirection = camera.forward;
		CamDirection.y = 0.0f;
		Quaternion refShift = Quaternion.FromToRotation (Vector3.forward, CamDirection);

		Vector3 moveDirection = refShift * inputDirection;
		Vector3 axisSign = Vector3.Cross (moveDirection, rootDirection);

		float angleRootToMove = Vector3.Angle (rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);

		angleRootToMove /= 180f;

		directionOut = angleRootToMove * directionSpeed;
	}

	public bool IsInLocomotion(){
		return stateInfo.fullPathHash == m_LocomotionId;
	}
}
