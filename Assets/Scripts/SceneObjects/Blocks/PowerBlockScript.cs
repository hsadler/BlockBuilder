using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBlockScript : BaseBlockScript
{


	public new void Awake() {
		base.Awake();
		blockType = BlockTypes.instance.POWER_BLOCK;
	}

	public new void Start() {
		base.Start();
	}

	public override void AsyncEvalInteractions() {
		EvalPowerForwardConnectedBlock();
	}

	public override void PlayerFKeyInteraction(PlayerToBlockMessage message) {
		base.PlayerFKeyInteraction(message);
	}

	// IMPLEMENTATION METHODS

	private void EvalPowerForwardConnectedBlock() {
		Vector3 neighborCoords =
			blockState.position + (blockState.rotation * Vector3.forward);
		if(BlockManager.instance.BlockExists(neighborCoords)) {
			Block connectedBlock = BlockManager.instance.GetBlock(neighborCoords);
			if(connectedBlock.script.AcceptsPower) {
				connectedBlock.script.blockStateMutation.AddPower(1);
			}
		}
	}

}
