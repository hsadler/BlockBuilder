using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNavigation : MonoBehaviour
{

	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}

}
