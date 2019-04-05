using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    
    // DATASTORE AND SERVICE FOR MANAGING GAME BLOCKS


    public IDictionary<string, GameObject> coordToBlockDict = new Dictionary<string, GameObject>();
    public List<GameObject> blockList = new List<GameObject>();


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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetFormattedCoordinateFromBlock(GameObject block) {
        string formattedCoordinateString = string.Format(
            "{0},{1},{2}", 
            block.transform.position.x,
            block.transform.position.y, 
            block.transform.position.z
        );
        return formattedCoordinateString;
    }

    public string GetFormattedCoordinateString(Vector3 coordinate) {
        string formattedCoordinateString = string.Format(
            "{0},{1},{2}", 
            coordinate[0],
            coordinate[1],
            coordinate[2]
        ); 
        return formattedCoordinateString; 
    }

    public bool SetBlock(Vector3 coordinate) {
        // TODO
        return false;
    }

    public GameObject GetBlock(Vector3 coordinate) {
        GameObject block = null;
        string formattedCoords = GetFormattedCoordinateString(coordinate);
        if(coordToBlockDict.ContainsKey(formattedCoords)) {
            block = coordToBlockDict[formattedCoords];
        } else {
            Debug.Log("block not found at coordinate x:" + coordinate[0] + " y:" + coordinate[1] + " z:" + coordinate[2]);
        }
        return block;
    }
}
