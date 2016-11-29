using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class Interactable : MonoBehaviour{
	public Util util;

	protected InteractionController plr;
	protected UIManager uiManager;

	public string interactionString; 

	public bool active = true;

	void Start(){
		Init ();
	}

	public virtual void Init(){
		util = GameObject.Find ("GameManager").GetComponent<Util> ();
		plr = util.intController;
		uiManager = util.uiManager;
	}
	
	public abstract void interact (GameObject chr);

	void OnTriggerEnter(Collider hit){
		if (util.time.paused == true)
			return;

		if (hit.GetComponent<InteractionController> () != null) {
			triggerEnter (hit);
		}
	}

	protected virtual void triggerEnter(Collider hit){
		if (!active) {return;}

		addToPlr ();
	}

	void OnTriggerExit(Collider hit){
		if (util.time.paused == true)
			return;

		if (hit.GetComponent<InteractionController> () != null) {
			triggerExit (hit);
		}
	}

	protected virtual void triggerExit(Collider hit){
		if (!active) {return;}

		util.interactionUi.gameObject.SetActive (false);
		removeFromPlr ();
	}

	public void addToPlr(){
		uiManager = util.uiManager;
		plr = util.intController;

		uiManager.updateInteractionText (interactionString);
		util.interactionUi.gameObject.SetActive (true);
		plr.interactions.Add (this);
	}

	public virtual void removeFromPlr(){
		util.interactionUi.gameObject.SetActive (false);
		plr.interactions.Remove (this);
	}
}