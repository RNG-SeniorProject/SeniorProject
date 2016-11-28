using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterStats : Destructible {
	private float _level;

	[SerializeField]
	private float _maxEnergy;
	[SerializeField]
	private float _energy;

	public float Level{
		get{ return _level;}
		set{ _level = value;}
	}

	public float MaxEnergy{
		get{ return _maxEnergy;}
		set{ _maxEnergy = value;}
	}

	public float Energy{
		get{ return _energy;}
		set{ _energy = Mathf.Clamp (value, 0, MaxEnergy); }
	}

	void Start(){
		Init ();
	}

	protected override void Init(){
		base.Init ();

		Energy = MaxEnergy;
	}

	void Update(){
		if (util.time.paused) {return;}

		update ();
	}

	protected override void update(){
		base.update ();
	}

	public bool useEnergy(float value){
		if (isDead) {return false;}

		if (value != 0 && Energy == 0) {return false;}

		Energy += value;

		uiManager.changePlayerEnergy (this);

		return true;
	}

}
