using UnityEngine;
using System.Collections;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager : MonoBehaviour {
	public static SaveManager control;

	void Awake(){
		if (control == null) {
			DontDestroyOnLoad (gameObject);	
			control = this;
		} else if (control != this){
			Destroy (this);
		}
	}

	//Saves player data to file
	public void save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

		PlayerData data = new PlayerData ();

		bf.Serialize (file, data);
		file.Close ();
	}

	//Loads player data from a file
	public void load(){
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

			PlayerData data = (PlayerData)bf.Deserialize (file);

			file.Close ();

			//Now update local values with saved values
		}
	}
}

[Serializable]
class PlayerData{

}