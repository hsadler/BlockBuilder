using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{

	// RESPONSIBLE FOR GAME SAVING AND LOADING


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

	void Start () {}
    
    void Update() {}

    public void TestSave() {
        print("Testing Save");
        int saveVal = 99;
        TestSave ts = new TestSave(saveVal);
        string json = JsonUtility.ToJson(ts);
        print("json to save: " + json);
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

}
