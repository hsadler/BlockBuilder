using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

	// SERVICE FOR MANAGING PLAYER

	
    public Player player;
	public GameObject playerPrefab;
	public GameObject playerReticle;

	private Vector3 defaultPlayerPosition = new Vector3(0, 2, 0);
	private Vector3 defaultPlayerRotation = new Vector3(0, 0, 0);


	// the static reference to the singleton instance
	public static PlayerManager instance { get; private set; }

	void Awake() {
		// singleton pattern
		if(instance == null) {
			instance = this;
		} else {
			Destroy(gameObject);
		}
		AddPlayerToScene();
	}

	void Start() {}

	void Update() {}

	private void AddPlayerToScene() {
		GameObject playerGO = Instantiate(
			playerPrefab,
			defaultPlayerPosition,
			Quaternion.Euler(defaultPlayerRotation)
		);
		player = new Player(
			playerGO,
			playerGO.GetComponent<PlayerControlScript>(),
			playerGO.GetComponent<PlayerInventoryScript>()
		);
		player.controlScript.GetCameraComponent().gameObject.SetActive(true);
	}

    public void ActivatePlayer() {
		player.controlScript.ActivatePlayer();
		playerReticle.SetActive(true);
	}

    public void DeactivatePlayer() {
		player.controlScript.DeactivatePlayer();
		playerReticle.SetActive(false);
	}

}
