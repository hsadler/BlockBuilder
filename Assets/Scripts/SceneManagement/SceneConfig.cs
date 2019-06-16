using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneConfig : MonoBehaviour
{

	// REGISTRY FOR SCENE CONFIG VARIABLES


	public float tickDurationSeconds = 1.0f;


	// the static reference to the singleton instance
	public static SceneConfig instance { get; private set; }

	void Awake() {
		// singleton pattern
		if(instance == null) {
			instance = this;
		} else {
			Destroy(gameObject);
		}
	}

}
