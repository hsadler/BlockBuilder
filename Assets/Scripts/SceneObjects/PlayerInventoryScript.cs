using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryScript : MonoBehaviour
{
    
    private BlockTypes blockTypes;

    public GameObject currentSelected;
    
    // Start is called before the first frame update
    void Start() {
        // set references
        blockTypes = BlockTypes.instance;
        // do initializations
        InitInventorySelection();
    }

    // Update is called once per frame
    void Update() {
        CheckInventorySelection();    
    }

    private void InitInventorySelection() {
        // set default block selection
        currentSelected = blockTypes.block1;
        BlockManager.instance.UpdateGhostBlockType(currentSelected);
        BlockManager.instance.ActivateGhostBlock();
    }

    private void CheckInventorySelection() {
        GameObject oldSelected = currentSelected;
        if(Input.GetKey(KeyCode.Alpha1)) {
            currentSelected = blockTypes.block1;
        } else if(Input.GetKey(KeyCode.Alpha2)) {
            currentSelected = blockTypes.block2;
        }
        // if inventory item was changed, update the current ghost block type
        if(oldSelected != currentSelected) {
            BlockManager.instance.UpdateGhostBlockType(currentSelected);
        }
    }

}
