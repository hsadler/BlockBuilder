using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGeneration : MonoBehaviour {

	// RESPONSIBLE FOR PROCEDURAL ENVIRONMENT GENERATION


    public GameObject blocks;
    public GameObject blockPrefab;


	// the static reference to the singleton instance
    public static EnvironmentGeneration instance { get; private set; }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        // singleton pattern
        if(instance == null) {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

	// Use this for initialization
	void Start () {
		GenerateEnvironment();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void GenerateEnvironment() {
        int size = 10;
        int halfSize = size / 2;
        for (int i = -halfSize; i < halfSize; i++) {
            for (int j = -halfSize; j < halfSize; j++) {
                for (int k = -halfSize; k < halfSize; k++) {
                    // create block
                    Vector3 position = new Vector3(i, j, k);
                    GameObject newBlock = Instantiate(
                        blockPrefab, 
                        position, 
                        transform.rotation, 
                        blocks.transform
                    );
                }
            }
        }
    }

}
