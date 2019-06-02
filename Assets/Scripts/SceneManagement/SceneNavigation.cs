using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour 
{


	public GameObject inGameMenu;
	public GameObject player;
	
	
	private PlayerControlScript playerControlScript;


	void Start() {
		playerControlScript = player.GetComponent<PlayerControlScript>();
	}

	void Update() {}

	public void NavToStartMenu() {
		SceneManager.LoadScene("StartMenu");
	}

	public void QuitGame() {
		Application.Quit();
	}

}
