using UnityEngine;
using System.Collections;

public interface IDamagable {

	float MaxHealth{
		get;
		set;
	}

	float Health{
		get;
		set;
	}

	float HealthMod{
		get;
		set;
	}
		
	void takeDamage(float ammount);
}

public interface IKillable {
	bool isDead {
		get;
		set;
	}

	void Die ();
}

public interface IResourceUse {
	float MaxEnergy{
		get;
		set;
	}

	float Energy{
		get;
		set;
	}

	float EnergyMod{
		get;
		set;
	}
}

public interface IBaseStats {

}