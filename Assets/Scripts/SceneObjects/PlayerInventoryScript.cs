using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryScript : MonoBehaviour
{
    
    private BlockTypesRegistry blockTypesRegistry;

    private GameObject currentSelected;
    
    // Start is called before the first frame update
    void Start() {
        // set references
        blockTypesRegistry = BlockTypesRegistry.instance;
        // set default block selection
        currentSelected = blockTypesRegistry.GetByName("Block1");
    }

    // Update is called once per frame
    void Update() {
        CheckInventorySelection();    
    }

    private void CheckInventorySelection() {
        if(Input.GetKey(KeyCode.Alpha1)) {
            currentSelected = blockTypesRegistry.GetByName("Block1");
        }
    }

}
