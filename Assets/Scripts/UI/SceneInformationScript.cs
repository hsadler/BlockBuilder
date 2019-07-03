using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneInformationScript : MonoBehaviour
{


	private Rigidbody playerRb;
	private PlayerInventoryScript playerInventoryScript;


	void Start() {
		GameObject playerGO = PlayerManager.instance.player.gameObject;
		playerRb = playerGO.GetComponent<Rigidbody>();
		playerInventoryScript = playerGO.GetComponent<PlayerInventoryScript>();
	}

	void Update() {
		GetComponent<Text>().text = string.Format(
			"Selected block: {0}\nBlock count: {1}",
			playerInventoryScript.currentSelected.name,
			BlockManager.instance.blockDict.Count
		);

	}

}
