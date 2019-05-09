using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTypes : MonoBehaviour
{
    
    // REGISTRY FOR BLOCK TYPES


    public GameObject baseBlock;
    public GameObject ghostBlock;
    public GameObject testBlock;
    public GameObject plainBlock;
    public GameObject moverBlock;
    public GameObject rotatorBlock;
    public GameObject lightBlock;
    public GameObject conveyorBlock;
    public GameObject powerBlock;


    // the static reference to the singleton instance
    public static BlockTypes instance { get; private set; }

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
