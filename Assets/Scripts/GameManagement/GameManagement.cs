using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{

	// GLOBAL GAME MANAGEMENT


	public bool startSceneFromLoad = false;


	// the static reference to the singleton instance
	public static GameManagement instance { get; private set; }

	void Awake() {
		// singleton pattern
		if(instance == null) {
			instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}

}
