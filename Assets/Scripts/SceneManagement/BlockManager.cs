﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    
    // DATASTORE AND SERVICE FOR MANAGING GAME BLOCKS


    public IDictionary<string, GameObject> coordsToBlockDict = new Dictionary<string, GameObject>();
    public GameObject blocksContainer;
    public GameObject ghostBlock;


    // the static reference to the singleton instance
    public static BlockManager instance { get; private set; }

    void Awake() {
        // singleton pattern
        if(instance == null) {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        InvokeRepeating("evaluateBlocks", 0, SceneConfig.instance.tickDurationSeconds);
    }

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

    // BLOCK EVALUATION METHODS

    private void evaluateBlocks() {
        List<GameObject> gos = new List<GameObject>(coordsToBlockDict.Values);
        foreach (GameObject go in gos) {
            go.GetComponent<BaseBlockScript>().EvaluateAtTick();
        }
    }

    // GHOST BLOCK METHODS

    public void UpdateGhostBlockType(GameObject blockPrefab) {
        Destroy(ghostBlock);
        ghostBlock = Instantiate(
            blockPrefab,
            Vector3.zero,
            transform.rotation,
            blocksContainer.transform
        );
        ghostBlock.GetComponent<BaseBlockScript>().TransformToGhostBlock();
    }

    public void UpdateGhostBlockPosition(Vector3 position) {
        ghostBlock.transform.position = position;
    }

    public void RotateGhostBlock(Vector3 direction) {
        // TODO: rotates based on initial orientation, so not intuitive
        // rotation should be done where origin is current orientation
        if(ghostBlock.activeSelf) {
            
            // Debug.Log("rotating ghost block...");
            Vector3 addRotation = direction * 90.0f;

            // TODO: figure out Lerp later with coroutine
            // float rotationSpeed = 1;
            // ghostBlock.transform.rotation = Quaternion.Lerp(
            //     ghostBlock.transform.rotation,
            //     Quaternion.Euler(newRotation),
            //     Time.time * rotationSpeed
            // );

            // NAIVE SOLUTION: snap to new rotation
            Quaternion gr = ghostBlock.transform.rotation;
            // ghostBlock.transform.Rotate(gr.eulerAngles + addRotation, Space.Self);
            ghostBlock.transform.Rotate(addRotation, Space.Self); 

        }
    }

    public void ActivateGhostBlock() {
        ghostBlock.SetActive(true);
    }

    public void DeactivateGhostBlock() {
        ghostBlock.SetActive(false);
    }

}
