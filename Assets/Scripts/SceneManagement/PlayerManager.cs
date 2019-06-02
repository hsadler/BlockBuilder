using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

	// SERVICE FOR MANAGING PLAYER


    public GameObject player;
	public GameObject playerReticle;


    private PlayerControlScript playerControlScript;


	// the static reference to the singleton instance
	public static PlayerManager instance { get; private set; }

	void Awake() {
		// singleton pattern
		if(instance == null) {
			instance = this;
			// DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}

	void Start() {
        playerControlScript = player.GetComponent<PlayerControlScript>();
    }

	void Update() {}

    public void ActivatePlayer() {
		playerControlScript.ActivatePlayer();
		playerReticle.SetActive(true);
	}

    public void DeactivatePlayer() {
		playerControlScript.DeactivatePlayer();
		playerReticle.SetActive(false);
	}

}
