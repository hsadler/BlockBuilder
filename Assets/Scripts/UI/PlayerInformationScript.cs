using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInformationScript : MonoBehaviour
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
			"Player velocity magnitude: {0}\nSelected block: {1}",
			Mathf.Round(playerRb.velocity.magnitude),
			playerInventoryScript.currentSelected.name
		);

	}

}
