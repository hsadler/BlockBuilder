using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTypesRegistry : MonoBehaviour
{
    
    // REGISTRY FOR BLOCK TYPES


    public IDictionary<string, GameObject> blockNameToGameObject = new Dictionary<string, GameObject>()
    {
		{ "Block1", null },
		{ "Block2", null },
		{ "Block3", null }
	};


    // the static reference to the singleton instance
    public static BlockTypesRegistry instance { get; private set; }

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

    public GameObject GetByName(string blockName) {
        // TODO: stub
        return null;
    }

}
