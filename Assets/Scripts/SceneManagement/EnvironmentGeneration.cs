using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGeneration : MonoBehaviour
{

	// RESPONSIBLE FOR PROCEDURAL ENVIRONMENT GENERATION


	// the static reference to the singleton instance
	public static EnvironmentGeneration instance { get; private set; }

	void Awake() {
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
		if(GameManagement.instance.startSceneFromLoad) {
			GameSaveManager.instance.LoadGameFromJsonFile();
			GameManagement.instance.startSceneFromLoad = false;
		} else {
			GenerateFlatLandEnvironment();
		}
	}

	private void GenerateTestCubeEnvironment() {
		int size = 10;
		Vector3 dimensions = new Vector3(size, size, size);
		Vector3 pos = new Vector3(-size/2, -size/2, -size/2);
		GenerateBlockChunk(BlockTypes.instance.baseBlock, dimensions, pos);
	}

	private void GenerateFlatLandEnvironment() {
		int width = 50;
		int height = 1;
		int depth = 50;
		Vector3 dimensions = new Vector3(width, height, depth);
		Vector3 pos = new Vector3(-width/2, -height, -depth/2);
		GenerateBlockChunk(BlockTypes.instance.baseBlock, dimensions, pos);
	}

	private void GenerateRectangularRoomEnvironment(
		Vector3 dimensions,
		Vector3 position
	) {
		// TODO: implement stub
	}

	private void GenerateBlockChunk(
		GameObject blockPrefab,
		Vector3 dimensions,
		Vector3 position
	) {
		float width = dimensions[0];
		float depth = dimensions[1];
		float height = dimensions[2];
		for (float i = position[0]; i < position[0] + width; i++) {
			for (float j = position[1]; j < position[1] + depth; j++) {
				for (float k = position[2]; k < position[2] + height; k++) {
					Vector3 blockPosition = new Vector3(i, j, k);
					BlockManager.instance.CreateBlock(
						blockPrefab,
						blockPosition,
						Quaternion.Euler(Vector3.zero)
					);
				}
			}
		}
	}

}
