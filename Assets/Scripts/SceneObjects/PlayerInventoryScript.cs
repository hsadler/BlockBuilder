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
        // set default block selection
        currentSelected = blockTypes.block1;
    }

    // Update is called once per frame
    void Update() {
        CheckInventorySelection();    
    }

    private void CheckInventorySelection() {
        if(Input.GetKey(KeyCode.Alpha1)) {
            currentSelected = blockTypes.block1;
        } else if(Input.GetKey(KeyCode.Alpha2)) {
            currentSelected = blockTypes.block2;
        }
    }

}
