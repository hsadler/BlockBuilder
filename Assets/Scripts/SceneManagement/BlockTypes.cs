using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTypes : MonoBehaviour
{
    
    // REGISTRY FOR BLOCK TYPES


    public GameObject baseBlock;
    public GameObject ghostBlock;
    public GameObject block1;
    public GameObject block2;


    // the static reference to the singleton instance
    public static BlockTypes instance { get; private set; }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
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
