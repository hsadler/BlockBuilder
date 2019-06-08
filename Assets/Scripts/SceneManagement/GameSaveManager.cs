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
        print("Testing Save");
        int saveVal = 99;
        TestSave ts = new TestSave(saveVal);
        string json = JsonUtility.ToJson(ts);
        print("json to save: " + json);
        print("path to save: " + GetSavePath());
        File.WriteAllText(GetSavePath(), json, Encoding.UTF8);
    }

    public void TestLoad() {
        // stub
    }

    public void LoadGameFromJsonFile() {
        // stub
    }

    public void SaveGameToJsonFile() {
        // stub
    }

    private string GetTestSavePath() {
        return Application.persistentDataPath + TEST_SAVE_DIR + TEST_SAVE_FILENAME;
    }

    private string GetSavePath() {
        return Application.persistentDataPath + SAVE_DIR + SAVE_FILENAME;
    }

}
