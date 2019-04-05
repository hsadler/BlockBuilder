using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

	private EnvironmentGeneration environmentGeneration;
	private BlockManager blockManager;

	public IDictionary<string, Vector3> sensorTagToDirectionVector3 = new Dictionary<string, Vector3>()
	{
		{ "TopSensor", new Vector3(0, 1, 0) },
		{ "LeftSensor", new Vector3(-1, 0, 0) },
		{ "RightSensor", new Vector3(1, 0, 0) },
		{ "FrontSensor", new Vector3(0, 0, -1) },
		{ "BackSensor", new Vector3(0, 0, 1) },
		{ "BottomSensor", new Vector3(0, -1, 0) }
	};


	// Use this for initialization
	void Start () {
		// set references
		environmentGeneration = EnvironmentGeneration.instance;
        blockManager = BlockManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool DestroyBlock() {
		blockManager.UnsetBlock(gameObject);
		Destroy(gameObject);
		return true;
	}

	public GameObject AddNewBlockAsNeighbor(string hitTag) {
		// TODO: this method probably shouldn't be here, not the responsibility of the Block class
		Vector3 direction = sensorTagToDirectionVector3[hitTag];
		Vector3 newBlockPosition = transform.position + direction;
		return environmentGeneration.CreateBlock(newBlockPosition);
	}

	public void PlayerLeftClickInteraction(string hitTag) {
		Debug.LogFormat(
			"game object tag: {0} was left clicked with child object tag: {1}",
			gameObject.tag,
			hitTag
		);
		DestroyBlock();
	}

	public void PlayerRightClickInteraction(string hitTag) {
		Debug.LogFormat(
			"game object tag: {0} was right clicked with child object tag: {1}",
			gameObject.tag,
			hitTag
		);
		AddNewBlockAsNeighbor(hitTag);
	}

}
