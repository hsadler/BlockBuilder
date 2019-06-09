using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour 
{
	
	// (TODO: may not be needed)
	// RESPONSIBLE FOR PROVIDING SCENE NAVIGATION METHODS


	public GameObject inGameMenu;
	public GameObject player;
	

	void Start() {}

	void Update() {}

	public void NavToStartMenu() {
		SceneManager.LoadScene("StartMenu");
	}

	public void QuitGame() {
		Application.Quit();
	}

}
