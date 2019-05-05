using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneConfig : MonoBehaviour
{

    // REGISTRY FOR GENERAL SCENE VARIABLES


    public float tickDurationSeconds = 1.0f;


    // the static reference to the singleton instance
    public static SceneConfig instance { get; private set; }

    void Awake() {
        // singleton pattern
        if(instance == null) {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

}
