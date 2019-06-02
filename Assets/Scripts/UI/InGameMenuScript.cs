using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuScript : MonoBehaviour
{
    

    public GameObject menuContainer;


    void Start() {
        HideInGameMenu();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
			ToggleInGameMenu();
		}
    }

    private void ToggleInGameMenu() {
		menuContainer.SetActive(!menuContainer.activeSelf);
		if(menuContainer.activeSelf) {
			PlayerManager.instance.DeactivatePlayer();
		} else {
			PlayerManager.instance.ActivatePlayer();
		}
	}

    private void HideInGameMenu() {
        menuContainer.SetActive(false);
        PlayerManager.instance.ActivatePlayer();
    }

    public void NavToStartMenu() {
		SceneManager.LoadScene("StartMenu");
	}

}
