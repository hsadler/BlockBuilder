using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuMenuScript : MonoBehaviour
{
    
    public void NavToNewGame() {
        SceneManager.LoadScene("Main");
    }

    public void QuitGame() {
        Application.Quit();
    }

}
