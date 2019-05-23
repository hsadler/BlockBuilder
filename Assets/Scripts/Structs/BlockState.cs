using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState
{


    public Vector3 position;
    public Quaternion rotation;


    public BlockState(Vector3 position, Quaternion rotation) {
        this.position = position;
        this.rotation = rotation;
    }

}
