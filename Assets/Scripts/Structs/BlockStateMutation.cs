using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStateMutation
{


    public List<Vector3> moveVectors;
    public List<Quaternion> rotations;


    public BlockStateMutation() {
        Init();
    }

    public void Init() {
        moveVectors = new List<Vector3>();
        rotations = new List<Quaternion>();
    }

    public Vector3 getCombinedMoveVectors() {
        Vector3 combinedVectors = Vector3.zero;
        foreach (Vector3 v in moveVectors) {
            combinedVectors += v;
        }
        return combinedVectors;
    }

}
