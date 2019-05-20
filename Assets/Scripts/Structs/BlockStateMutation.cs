using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BlockStateMutation
{


    public Vector3 position;
    public Quaternion rotation;


    public BlockStateMutation(Vector3 position, Quaternion rotation) {
        this.position = position;
        this.rotation = rotation;
    }

}
