using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInformationScript : MonoBehaviour
{


	public GameObject player;
	private Rigidbody playerRb;
	private PlayerInventoryScript playerInventoryScript;


	void Start() {
		playerRb = player.GetComponent<Rigidbody>();
		playerInventoryScript = player.GetComponent<PlayerInventoryScript>();
	}

	void Update() {
		GetComponent<Text>().text = string.Format(
			"Player velocity magnitude: {0}\nSelected block: {1}",
			Mathf.Round(playerRb.velocity.magnitude),
			playerInventoryScript.currentSelected.name
		);

	}

}
