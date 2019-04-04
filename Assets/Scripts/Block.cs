using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void PlayerLeftClickInteraction(string hitTag) {
		Debug.LogFormat(
			"game object tag: {0} was left clicked with child object tag: {1}",
			gameObject.tag,
			hitTag
		);
	}

	void PlayerRightClickInteraction(string hitTag) {
		Debug.LogFormat(
			"game object tag: {0} was right clicked with child object tag: {1}",
			gameObject.tag,
			hitTag
		);
	}

}
