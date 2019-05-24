using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBlockScript : BaseBlockScript
{


    public float conveyorSpeed = 1.0f;


    public new void Start() {
        base.Start();
    }

	public override void EvaluateAtTick() {
        MoveNeighbor(Vector3.up, Vector3.forward);
    }

    public override void PlayerFKeyInteraction(PlayerToBlockMessage message) {
		base.PlayerFKeyInteraction(message);
	}

    // IMPLEMENTATION METHODS

    private void MoveNeighbor(Vector3 neighborDirection, Vector3 moveDirection) {
        Vector3 neighborCoords = transform.position + (transform.rotation * neighborDirection);
        if(BlockManager.instance.BlockExists(neighborCoords)) {
            GameObject neighborBlock = BlockManager.instance.GetBlock(neighborCoords);
            moveDirection = transform.rotation * moveDirection;
            BlockStateMutation bsm = neighborBlock.GetComponent<BaseBlockScript>().blockStateMutation;
            bsm.AddMoveVector(moveDirection * conveyorSpeed);
        }
    }

}
