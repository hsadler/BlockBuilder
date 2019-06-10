using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTypes : MonoBehaviour
{

	// REGISTRY FOR BLOCK TYPES


	public string BASE_BLOCK = "base_block";
	public string GHOST_BLOCK = "ghost_block";
	public string TEST_BLOCK = "test_block";
	public string PLAIN_BLOCK = "plain_block";
	public string MOVER_BLOCK = "mover_block";
	public string ROTATOR_BLOCK = "rotator_block";
	public string LIGHT_BLOCK = "light_block";
	public string CONVEYOR_BLOCK = "conveyor_block";
	public string POWER_BLOCK = "power_block";


	public GameObject baseBlock;
	public GameObject ghostBlock;
	public GameObject testBlock;
	public GameObject plainBlock;
	public GameObject moverBlock;
	public GameObject rotatorBlock;
	public GameObject lightBlock;
	public GameObject conveyorBlock;
	public GameObject powerBlock;

	public IDictionary<string, GameObject> blockTypeToGameObject = new Dictionary<string, GameObject>();


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
		Init();
	}

	private void Init() {
		blockTypeToGameObject.Add(BASE_BLOCK, baseBlock);
		blockTypeToGameObject.Add(GHOST_BLOCK, ghostBlock);
		blockTypeToGameObject.Add(TEST_BLOCK, testBlock);
		blockTypeToGameObject.Add(PLAIN_BLOCK, plainBlock);
		blockTypeToGameObject.Add(MOVER_BLOCK, moverBlock);
		blockTypeToGameObject.Add(ROTATOR_BLOCK, rotatorBlock);
		blockTypeToGameObject.Add(LIGHT_BLOCK, lightBlock);
		blockTypeToGameObject.Add(CONVEYOR_BLOCK, conveyorBlock);
		blockTypeToGameObject.Add(POWER_BLOCK, powerBlock);
	}

	public GameObject GetBlockGameObjectFromType(string type) {
		return blockTypeToGameObject[type];
	}


}
