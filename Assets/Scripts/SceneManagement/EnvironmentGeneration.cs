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
		GenerateTestCubeEnvironment();
        // GenerateFlatLandEnvironment();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void GenerateTestCubeEnvironment() {
        int size = 10;
        Vector3 pos = new Vector3();
        GenerateBlockChunk(size, size, size, pos);
    }

    private void GenerateFlatLandEnvironment() {
        int width = 100;
        int depth = 100;
        int height = 1;
        Vector3 pos = new Vector3(0, 0, 0);
        GenerateBlockChunk(width, height, depth, pos);
    }

    private void GenerateRectangularRoomEnvironment(int width, int height, int depth) {
        // TODO
    }

    private void GenerateBlockChunk(int width, int height, int depth, Vector3 position) {
        int halfWidth = width / 2;
        int halfHeight = height / 2;
        int halfDepth = depth / 2;
        for (int i = -halfWidth; i < halfWidth; i++) {
            for (int j = -halfHeight; j < halfHeight; j++) {
                for (int k = -halfDepth; k < halfDepth; k++) {
                    // create block from prefab
                    Vector3 blockPosition = new Vector3(i, j, k);
                    Instantiate(
                        blockPrefab, 
                        blockPosition, 
                        transform.rotation, 
                        blocks.transform
                    );
                }
            }
        }
    }

}
