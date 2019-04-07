using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    
    // DATASTORE AND SERVICE FOR MANAGING GAME BLOCKS


    public IDictionary<string, GameObject> coordsToBlockDict = new Dictionary<string, GameObject>();


    // the static reference to the singleton instance
    public static BlockManager instance { get; private set; }

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
    
    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update() {}

    public string GetFormattedCoordinateFromBlock(GameObject block) {
        string formattedCoordinatesString = string.Format(
            "{0},{1},{2}", 
            block.transform.position.x,
            block.transform.position.y, 
            block.transform.position.z
        );
        return formattedCoordinatesString;
    }

    public string GetFormattedCoordinateString(Vector3 coordinates) {
        string formattedCoordinatesString = string.Format(
            "{0},{1},{2}", 
            coordinates[0],
            coordinates[1],
            coordinates[2]
        ); 
        return formattedCoordinatesString; 
    }

    public bool BlockExists(Vector3 coordinates) {
        string formattedCoords = GetFormattedCoordinateString(coordinates);
        return coordsToBlockDict.ContainsKey(formattedCoords); 
    }

    public GameObject GetBlock(Vector3 coordinates) {
        GameObject block = null;
        if(BlockExists(coordinates)) {
            string formattedCoords = GetFormattedCoordinateString(coordinates);
            block = coordsToBlockDict[formattedCoords];
        } else {
            Debug.Log("block not found at coordinate x:" + coordinates[0] + " y:" + coordinates[1] + " z:" + coordinates[2]);
        }
        return block;
    }
    
    public bool SetBlock(GameObject block) {
        // add to coordinates->block dictionary
        string coordsKey = GetFormattedCoordinateFromBlock(block);
        coordsToBlockDict.Add(coordsKey, block);
        return true;
    }

    public bool UnsetBlock(GameObject block) {
        // remove from coordinates->block dictionary if possible
        if(!BlockExists(block.transform.position)) {
            return false;
        } else {
            string formattedCoords = GetFormattedCoordinateFromBlock(block);
            coordsToBlockDict.Remove(formattedCoords);
            return true;
        }
    }

}
