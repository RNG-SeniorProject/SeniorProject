using UnityEngine;
using System.Collections;

public class PredatorLogic : MonoBehaviour {

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
	private AnimatorStateInfo stateInfo;

	private PredatorController predatorController;

	private int m_LocomotionId = 0;

	void Start () {
		animator = GetComponent<Animator> ();

		if (animator.layerCount >= 2) {
			animator.SetLayerWeight (1, 1);
		}

		m_LocomotionId = Animator.StringToHash ("Base Layer.Locomotion");

		predatorController = gameObject.GetComponent ("PredatorController") as PredatorController;
	}

	void Update () {
		if (animator) {
			stateInfo = animator.GetCurrentAnimatorStateInfo (0);

			//stickToWorldspace (this.transform, gamecam.transform, ref direction, ref speed);

			if (predatorController.IsChasing ()) {
				animator.SetFloat ("Speed", 1);
				// animator.SetFloat ("Direction", direction, directionDampTime, Time.deltaTime);
			} else {
				animator.SetFloat ("Speed", 0);
				// animator.SetFloat ("Direction", direction, directionDampTime, Time.deltaTime);
			}
		}
	}

	/*void FixedUpdate(){
		if (IsInLocomotion () && ((direction >= 0 && hor >= 0) || (direction < 0 && hor < 0))) {
			Vector3 rotationAmount = Vector3.Lerp (Vector3.zero, new Vector3 (0f, rotationDegreePerSecond * (hor < 0f ? -1f : 1f), 0f), Mathf.Abs (hor));
			Quaternion deltaRotation = Quaternion.Euler (rotationAmount * Time.deltaTime);
			this.transform.rotation = (this.transform.rotation * deltaRotation);
		}
	}*/

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
