﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryScript : MonoBehaviour
{

    // RESPONSIBLE FOR INTERFACE AND MANAGEMENT OF PLAYER INVENTORY


    public GameObject currentSelected;

    
    void Start() {
        // do initializations
        InitInventorySelection();
    }

    void Update() {
        CheckInventorySelection();    
    }

    private void InitInventorySelection() {
        // set default block selection
        currentSelected = BlockTypes.instance.testBlock;
        BlockManager.instance.UpdateGhostBlockType(currentSelected);
        BlockManager.instance.ActivateGhostBlock();
    }

    private void CheckInventorySelection() {
        // TODO: come up with a better system for this
        GameObject oldSelected = currentSelected;
        if(Input.GetKey(KeyCode.Alpha1)) {
            currentSelected = BlockTypes.instance.testBlock;
        } else if(Input.GetKey(KeyCode.Alpha2)) {
            currentSelected = BlockTypes.instance.plainBlock;
        } else if(Input.GetKey(KeyCode.Alpha3)) {
            currentSelected = BlockTypes.instance.moverBlock;
        }
        // if inventory item was changed, update the current ghost block type
        if(oldSelected != currentSelected) {
            BlockManager.instance.UpdateGhostBlockType(currentSelected);
        }
    }

}