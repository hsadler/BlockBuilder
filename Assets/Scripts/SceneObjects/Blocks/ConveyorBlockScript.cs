using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBlockScript : BaseBlockScript
{


	public float conveyorSpeed = 1.0f;

	
	public new void Awake() {
		base.Awake();
		blockType = BlockTypes.instance.CONVEYOR_BLOCK;
	}

	public new void Start() {
		base.Start();
	}

	public override void EvaluateAtTick() {
		EvalMoveNeighbor(Vector3.up, Vector3.forward);
	}

	public override void PlayerFKeyInteraction(PlayerToBlockMessage message) {
		base.PlayerFKeyInteraction(message);
	}

	// IMPLEMENTATION METHODS

	private void EvalMoveNeighbor(Vector3 neighborDirection, Vector3 moveDirection) {
		Vector3 neighborCoords = blockState.position + (blockState.rotation * neighborDirection);
		if(BlockManager.instance.BlockExists(neighborCoords)) {
			Block neighborBlock = BlockManager.instance.GetBlock(neighborCoords);
			moveDirection = blockState.rotation * moveDirection;
			BlockStateMutation bsm = neighborBlock.script.blockStateMutation;
			bsm.AddMoveVector(moveDirection * conveyorSpeed);
		}
	}

}
