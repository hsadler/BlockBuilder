using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBlockScript : BaseBlockScript
{

	public new void Start() {
		base.Start();
	}

	public override void EvaluateAtTick() {
		PowerForwardConnectedBlock();
	}

	public override void PlayerFKeyInteraction(PlayerToBlockMessage message) {
		base.PlayerFKeyInteraction(message);
	}

	// IMPLEMENTATION METHODS

	private void PowerForwardConnectedBlock() {
		Vector3 neighborCoords =
			transform.position + (transform.rotation * Vector3.forward);
		if(BlockManager.instance.BlockExists(neighborCoords)) {
			GameObject connectedBlock =
				BlockManager.instance.GetBlock(neighborCoords);
			BaseBlockScript connectedBlockScript =
				connectedBlock.GetComponent<BaseBlockScript>();
			if(connectedBlockScript.AcceptsPower) {
				connectedBlockScript.PowerOn();
			}
		}
	}

}
