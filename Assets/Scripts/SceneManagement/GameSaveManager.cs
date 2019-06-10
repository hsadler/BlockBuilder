using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{

	// RESPONSIBLE FOR GAME SAVING AND LOADING


	private const string TEST_SAVE_DIR = "/saves/";
	private const string TEST_SAVE_FILENAME = "test_save.json";

	private const string SAVE_DIR = "/saves/";
	private const string SAVE_FILENAME = "save.json";


	// the static reference to the singleton instance
	public static GameSaveManager instance { get; private set; }

	void Awake() {
		// singleton pattern
		if(instance == null) {
			instance = this;
			// DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}

	void Start () {
		Init();
	}

	void Update() {}

	public void Init() {
		if(!Directory.Exists(Application.persistentDataPath + TEST_SAVE_DIR)) {
			Directory.CreateDirectory(Application.persistentDataPath + TEST_SAVE_DIR);
		}
		if(!Directory.Exists(Application.persistentDataPath + SAVE_DIR)) {
			Directory.CreateDirectory(Application.persistentDataPath + SAVE_DIR);
		}
	}

	public void TestSave() {
		print("Testing Save...");
		int saveVal = 99;
		TestSave ts = new TestSave(saveVal);
		string json = JsonUtility.ToJson(ts);
		print("json to save: " + json);
		print("path to save: " + GetTestSavePath());
		File.WriteAllText(GetTestSavePath(), json, Encoding.UTF8);
	}

	public void TestLoad() {
		print("Testing Load...");
		print("reading from file at path: " + GetTestSavePath());
		string json = File.ReadAllText(GetTestSavePath());
		print("json read from file: " + json);
		TestSave ts = JsonUtility.FromJson<TestSave>(json);
		print("myVal from TestSave object: " + ts.myVal);
	}

	public void SaveGameToJsonFile() {
		GameSave gameSave = new GameSave();
		// get player data and set to serializable structs
		// print("getting player data for save...");
		GameObject player = PlayerManager.instance.player;
		// print("player position: " + player.transform.position.ToString());
		Vector3Struct playerPositionStruct = new Vector3Struct(
			player.transform.position
		);
		// print(string.Format(
		// 	"player position struct x:{0}, y:{1}, z:{2}",
		// 	playerPositionStruct.x,
		// 	playerPositionStruct.y,
		// 	playerPositionStruct.z
		// ));
		Vector3Struct playerRotationStruct = new Vector3Struct(
			player.transform.rotation.eulerAngles
		);
		PlayerStruct playerStruct = new PlayerStruct(
			playerPositionStruct,
			playerRotationStruct
		);
		// get block data and set to serializable structs
		foreach (Block b in BlockManager.instance.GetBlocksAsList()) {
			BlockStruct blockStruct = b.ToBlockStruct();
			gameSave.AddBlock(blockStruct);
		}
		// commit the save data
		print("save game to file...");
		gameSave.SetPlayer(playerStruct);
		string json = JsonUtility.ToJson(gameSave);
		print("json to save: " + json);
		print("path to save: " + GetSavePath());
		File.WriteAllText(GetSavePath(), json, Encoding.UTF8);
	}

	public void LoadGameFromJsonFile() {
		print("loading game from save file...");
		print("reading from file at path: " + GetSavePath());
		string json = File.ReadAllText(GetSavePath());
		print("json read from file: " + json);
		GameSave gameSave = JsonUtility.FromJson<GameSave>(json);
		// get player data from save data 
		Vector3 playerPosition = new Vector3(
			gameSave.player.position.x,
			gameSave.player.position.y,
			gameSave.player.position.z
		);
		Vector3 playerRotation = new Vector3(
			gameSave.player.rotation.x,
			gameSave.player.rotation.y,
			gameSave.player.rotation.z
		);
		// set player position and rotation from save data
		GameObject player = PlayerManager.instance.player;
		player.transform.position = playerPosition;
		player.transform.rotation = Quaternion.Euler(playerRotation);
		// clear all blocks from scene first
		BlockManager.instance.ClearAllBlocks();
		foreach (BlockStruct blockStruct in gameSave.blocks) {
			// get block data from save data
			GameObject blockPrefab = BlockTypes.instance.GetBlockGameObjectFromType(blockStruct.type);
			Vector3 blockPosition = new Vector3(
				blockStruct.position.x,
				blockStruct.position.y,
				blockStruct.position.z
			);
			Vector3 blockRotation = new Vector3(
				blockStruct.rotation.x,
				blockStruct.rotation.y,
				blockStruct.rotation.z
			);
			// create block from save data
			EnvironmentGeneration.instance.CreateBlock(
				blockPrefab,
				blockPosition,
				Quaternion.Euler(blockRotation)
			);
		}
	}

	private string GetTestSavePath() {
		return Application.persistentDataPath + TEST_SAVE_DIR + TEST_SAVE_FILENAME;
	}

	private string GetSavePath() {
		return Application.persistentDataPath + SAVE_DIR + SAVE_FILENAME;
	}

}
