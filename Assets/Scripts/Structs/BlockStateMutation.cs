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

    public void AddMoveVector(Vector3 moveVector) {
        moveVectors.Add(moveVector);
    }

    public Vector3 GetCombinedMoveVectors() {
        Vector3 combinedVectors = Vector3.zero;
        foreach (Vector3 v in moveVectors) {
            combinedVectors += v;
        }
        return combinedVectors;
    }

    public void AddRotation(Quaternion rotation) {
        rotations.Add(rotation);
    }

    public Quaternion GetCombinedRotations() {
        Quaternion combinedRotations = Quaternion.Euler(Vector3.zero);
        foreach (Quaternion q in rotations) {
            combinedRotations *= q;
        }
        return combinedRotations;
    }

}
